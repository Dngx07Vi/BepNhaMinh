using BepNhaMinh.Services;
using BepNhaMinh.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BepNhaMinh.Controllers;

public class RecipeController(IRecipeService recipeService) : Controller
{
    public async Task<IActionResult> Index(string? search, string? category)
    {
        var recipes = await recipeService.GetAllRecipesAsync(search, category);
        var all = await recipeService.GetAllRecipesAsync();
        var categories = all.Select(r => r.Category!).GroupBy(c => c.Id).Select(g => g.First()).OrderBy(c => c.Name);
        var favorites = await recipeService.GetFavoriteRecipesAsync(GetClientKey());
        return View(new RecipeListViewModel { Recipes = recipes, Categories = categories, SearchKeyword = search ?? "", SelectedCategory = category ?? "", FavoriteIds = favorites.Select(x => x.Id).ToHashSet() });
    }
    public async Task<IActionResult> Details(int id)
    {
        var recipe = await recipeService.GetRecipeByIdAsync(id);
        if (recipe is null) return NotFound();
        var related = await recipeService.GetRelatedRecipesAsync(id, recipe.CategoryId);
        return View(new RecipeDetailViewModel { Recipe = recipe, Ingredients = recipe.RecipeIngredients, CookingSteps = recipe.CookingSteps.OrderBy(s => s.StepNumber), RelatedRecipes = related, IsFavorite = await recipeService.IsFavoriteAsync(id, GetClientKey()) });
    }
    public async Task<IActionResult> Random() => View(await recipeService.GetRandomRecipeAsync());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleFavorite(int id, string? returnUrl)
    {
        var recipe = await recipeService.GetRecipeByIdAsync(id);
        if (recipe is null) return NotFound();
        var isFavorite = await recipeService.ToggleFavoriteAsync(id, GetClientKey());
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest") return Json(new { success = true, isFavorite, message = isFavorite ? "Đã thêm vào yêu thích" : "Đã bỏ khỏi yêu thích" });
        TempData["Toast"] = isFavorite ? "Đã thêm vào yêu thích" : "Đã bỏ khỏi yêu thích";
        return LocalRedirect(returnUrl ?? $"/Recipe/Details/{id}");
    }
    private string GetClientKey() => HttpContext.Session.GetString("ClientKey") ?? SetClientKey();
    private string SetClientKey() { var key = Guid.NewGuid().ToString("N"); HttpContext.Session.SetString("ClientKey", key); return key; }
}
