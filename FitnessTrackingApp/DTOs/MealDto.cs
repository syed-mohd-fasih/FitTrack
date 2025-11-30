namespace FitnessTrackingApp.DTOs;

public class MealDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Calories { get; set; }
    public int ProteinGrams { get; set; }
    public DateTime Date { get; set; }
    public bool Consumed { get; set; } = false;
}
