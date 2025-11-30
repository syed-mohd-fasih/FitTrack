using FitnessTrackingApp.DTOs;
using FitnessTrackingApp.Models;

namespace FitnessTrackingApp.Interfaces;

public interface IWorkoutService
{
    Task<List<Workout>> GetUserWorkoutsAsync(string userId);
    Task<Workout?> GetWorkoutByIdAsync(int id, string userId);
    Task<Workout> CreateWorkoutAsync(string userId, CreateWorkoutDto dto);
    Task<Workout?> UpdateWorkoutAsync(int id, string userId, UpdateWorkoutDto dto);
    Task<bool> DeleteWorkoutAsync(int id, string userId);
}
