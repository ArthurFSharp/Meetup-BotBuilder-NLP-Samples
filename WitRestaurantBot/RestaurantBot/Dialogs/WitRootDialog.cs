using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Framework.Builder.Witai;
using Microsoft.Bot.Framework.Builder.Witai.Dialogs;
using Microsoft.Bot.Framework.Builder.Witai.Models;
using RestaurantBot.Dialogs.Forms;
using System;
using System.Threading.Tasks;

namespace RestaurantBot.Dialogs
{
    [Serializable]
    [WitModel("4644cc35-203b-4ce3-bf6e-acea9403df2d")]
    public class WitRootDialog : WitDialog<object>
    {
        [WitIntent("")]
        [WitIntent("None")]
        public async Task None(IDialogContext context, WitResult result)
        {
            await context.PostAsync("Je n'ai pas compris ce que vous avez dit.");
        }
        
        [WitIntent("Commander")]
        public async Task Commander(IDialogContext context, WitResult result)
        {
            var form = OrderForm.ReadFromWit(result);

            if (string.IsNullOrWhiteSpace(form.Item) || form.Count < 1 || !form.DeliveryDate.HasValue)
            {
                await context.PostAsync("Des informations sont manquantes");
                return;
            }

            await context.PostAsync($"Vous avez commandé {form.Count}x {form.Item}");
            await context.PostAsync($"Vous serez livré à partir de {form.DeliveryDate.Value.ToString("HH\\hmm")} le {form.DeliveryDate.Value.ToString("dd/MM/yyyy")}");
        }

        [WitIntent("Reserver")]
        public async Task Reserver(IDialogContext context, WitResult result)
        {
            var form = ReservationForm.ReadFromWit(result);

            if (string.IsNullOrWhiteSpace(form.RestaurantName) || !form.PeopleCount.HasValue || !form.ReservationDate.HasValue)
            {
                await context.PostAsync("Des informations sont manquantes");
                return;
            }

            await context.PostAsync($"Je vous ai réservé une table pour {form.PeopleCount.Value} chez {form.RestaurantName} à {form.ReservationDate.Value.ToString("HH\\hmm")} le {form.ReservationDate.Value.ToString("dd/MM/yyyy")}");
        }
    }
}