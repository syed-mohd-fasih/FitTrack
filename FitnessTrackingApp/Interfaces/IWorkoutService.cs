using FitnessTrackingApp.DTOs;
using FitnessTrackingApp.Models;

namespace FitnessTrackingApp.Interfaces;

// Service interface for managing user workouts
public interface IWorkoutService
{
    // Get all workouts for a specific user
    Task<List<Workout>> GetUserWorkoutsAsync(string userId);

    // Get a specific workout by ID for a specific user
    Task<Workout?> GetWorkoutByIdAsync(int id, string userId);

    // Create a new workout for a user using the provided DTO
    Task<Workout> CreateWorkoutAsync(string userId, CreateWorkoutDto dto);

    // Update an existing workout for a user using the provided DTO
    Task<Workout?> UpdateWorkoutAsync(int id, string userId, UpdateWorkoutDto dto);

    // Delete a workout by ID for a specific user
    Task<bool> DeleteWorkoutAsync(int id, string userId);
}
