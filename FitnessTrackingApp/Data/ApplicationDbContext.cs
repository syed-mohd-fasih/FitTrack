using FitnessTrackingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessTrackingApp.Data;

// Database context for the FitnessTrackingApp application
// Inherits from IdentityDbContext to include Identity (user authentication) tables
public class ApplicationDbContext : IdentityDbContext
{
    // Constructor that accepts DbContext options and passes them to the base class
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSet representing the collection of Workout entities in the database
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Meal> Meals { get; set; }
}
