using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FitnessTrackingApp.Models;

public class Workout
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ExerciseName { get; set; } = string.Empty;

    [Required]
    public int Sets { get; set; }

    [Required]
    public int Reps { get; set; }
    public double WeightKg { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    [Required]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey(nameof(UserId))]
    public IdentityUser? User { get; set; }
}