using FitnessTrackingApp.Interfaces;
using FitnessTrackingApp.DTOs;
using FitnessTrackingApp.Models;
using Microsoft.EntityFrameworkCore;
using FitnessTrackingApp.Data;

namespace FitnessTrackingApp.Services;

// Workout service implementation
public class WorkoutService : IWorkoutService
{
    private readonly ApplicationDbContext _db; // Database context

    public WorkoutService(ApplicationDbContext db)
    {
        _db = db; // Inject DB context
    }

    // Get all workouts for a specific user
    public async Task<List<Workout>> GetUserWorkoutsAsync(string userId)
    {
        return await _db.Workouts
            .Where(w => w.UserId == userId) // Filter by user
            .OrderByDescending(w => w.Date) // Latest first
            .ToListAsync();
    }

    // Get a single workout by ID for a specific user
    public async Task<Workout?> GetWorkoutByIdAsync(int id, string userId)
    {
        return await _db.Workouts.FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
    }

    // Create a new workout
    public async Task<Workout> CreateWorkoutAsync(string userId, CreateWorkoutDto dto)
    {
        var workout = new Workout
        {
            ExerciseName = dto.ExerciseName,
            Sets = dto.Sets,
            Reps = dto.Reps,
            WeightKg = dto.WeightKg,
            Date = dto.Date,
            UserId = userId // Assign owner
        };

        _db.Workouts.Add(workout); // Add to DB
        await _db.SaveChangesAsync(); // Save changes
        return workout;
    }

    // Update an existing workout
    public async Task<Workout?> UpdateWorkoutAsync(int id, string userId, UpdateWorkoutDto dto)
    {
        var workout = await GetWorkoutByIdAsync(id, userId); // Fetch workout

        if (workout == null)
            return null; // Not found

        workout.ExerciseName = dto.ExerciseName;
        workout.Sets = dto.Sets;
        workout.Reps = dto.Reps;
        workout.WeightKg = dto.WeightKg;
        workout.Date = dto.Date;

        await _db.SaveChangesAsync(); // Save updates
        return workout;
    }

    // Delete a workout
    public async Task<bool> DeleteWorkoutAsync(int id, string userId)
    {
        var workout = await GetWorkoutByIdAsync(id, userId); // Fetch workout

        if (workout == null)
            return false; // Not found

        _db.Workouts.Remove(workout); // Remove from DB
        await _db.SaveChangesAsync(); // Save deletion
        return true;
    }
}
