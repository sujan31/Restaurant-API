using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipe_API.Data_Transfer_Object;
using Recipe_API.DB;
using Recipe_API.Model;

namespace Recipe_API.Repository
{
    public class RecipeSqlRepo : IRecipeRepo
    {
        private readonly MyDbContext _db;
        private readonly IMapper _mapper;
        public RecipeSqlRepo(MyDbContext db, IMapper mapper) {
            _db = db;
            _mapper = mapper;
        }
        public async Task<CreateRecipeDTO> CreateRecipe(CreateRecipeDTO createRecipeDTO)
        {
            var result = await _db.Recipes.FirstOrDefaultAsync(m => m.Title == createRecipeDTO.Title);
            if (result != null)
            {
                throw new Exception("Recipe with given title already exists");
            }
            var recipe = _mapper.Map<Recipe>(createRecipeDTO);
            await _db.Recipes.AddAsync(recipe);
            await _db.SaveChangesAsync();
            return createRecipeDTO;
        }


        public async Task<List<RecipeDTOWithId>> GetAllRecipes()
        {
            var result = await _db.Recipes.ToListAsync();
            if (result.Count()==0)
            {
                throw new Exception("There are no Recipies in DB");
            }
            var recipes = _mapper.Map<List<RecipeDTOWithId>>(result);
            return recipes;
        }


        public async Task<RecipeDTOWithId> GetRecipeByTitle(string title)
        {
            var result = await _db.Recipes.FirstOrDefaultAsync(r => r.Title.ToLower() == title.ToLower());
            if (result == null)
            {
                throw new Exception($"{title} is not found.");
            }
            var recipe = _mapper.Map<RecipeDTOWithId>(result);
            return recipe;
        }

        public async Task<RecipeDTOWithId> DeleteRecipeById(int id)
        {
            var result = await _db.Recipes.FirstOrDefaultAsync(i => i.Id == id);
            if (result == null) { throw new Exception("There is no recipe associated with given ID"); }
            var recipe = _mapper.Map<RecipeDTOWithId>(result);
            _db.Recipes.Remove(result);
            await _db.SaveChangesAsync();
            return recipe;
        }
        public async Task<RecipeDTOWithId> UpdateRecipe(RecipeDTOWithId dto)
        {
            var result= await _db.Recipes.AsNoTracking().FirstOrDefaultAsync(i=>i.Id == dto.Id);
            var titleExists=_db.Recipes.Any(t=>t.Title.ToLower() == dto.Title.ToLower() && t.Id != dto.Id);
            if(result == null )
            { 
                throw new Exception("There is no recipe associated with given ID");
            }
            else if(titleExists)
            {
                throw new Exception("Alredy a recipe exists with provided title");
            }
            var recipe=_mapper.Map<Recipe>(dto);
            _db.Recipes.Update(recipe);
            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task<List<RecipeDTOWithId>> GetRecipeByCuisine(string cuisine)
        {
            var result=await _db.Recipes.Where(m=>m.Cusine.ToLower().Contains(cuisine.ToLower())).ToListAsync();
            var recipies = _mapper.Map<List<RecipeDTOWithId>>(result);
            if (recipies.Count() == 0)
            {
                throw new Exception("There is no recipe for given cuisine");
            }
            return recipies;
        }
        public async Task<List<RecipeDTOWithId>> GetRecipeByIngredients(string ingredient)
        {
            var result = await _db.Recipes.ToListAsync();
            var mapResult = _mapper.Map<List<RecipeDTOWithId>>(result);
            var recipes=mapResult.Where(i=>i.Ingredients.Any(n=>n.Name.ToLower()==ingredient.ToLower())).ToList();
            if(recipes.Count()==0)
            {
                throw new Exception("There is no recipe with given ingredient");
            }
            return recipes;
        }
    }
}
