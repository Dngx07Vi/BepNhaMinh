using BepNhaMinh.Models;

namespace BepNhaMinh.ViewModels;

public class RecipeDetailViewModel
{
    public Recipe Recipe { get; set; } = null!;
    public IEnumerable<RecipeIngredient> Ingredients { get; set; } = [];
    public IEnumerable<CookingStep> CookingSteps { get; set; } = [];
    public IEnumerable<Recipe> RelatedRecipes { get; set; } = [];
    public bool IsFavorite { get; set; }
}
