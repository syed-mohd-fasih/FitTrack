using FitnessTrackingApp.Interfaces;
using FitnessTrackingApp.DTOs;
using FitnessTrackingApp.Models;
using Microsoft.EntityFrameworkCore;
using FitnessTrackingApp.Data;

namespace FitnessTrackingApp.Services;

public class WorkoutService : IWorkoutService
{
    private readonly ApplicationDbContext _db;
    public WorkoutService(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<List<Workout>> GetUserWorkoutsAsync(string userId)
    {
        return await _db.Workouts
            .Where(w => w.UserId == userId)
            .OrderByDescending(w => w.Date)
            .ToListAsync();
    }
    public async Task<Workout?> GetWorkoutByIdAsync(int id, string userId)
    {
        return await _db.Workouts.FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
    }
    public async Task<Workout> CreateWorkoutAsync(string userId, CreateWorkoutDto dto)
    {
        var workout = new Workout
        {
            ExerciseName = dto.ExerciseName,
            Sets = dto.Sets,
            Reps = dto.Reps,
            WeightKg = dto.WeightKg,
            Date = dto.Date,
            UserId = userId
        };
        _db.Workouts.Add(workout); await _db.SaveChangesAsync(); return workout;
    }
    public async Task<Workout?> UpdateWorkoutAsync(int id, string userId, UpdateWorkoutDto dto)
    {
        var workout = await GetWorkoutByIdAsync(id, userId);

        if (workout == null)
            return null;

        workout.ExerciseName = dto.ExerciseName;
        workout.Sets = dto.Sets;
        workout.Reps = dto.Reps;
        workout.WeightKg = dto.WeightKg;
        workout.Date = dto.Date;

        await _db.SaveChangesAsync();
        return workout;
    }
    public async Task<bool> DeleteWorkoutAsync(int id, string userId)
    {
        var workout = await GetWorkoutByIdAsync(id, userId);
        if (workout == null)
            return false;
        _db.Workouts.Remove(workout);
        await _db.SaveChangesAsync();
        return true;
    }
}