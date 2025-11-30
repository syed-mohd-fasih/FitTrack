using FitnessTrackingApp.Data;
using FitnessTrackingApp.DTOs;
using FitnessTrackingApp.Interfaces;
using FitnessTrackingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessTrackingApp.Services;

public class MealService : IMealService
{
    private readonly ApplicationDbContext _db;

    public MealService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Meal>> GetUserMealsAsync(string userId)
    {
        return await _db.Meals
            .Where(m => m.UserId == userId)
            .OrderBy(m => m.Date)
            .ThenBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<List<Meal>> GetUserMealsByDateAsync(string userId, DateTime date)
    {
        return await _db.Meals
            .Where(m => m.UserId == userId && m.Date.Date == date.Date)
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<Meal?> GetMealByIdAsync(int id, string userId)
    {
        return await _db.Meals.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
    }

    public async Task<Meal> CreateMealAsync(string userId, MealDto dto)
    {
        var meal = new Meal
        {
            Name = dto.Name,
            Description = dto.Description,
            Calories = dto.Calories,
            ProteinGrams = dto.ProteinGrams,
            Date = dto.Date,
            UserId = userId,
            Consumed = dto.Consumed
        };

        _db.Meals.Add(meal);
        await _db.SaveChangesAsync();
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

        await _db.SaveChangesAsync();
        return meal;
    }

    public async Task<bool> DeleteMealAsync(int id, string userId)
    {
        var meal = await GetMealByIdAsync(id, userId);
        if (meal == null) return false;

        _db.Meals.Remove(meal);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<Meal?> ToggleConsumedAsync(int id, string userId)
    {
        var meal = await GetMealByIdAsync(id, userId);
        if (meal == null) return null;

        meal.Consumed = !meal.Consumed;
        await _db.SaveChangesAsync();
        return meal;
    }
}
