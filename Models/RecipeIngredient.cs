using System.ComponentModel.DataAnnotations;

namespace BepNhaMinh.Models;

public class RecipeIngredient
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int IngredientId { get; set; }
    [Required, MaxLength(100)] public string Quantity { get; set; } = string.Empty;
    public Recipe? Recipe { get; set; }
    public Ingredient? Ingredient { get; set; }
}
