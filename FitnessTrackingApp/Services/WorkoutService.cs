using FitnessTrackingApp.Interfaces;
using FitnessTrackingApp.DTOs;
using FitnessTrackingApp.Models;
using Microsoft.Azure.Cosmos;

namespace FitnessTrackingApp.Services;

public class WorkoutService : IWorkoutService
{
    private readonly Container _container;

    public WorkoutService(CosmosClient client, IConfiguration config)
    {
        _container = client
            .GetDatabase(config["CosmosDb:DatabaseName"])
            .GetContainer("Workouts");
    }

    public async Task<List<Workout>> GetUserWorkoutsAsync(string userId)
    {
        var query = new QueryDefinition(
            "SELECT * FROM c WHERE c.userId = @uid ORDER BY c.date DESC"
        ).WithParameter("@uid", userId);

        var iterator = _container.GetItemQueryIterator<Workout>(query);
        var results = new List<Workout>();

        while (iterator.HasMoreResults)
            results.AddRange(await iterator.ReadNextAsync());

        return results;
    }

    public async Task<Workout?> GetWorkoutByIdAsync(int id, string userId)
    {
        var idStr = id.ToString();

        try
        {
            var response = await _container.ReadItemAsync<Workout>(idStr, new PartitionKey(userId));
            return response.Resource;
        }
        catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<Workout> CreateWorkoutAsync(string userId, CreateWorkoutDto dto)
    {
        var workout = new Workout
        {
            UserId = userId,
            ExerciseName = dto.ExerciseName,
            Sets = dto.Sets,
            Reps = dto.Reps,
            WeightKg = dto.WeightKg,
            Date = dto.Date
        };

        await _container.CreateItemAsync(workout, new PartitionKey(userId));
        return workout;
    }

    public async Task<Workout?> UpdateWorkoutAsync(int id, string userId, UpdateWorkoutDto dto)
    {
        var workout = await GetWorkoutByIdAsync(id, userId);
        if (workout == null) return null;

        workout.ExerciseName = dto.ExerciseName;
        workout.Sets = dto.Sets;
        workout.Reps = dto.Reps;
        workout.WeightKg = dto.WeightKg;
        workout.Date = dto.Date;

        await _container.ReplaceItemAsync(workout, workout.Id, new PartitionKey(userId));
        return workout;
    }

    public async Task<bool> DeleteWorkoutAsync(int id, string userId)
    {
        var workout = await GetWorkoutByIdAsync(id, userId);
        if (workout == null) return false;

        await _container.DeleteItemAsync<Workout>(workout.Id, new PartitionKey(userId));
        return true;
    }
}
