using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FitnessTrackingApp.Models;

// Workout entity representing a user's exercise session
public class Workout
{
    [Key] // Primary key
    public int Id { get; set; }

    [Required] // Name of the exercise (e.g., Bench Press)
    public string ExerciseName { get; set; } = string.Empty;

    [Required] // Number of sets performed
    public int Sets { get; set; }

    [Required] // Number of repetitions per set
    public int Reps { get; set; }

    // Weight used for the exercise in kilograms
    public double WeightKg { get; set; }

    [Required] // Date of the workout session
    public DateTime Date { get; set; } = DateTime.UtcNow;

    [Required] // Associated user's ID
    public string UserId { get; set; } = string.Empty;

    [ForeignKey(nameof(UserId))] // Link to the IdentityUser entity
    public IdentityUser? User { get; set; }
}
