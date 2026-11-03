using System.ComponentModel.DataAnnotations;

namespace BepNhaMinh.Models;

public class Recipe
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required, MaxLength(300)]
    public string ImageUrl { get; set; } = string.Empty;

    [Range(0, 1000)] public int PreparationTime { get; set; }
    [Range(0, 1000)] public int CookingTime { get; set; }
    [Range(1, 100)] public int Servings { get; set; }
    [Required, MaxLength(30)] public string Difficulty { get; set; } = "Dễ";
    public int CategoryId { get; set; }
    [Range(0, 10000)] public int Calories { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Category? Category { get; set; }
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    public ICollection<CookingStep> CookingSteps { get; set; } = new List<CookingStep>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
