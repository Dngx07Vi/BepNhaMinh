using System.ComponentModel.DataAnnotations;

namespace BepNhaMinh.Models;

public class CookingStep
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    [Range(1, 100)] public int StepNumber { get; set; }
    [Required, MaxLength(150)] public string Title { get; set; } = string.Empty;
    [Required, MaxLength(1000)] public string Description { get; set; } = string.Empty;
    [MaxLength(300)] public string? ImageUrl { get; set; }
    public Recipe? Recipe { get; set; }
}
