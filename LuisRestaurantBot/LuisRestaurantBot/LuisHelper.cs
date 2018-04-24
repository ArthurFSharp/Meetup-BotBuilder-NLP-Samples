using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuisRestaurantBot
{
    public static class LuisHelper
    {
        public static string ResolveFood(EntityRecommendation entity)
        {
            var value = entity.Resolution["values"] as IEnumerable<object>;


            return (value.ToList())[0].ToString();
        }

        public static int ResolveQuantity(EntityRecommendation entity)
        {
            var value = entity.Resolution["value"];
            
            return Convert.ToInt32(value);
        }
    }
}