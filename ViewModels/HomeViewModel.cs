using BepNhaMinh.Models;

namespace BepNhaMinh.ViewModels;

public class HomeViewModel
{
    public IEnumerable<Recipe> FeaturedRecipes { get; set; } = [];
    public IEnumerable<Category> Categories { get; set; } = [];
    public Recipe? RandomRecipe { get; set; }
    public IEnumerable<Recipe> PopularRecipes { get; set; } = [];
}
