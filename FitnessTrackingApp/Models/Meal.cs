using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FitnessTrackingApp.Models;

public class Meal
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty; // e.g., Breakfast, Lunch, Dinner

    [Required]
    public string Description { get; set; } = string.Empty; // Meal description

    public int Calories { get; set; }

    public int ProteinGrams { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.UtcNow.Date;

    [Required]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey(nameof(UserId))]
    public IdentityUser? User { get; set; }

    public bool Consumed { get; set; } = false;
}
