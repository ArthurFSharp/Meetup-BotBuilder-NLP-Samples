using Bot.Builder.Luisai;
using Bot.Builder.Luisai.Extensions;
using BotLuisMeetup.Dialog.Helpers;
using BotLuisMeetup.Dialog.Luis.Entities;
using Microsoft.Bot.Builder.Luis.Models;
using System;

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

            }

            return viewModel;
        }
    }
}