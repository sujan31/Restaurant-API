using Recipe_API.Model;

namespace Recipe_API.Data_Transfer_Object
{
    public class CreateRecipeDTO
    {
        public string Title { get; set; }
        public string Cusine { get; set; }
        public string Instructions { get; set; }
        public List<Ingredient> Ingredients { get; set; }   


    }
}
