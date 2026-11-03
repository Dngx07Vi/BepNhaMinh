using System.ComponentModel.DataAnnotations;

namespace BepNhaMinh.Models;

public class Ingredient
{
    public int Id { get; set; }
    [Required, MaxLength(120)] public string Name { get; set; } = string.Empty;
    [MaxLength(50)] public string Unit { get; set; } = string.Empty;
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
