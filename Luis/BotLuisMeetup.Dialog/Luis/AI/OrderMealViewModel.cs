using Bot.Builder.Luisai;
using Bot.Builder.Luisai.Extensions;
using Bot.Builder.Luisai.Parsers;
using BotLuisMeetup.Dialog.Helpers;
using BotLuisMeetup.Dialog.Luis.Entities;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Linq;

namespace BotLuisMeetup.Dialog.Luis.AI
{
    [Serializable]
    public class OrderMealViewModel : BaseViewModel
    {
        public string Food { get; set; }

        public DateTime Date { get; set; }

        public bool HasFoundFood { get; set; } = false;

        public bool HasFoundTime { get; set; } = false;

        public override bool IsValid => !string.IsNullOrEmpty(Food);

        public static OrderMealViewModel ReadFromLuis(LuisResult result)
        {
            var viewModel = new OrderMealViewModel();
            
            var hasFoundFood = result.TryFindEntity(LuisPizzaEntities.Food, out var foodEntity);
            var hasFoundTime = result.TryFindEntities(LuisBuiltinEntities.Time, out var timeEntities);

            viewModel.HasFoundFood = hasFoundFood;
            viewModel.HasFoundTime = hasFoundTime;

            if (hasFoundFood)
            {
                viewModel.Food = foodEntity.Entity;
            }

            if (hasFoundTime)
            {
                var lst = timeEntities.ToList();
                var date = DateTimeParser.ParseDateTime(result, lst[0], "builtin.datetimeV2.time");
            }

            return viewModel;
        }
    }
}