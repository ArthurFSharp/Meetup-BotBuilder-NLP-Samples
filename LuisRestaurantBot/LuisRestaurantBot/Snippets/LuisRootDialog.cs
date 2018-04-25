//using LuisRestaurantBot.Dialogs.Forms;
//using Microsoft.Bot.Builder.Dialogs;
//using Microsoft.Bot.Builder.Luis;
//using Microsoft.Bot.Builder.Luis.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LuisRestaurantBot.Dialogs
//{
//    [Serializable]
//    [LuisModel("4644cc35-203b-4ce3-bf6e-acea9403df2d", "83b4bc108ec947cd8ecf7799afafdad2")]
//    public class LuisRootDialog : LuisDialog<object>
//    {
//        [LuisIntent("")]
//        [LuisIntent("None")]
//        public async Task None(IDialogContext context, LuisResult result)
//        {
//            await context.PostAsync("Je n'ai pas compris ce que vous avez dit.");
//        }

//        [LuisIntent("Commander")]
//        public async Task Commander(IDialogContext context, LuisResult result)
//        {
//            var form = OrderForm.ReadFromLuis(result);

//            var commands = GetOrderedFoods(form);
//            await PrintOrderedFoods(context, commands);
//        }

//        private IEnumerable<OrderedFood> GetOrderedFoods(OrderForm form)
//        {
//            return form.Counts.Zip(form.Items, (q, i) => new OrderedFood(i, q));
//        }

//        private async Task PrintOrderedFoods(IDialogContext context, IEnumerable<OrderedFood> orderedFoods)
//        {
//            var resume = orderedFoods.Select(x => "x" + x.Quantity + " " + x.Name).Aggregate(a String.Join(", ", );
//            await context.PostAsync($"Vous avez choisi : {resume}");
//        }
//    }
//}