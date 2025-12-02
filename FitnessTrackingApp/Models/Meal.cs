namespace FitnessTrackingApp.Models;

using Newtonsoft.Json;

public class Meal
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonProperty("userId")]
    public string UserId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("calories")]
    public int Calories { get; set; }

    [JsonProperty("proteinGrams")]
    public int ProteinGrams { get; set; }

    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("consumed")]
    public bool Consumed { get; set; }
}
