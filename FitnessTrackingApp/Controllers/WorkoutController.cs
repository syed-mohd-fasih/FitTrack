using FitnessTrackingApp.DTOs;
using FitnessTrackingApp.Interfaces;
using FitnessTrackingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessTrackingApp.Controllers;

[ApiController]
[Route("api/workouts")]
[Authorize]
public class WorkoutController : ControllerBase
{
    private readonly IWorkoutService _service;

    public WorkoutController(IWorkoutService service)
    {
        _service = service;
    }

    private string GetUserId() =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetUserWorkouts()
    {
        string userId = GetUserId();
        var data = await _service.GetUserWorkoutsAsync(userId);
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkout(CreateWorkoutDto dto)
    {
        string userId = GetUserId();
        var workout = await _service.CreateWorkoutAsync(userId, dto);
        return Ok(workout);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkout(int id, UpdateWorkoutDto dto)
    {
        string userId = GetUserId();
        var updated = await _service.UpdateWorkoutAsync(id, userId, dto);
        if (updated == null) return NotFound("Workout not found.");
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkout(int id)
    {
        string userId = GetUserId();
        var ok = await _service.DeleteWorkoutAsync(id, userId);
        if (!ok) return NotFound("Workout not found.");
        return Ok();
    }
}
