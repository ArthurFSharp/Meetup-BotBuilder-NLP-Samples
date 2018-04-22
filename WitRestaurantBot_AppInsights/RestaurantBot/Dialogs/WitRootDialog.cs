using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Framework.Builder.Witai;
using Microsoft.Bot.Framework.Builder.Witai.Dialogs;
using Microsoft.Bot.Framework.Builder.Witai.Models;
using RestaurantBot.Dialogs.Forms;
using RestaurantBot.Telemetry;
using System;
using System.Threading.Tasks;

namespace RestaurantBot.Dialogs
{
    [Serializable]
    [WitModel("FEANTFQTZWMMEM3NXU7QSL2AH7ZRJ62A")]
    public class WitRootDialog : WitDialog<object>
    {
        [WitIntent("")]
        [WitIntent("None")]
        public async Task None(IDialogContext context, WitResult result)
        {
            var telemetryService = Conversation.Container.Resolve<ITelemetryService>();
            telemetryService.UnrecognizedIntent(context.Activity.ChannelId, context.Activity.From.Id, context.Activity.AsMessageActivity().Text, 0.0);

            await context.PostAsync("Je n'ai pas compris ce que vous avez dit.");
        }

        [WitIntent("Commander")]
        public async Task Commander(IDialogContext context, WitResult result)
        {
            var telemetryService = Conversation.Container.Resolve<ITelemetryService>();
            telemetryService.OrderAsked(context.Activity.ChannelId, context.Activity.From.Id, context.Activity.AsMessageActivity().Text, 0.0);

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
            var telemetryService = Conversation.Container.Resolve<ITelemetryService>();
            telemetryService.ReservationAsked(context.Activity.ChannelId, context.Activity.From.Id, context.Activity.AsMessageActivity().Text, 0.0);

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