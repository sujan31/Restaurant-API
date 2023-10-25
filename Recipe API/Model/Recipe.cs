using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipe_API.Model
{
    [Index(nameof(Title),IsUnique=true)]
    public class Recipe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Cusine { get; set; }
        public string? Instructions { get; set; }
        public string? Ingredients { get; set; }   
    }
}
