using BepNhaMinh.Services;
using Microsoft.AspNetCore.Mvc;

namespace BepNhaMinh.Controllers;

public class FavoriteController(IRecipeService recipeService) : Controller
{
    public async Task<IActionResult> Index() => View(await recipeService.GetFavoriteRecipesAsync(GetClientKey()));
    private string GetClientKey() => HttpContext.Session.GetString("ClientKey") ?? SetClientKey();
    private string SetClientKey() { var key = Guid.NewGuid().ToString("N"); HttpContext.Session.SetString("ClientKey", key); return key; }
}
