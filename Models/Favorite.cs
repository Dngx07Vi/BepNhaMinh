using System.ComponentModel.DataAnnotations;

namespace BepNhaMinh.Models;

public class Favorite
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    [Required, MaxLength(100)] public string ClientKey { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Recipe? Recipe { get; set; }
}
