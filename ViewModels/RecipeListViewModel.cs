using BepNhaMinh.Models;

namespace BepNhaMinh.ViewModels;

public class RecipeListViewModel
{
    public IEnumerable<Recipe> Recipes { get; set; } = [];
    public IEnumerable<Category> Categories { get; set; } = [];
    public string SearchKeyword { get; set; } = string.Empty;
    public string SelectedCategory { get; set; } = string.Empty;
    public HashSet<int> FavoriteIds { get; set; } = [];
}
