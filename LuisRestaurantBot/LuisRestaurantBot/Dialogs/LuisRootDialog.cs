using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using LuisRestaurantBot.Dialogs.Forms;
using System.Linq;
using System.Collections.Generic;

namespace LuisRestaurantBot.Dialogs
{
    [Serializable]
    [LuisModel("4644cc35-203b-4ce3-bf6e-acea9403df2d", "83b4bc108ec947cd8ecf7799afafdad2")]
    public class LuisRootDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Je n'ai pas compris ce que vous avez dit.");
        }

        [LuisIntent("Commander")]
        public async Task Commander(IDialogContext context, LuisResult result)
        {
            var form = OrderForm.ReadFromLuis(result);

            var commands = new List<KeyValuePair<int, string>>();
            
            foreach (var count in form.Counts)
            {
                foreach (var item in form.Items)
                {
                    commands.Add(new KeyValuePair<int, string>(count, item));
                }
            }

            var foodAndQuantities = form.Counts.Zip(form.Items, (q, i) => new { Quantity = q, Item = i });

            string resume = String.Join(", ", foodAndQuantities.Select(x => "x" + x.Quantity + " " + x.Item));

            await context.PostAsync($"Vous avez choisi : {resume}");
        }
    }
}