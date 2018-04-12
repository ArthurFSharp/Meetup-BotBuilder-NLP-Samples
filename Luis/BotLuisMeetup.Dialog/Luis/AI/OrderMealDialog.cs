using BotLuisMeetup.Dialog.Luis.AI;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Threading.Tasks;

namespace BotLuisMeetup.Dialog.Luis
{
    public partial class LuisRootDialog
    {
        private OrderMealViewModel _viewModel;

        [LuisIntent("CommanderManger")]
        public async Task OrderMeal(IDialogContext context, LuisResult result)
        {
            _viewModel = OrderMealViewModel.ReadFromLuis(result);
            //EntityRecommendation entityRecommendation;
            string hour = string.Empty;
            string datetime = string.Empty;

            if (_viewModel.IsValid)
            {
                await context.PostAsync("Nous préparons ça pour vous !");
            }

            //if (result.TryFindEntity("builtin.datetimeV2.time", out entityRecommendation))
            //{
            //    hour = entityRecommendation.Entity;
            //}
            //if (result.TryFindEntity("builtin.datetimeV2.datetime", out var entityRecommendation2)) 
            //{
            //    datetime = entityRecommendation2.Entity;
            //}
            await context.PostAsync($"Vous souhaitez manger, time : {hour} - datetime: {datetime}");
        }
    }
}
