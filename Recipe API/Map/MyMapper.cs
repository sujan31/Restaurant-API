using AutoMapper;
using Newtonsoft.Json;
using Recipe_API.Data_Transfer_Object;
using Recipe_API.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Recipe_API.Map
{
    public class MyMapper:Profile
    {
        public MyMapper()
        {
            CreateMap<Recipe,CreateRecipeDTO>().ReverseMap();
            CreateMap<Recipe,RecipeDTOWithId>().ReverseMap();
            CreateMap<List<Ingredient>, string>().ConvertUsing(e => JsonConvert.SerializeObject(e));
            CreateMap<string, List<Ingredient>>().ConvertUsing(e => JsonConvert.DeserializeObject<List<Ingredient>>(e));
        }
    }
}
