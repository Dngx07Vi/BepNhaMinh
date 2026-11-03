using BepNhaMinh.Data;
using BepNhaMinh.Models;
using Microsoft.EntityFrameworkCore;

namespace BepNhaMinh.Services;

public class RecipeService(ApplicationDbContext db) : IRecipeService
{
    public async Task<List<Recipe>> GetAllRecipesAsync(string? keyword = null, string? category = null)
    {
        var query = db.Recipes.Include(r => r.Category).AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            keyword = keyword.Trim();
            query = query.Where(r => r.Name.Contains(keyword) || r.Description.Contains(keyword) || r.Category!.Name.Contains(keyword));
        }
        if (!string.IsNullOrWhiteSpace(category)) query = query.Where(r => r.Category!.Name == category);
        return await query.OrderBy(r => r.Name).ToListAsync();
    }
    public Task<List<Recipe>> SearchRecipesAsync(string keyword) => GetAllRecipesAsync(keyword, null);
    public Task<List<Recipe>> GetRecipesByCategoryAsync(string category) => GetAllRecipesAsync(null, category);
    public async Task<Recipe?> GetRecipeByIdAsync(int id) => await db.Recipes.Include(r => r.Category).Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).Include(r => r.CookingSteps).AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
    public async Task<Recipe?> GetRandomRecipeAsync() { var ids = await db.Recipes.Select(r => r.Id).ToListAsync(); if (ids.Count == 0) return null; var id = ids[Random.Shared.Next(ids.Count)]; return await GetRecipeByIdAsync(id); }
    public async Task<List<Recipe>> GetRelatedRecipesAsync(int recipeId, int categoryId, int count = 4) => await db.Recipes.Include(r => r.Category).Where(r => r.Id != recipeId && r.CategoryId == categoryId).OrderBy(_ => Guid.NewGuid()).Take(count).AsNoTracking().ToListAsync();
    public async Task<List<Recipe>> GetFavoriteRecipesAsync(string clientKey) => await db.Favorites.Where(f => f.ClientKey == clientKey).Include(f => f.Recipe!).ThenInclude(r => r.Category).Select(f => f.Recipe!).AsNoTracking().OrderByDescending(r => r.CreatedAt).ToListAsync();
    public Task<bool> IsFavoriteAsync(int recipeId, string clientKey) => db.Favorites.AnyAsync(f => f.RecipeId == recipeId && f.ClientKey == clientKey);
    public async Task<bool> ToggleFavoriteAsync(int recipeId, string clientKey)
    {
        var favorite = await db.Favorites.FirstOrDefaultAsync(f => f.RecipeId == recipeId && f.ClientKey == clientKey);
        if (favorite is null) db.Favorites.Add(new Favorite { RecipeId = recipeId, ClientKey = clientKey }); else db.Favorites.Remove(favorite);
        await db.SaveChangesAsync();
        return favorite is null;
    }
}
