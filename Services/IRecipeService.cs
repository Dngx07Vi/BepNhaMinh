using BepNhaMinh.Models;

namespace BepNhaMinh.Services;

public interface IRecipeService
{
    Task<List<Recipe>> GetAllRecipesAsync(string? keyword = null, string? category = null);
    Task<Recipe?> GetRecipeByIdAsync(int id);
    Task<Recipe?> GetRandomRecipeAsync();
    Task<List<Recipe>> SearchRecipesAsync(string keyword);
    Task<List<Recipe>> GetRecipesByCategoryAsync(string category);
    Task<List<Recipe>> GetRelatedRecipesAsync(int recipeId, int categoryId, int count = 4);
    Task<List<Recipe>> GetFavoriteRecipesAsync(string clientKey);
    Task<bool> IsFavoriteAsync(int recipeId, string clientKey);
    Task<bool> ToggleFavoriteAsync(int recipeId, string clientKey);
}
