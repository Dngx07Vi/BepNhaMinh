using BepNhaMinh.Services;
using BepNhaMinh.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BepNhaMinh.Controllers;

public class HomeController(IRecipeService recipeService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var all = await recipeService.GetAllRecipesAsync();
        var categories = all.Select(r => r.Category!).GroupBy(c => c.Id).Select(g => g.First()).OrderBy(c => c.Name).ToList();
        return View(new HomeViewModel { FeaturedRecipes = all.Take(4), PopularRecipes = all.Skip(4).Take(8), Categories = categories });
    }
    public IActionResult Privacy() => View();
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] public IActionResult Error() => View(new { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
