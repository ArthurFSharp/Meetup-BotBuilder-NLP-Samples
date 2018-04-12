using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bot.Builder.Luisai.Extensions
{
    public static class LuisResultExtensions
    {
        public static bool TryFindEntity(this LuisResult result, string type, out EntityRecommendation luisEntity)
        {
            luisEntity = result?.Entities?.FirstOrDefault(e => e.Type == type);
            return luisEntity != null;
        }

        public static bool TryFindEntities(this LuisResult result, string type, out IEnumerable<EntityRecommendation> luisEntities)
        {
            luisEntities = result?.Entities?.Where(e => e.Type == type);
            return luisEntities != null;
        }
    }
}
