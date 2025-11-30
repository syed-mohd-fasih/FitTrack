using FitnessTrackingApp.Models;
using FitnessTrackingApp.DTOs;

namespace FitnessTrackingApp.Interfaces;

public interface IMealService
{
    Task<List<Meal>> GetUserMealsAsync(string userId);
    Task<List<Meal>> GetUserMealsByDateAsync(string userId, DateTime date);
    Task<Meal?> GetMealByIdAsync(int id, string userId);
    Task<Meal> CreateMealAsync(string userId, MealDto dto);
    Task<Meal?> UpdateMealAsync(int id, string userId, MealDto dto);
    Task<bool> DeleteMealAsync(int id, string userId);
    Task<Meal?> ToggleConsumedAsync(int id, string userId);
}
