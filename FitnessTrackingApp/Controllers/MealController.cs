using FitnessTrackingApp.DTOs;
using FitnessTrackingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessTrackingApp.Controllers;

[ApiController]
[Route("api/meals")]
[Authorize]
public class MealController : ControllerBase
{
    private readonly IMealService _service;

    public MealController(IMealService service)
    {
        _service = service;
    }

    private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetUserMeals()
    {
        string userId = GetUserId();
        var meals = await _service.GetUserMealsAsync(userId);
        return Ok(meals);
    }

    [HttpGet("{date}")]
    public async Task<IActionResult> GetMealsByDate(DateTime date)
    {
        string userId = GetUserId();
        var meals = await _service.GetUserMealsByDateAsync(userId, date);
        return Ok(meals);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMeal(MealDto dto)
    {
        string userId = GetUserId();
        var meal = await _service.CreateMealAsync(userId, dto);
        return Ok(meal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMeal(int id, MealDto dto)
    {
        string userId = GetUserId();
        var updated = await _service.UpdateMealAsync(id, userId, dto);
        if (updated == null) return NotFound("Meal not found.");
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeal(int id)
    {
        string userId = GetUserId();
        var ok = await _service.DeleteMealAsync(id, userId);
        if (!ok) return NotFound("Meal not found.");
        return Ok();
    }

    [HttpPatch("toggle/{id}")]
    public async Task<IActionResult> ToggleConsumed(int id)
    {
        string userId = GetUserId();
        var meal = await _service.ToggleConsumedAsync(id, userId);
        if (meal == null) return NotFound("Meal not found.");
        return Ok(meal);
    }
}
