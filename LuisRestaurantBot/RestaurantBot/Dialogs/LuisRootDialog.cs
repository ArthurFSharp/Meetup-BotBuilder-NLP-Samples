using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using RestaurantBot.Dialogs.Forms;
using System;
using System.Threading.Tasks;

namespace RestaurantBot.Dialogs
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

            if (string.IsNullOrWhiteSpace(form.Item) || form.Count < 1 || !form.DeliveryDate.HasValue)
            {
                await context.PostAsync("Des informations sont manquantes");
                return;
            }

            await context.PostAsync($"Vous avez commandé {form.Count}x {form.Item}");
            await context.PostAsync($"Vous serez livré à partir de {form.DeliveryDate.Value.ToString("HH\\hmm")} le {form.DeliveryDate.Value.ToString("dd/MM/yyyy")}");
        }

        [LuisIntent("Reserver")]
        public async Task Reserver(IDialogContext context, LuisResult result)
        {
            var form = ReservationForm.ReadFromLuis(result);

            if (string.IsNullOrWhiteSpace(form.RestaurantName) || !form.PeopleCount.HasValue || !form.ReservationDate.HasValue)
            {
                await context.PostAsync("Des informations sont manquantes");
                return;
            }

            await context.PostAsync($"Je vous ai réservé une table pour {form.PeopleCount.Value} chez {form.RestaurantName} à {form.ReservationDate.Value.ToString("HH\\hmm")} le {form.ReservationDate.Value.ToString("dd/MM/yyyy")}");
        }
    }
}