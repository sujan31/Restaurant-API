using Microsoft.AspNetCore.Mvc;
using Recipe_API.Data_Transfer_Object;

namespace Recipe_API.Repository
{
    public interface IRecipeRepo
    {
        public Task<CreateRecipeDTO> CreateRecipe(CreateRecipeDTO input);
        public Task<List<RecipeDTOWithId>> GetAllRecipes();
        public Task<RecipeDTOWithId> GetRecipeByTitle(string title);
        public Task<RecipeDTOWithId> DeleteRecipeById(int id);
        public Task<RecipeDTOWithId> UpdateRecipe(RecipeDTOWithId dto);
        public Task<List<RecipeDTOWithId>> GetRecipeByCuisine(string cuisine);
        public Task<List<RecipeDTOWithId>> GetRecipeByIngredients(string ingredient);
    }
}
