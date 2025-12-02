namespace FitnessTrackingApp.Models;

using Newtonsoft.Json;

public class Workout
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonProperty("userId")]
    public string UserId { get; set; }

    [JsonProperty("exerciseName")]
    public string ExerciseName { get; set; }

    [JsonProperty("sets")]
    public int Sets { get; set; }

    [JsonProperty("reps")]
    public int Reps { get; set; }

    [JsonProperty("weightKg")]
    public float WeightKg { get; set; }

    [JsonProperty("date")]
    public DateTime Date { get; set; }
}
