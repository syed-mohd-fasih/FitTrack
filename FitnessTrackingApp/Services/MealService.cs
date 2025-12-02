using FitnessTrackingApp.Interfaces;
using FitnessTrackingApp.DTOs;
using FitnessTrackingApp.Models;
using Microsoft.Azure.Cosmos;

namespace FitnessTrackingApp.Services;

public class MealService : IMealService
{
    private readonly Container _container;

    public MealService(CosmosClient client, IConfiguration config)
    {
        _container = client
            .GetDatabase(config["CosmosDb:DatabaseName"])
            .GetContainer("Meals");
    }

    public async Task<List<Meal>> GetUserMealsAsync(string userId)
    {
        var query = new QueryDefinition(
            "SELECT * FROM c WHERE c.userId = @uid ORDER BY c.date, c.name"
        ).WithParameter("@uid", userId);

        var iterator = _container.GetItemQueryIterator<Meal>(query);
        var results = new List<Meal>();

        while (iterator.HasMoreResults)
            results.AddRange(await iterator.ReadNextAsync());

        return results;
    }

    public async Task<List<Meal>> GetUserMealsByDateAsync(string userId, DateTime date)
    {
        var query = new QueryDefinition(
            "SELECT * FROM c WHERE c.userId = @uid AND c.date = @date ORDER BY c.name"
        )
        .WithParameter("@uid", userId)
        .WithParameter("@date", date.Date);

        var iterator = _container.GetItemQueryIterator<Meal>(query);
        var results = new List<Meal>();

        while (iterator.HasMoreResults)
            results.AddRange(await iterator.ReadNextAsync());

        return results;
    }

    public async Task<Meal?> GetMealByIdAsync(int id, string userId)
    {
        // Legacy int ID → convert to string to match Cosmos
        var idStr = id.ToString();

        try
        {
            var response = await _container.ReadItemAsync<Meal>(idStr, new PartitionKey(userId));
            return response.Resource;
        }
        catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<Meal> CreateMealAsync(string userId, MealDto dto)
    {
        var meal = new Meal
        {
            UserId = userId,
            Name = dto.Name,
            Description = dto.Description,
            Calories = dto.Calories,
            ProteinGrams = dto.ProteinGrams,
            Date = dto.Date,
            Consumed = dto.Consumed
        };

        await _container.CreateItemAsync(meal, new PartitionKey(userId));
        return meal;
    }

    public async Task<Meal?> UpdateMealAsync(int id, string userId, MealDto dto)
    {
        var meal = await GetMealByIdAsync(id, userId);
        if (meal == null) return null;

        meal.Name = dto.Name;
        meal.Description = dto.Description;
        meal.Calories = dto.Calories;
        meal.ProteinGrams = dto.ProteinGrams;
        meal.Date = dto.Date;
        meal.Consumed = dto.Consumed;

        await _container.ReplaceItemAsync(meal, meal.Id, new PartitionKey(userId));
        return meal;
    }

    public async Task<bool> DeleteMealAsync(int id, string userId)
    {
        var meal = await GetMealByIdAsync(id, userId);
        if (meal == null) return false;

        await _container.DeleteItemAsync<Meal>(meal.Id, new PartitionKey(userId));
        return true;
    }

    public async Task<Meal?> ToggleConsumedAsync(int id, string userId)
    {
        var meal = await GetMealByIdAsync(id, userId);
        if (meal == null) return null;

        meal.Consumed = !meal.Consumed;

        await _container.ReplaceItemAsync(meal, meal.Id, new PartitionKey(userId));
        return meal;
    }
}
