using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;

namespace RestaurantBot
{
    public static class LuisHelper
    {
        public static DateTime ResolveDateTime(EntityRecommendation entity)
        {
            var data = ((entity.Resolution["values"] as List<object>)[0] as IDictionary<string, object>);

            if (data["type"].ToString() == "datetimerange")
            {
                return DateTime.Parse(data["start"] as string);
            }
            else if (data["type"].ToString() == "datetime")
            {
                return DateTime.Parse(data["value"] as string);
            }
            else if (data["type"].ToString() == "time")
            {
                var timespan = TimeSpan.Parse(data["value"] as string);
                return DateTime.Now.Date + timespan;
            }

            return DateTime.MinValue;
        }
    }
}