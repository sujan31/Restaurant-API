using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipe_API.DB;
using AutoMapper;
using Recipe_API.Data_Transfer_Object;
using Recipe_API.Model;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Recipe_API.Repository;

namespace Recipe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepo _repo;

        public RecipeController(IRecipeRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("CreateRecipe")]
        public async Task<ActionResult> CreateRecipe(CreateRecipeDTO input)
        {
            try
            {
                 var recipe=await _repo.CreateRecipe(input);
                return CreatedAtAction("GetRecipeByTitle", new { title = recipe.Title }, recipe);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("GetAllRecipe")]
        public async Task<ActionResult> GetAllRecipe()
        {   
            try
            {
                 var recipes = await _repo.GetAllRecipes();
                 return Ok(recipes);

            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [Route("GetRecipeByTitle")]
        [HttpGet]
        public async Task<ActionResult> GetRecipeByTitle(string title)
        {
            try
            {
                var recipe = await _repo.GetRecipeByTitle(title);
                return Ok(recipe);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteRecipeWitId")]
        public async Task<ActionResult> DeleteRecipeById(int id)
        {
            try
            {
                var recipe = await _repo.DeleteRecipeById(id);
                return Ok(recipe);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
           
        }

        
        [HttpPut]
        [Route("UpdateRecipe")]
        public async Task<ActionResult> UpdateRecipe( RecipeDTOWithId dto)
        {
            try
            {
                var recipe = await _repo.UpdateRecipe(dto);
                return CreatedAtAction("GetRecipeByTitle", new { title = recipe.Title },recipe);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [Route("GetRecipeWithCuisine")]
        [HttpGet]
        public async Task<ActionResult> GetRecipeByCuisine(string cuisine)
        {
            try
            {
                var recipes = await _repo.GetRecipeByCuisine(cuisine);
                return Ok(recipes);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
            
        }

        [Route("GetRecipeWithIngredients")]
        [HttpGet]
        public async Task<ActionResult> GetRecipeByIngredients(string ingredient)
        {
            try
            {
                var recipes = await _repo.GetRecipeByIngredients(ingredient);
                return Ok(recipes);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
