namespace FitnessTrackingApp.DTOs;

// DTO for creating a new workout
public class CreateWorkoutDto
{
    // Name of the exercise
    public string ExerciseName { get; set; } = string.Empty;

    // Number of sets
    public int Sets { get; set; }

    // Number of repetitions per set
    public int Reps { get; set; }

    // Weight used in kilograms
    public float WeightKg { get; set; }

    // Date of the workout
    public DateTime Date { get; set; }
}

// DTO for updating an existing workout
// Inherits all properties from CreateWorkoutDto
public class UpdateWorkoutDto : CreateWorkoutDto { }
