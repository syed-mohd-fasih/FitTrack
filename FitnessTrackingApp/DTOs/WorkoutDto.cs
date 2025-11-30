namespace FitnessTrackingApp.DTOs;

public class CreateWorkoutDto
{
    public string ExerciseName { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public double WeightKg { get; set; }
    public DateTime Date { get; set; }
}

public class UpdateWorkoutDto : CreateWorkoutDto { }