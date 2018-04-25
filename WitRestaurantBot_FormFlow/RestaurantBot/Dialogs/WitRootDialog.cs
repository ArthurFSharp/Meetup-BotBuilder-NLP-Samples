using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Framework.Builder.Witai;
using Microsoft.Bot.Framework.Builder.Witai.Dialogs;
using Microsoft.Bot.Framework.Builder.Witai.Models;
using RestaurantBot.Dialogs.Forms;
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
            await context.PostAsync("Je n'ai pas compris ce que vous avez dit.");
        }

        #region Commander

        [WitIntent("Commander")]
        public async Task Commander(IDialogContext context, WitResult result)
        {
            var form = OrderForm.ReadFromWit(result);

            if (string.IsNullOrWhiteSpace(form.Item) || form.Count < 1 || form.DeliveryDate == null)
            {
                var setLeavesForm = new FormDialog<OrderForm>(form, OrderForm.BuildForm, FormOptions.PromptInStart);
                context.Call(setLeavesForm, ResumeAfterOrderFormCompleted);

                return;
            }

            await ResumeOrder(context, form);
        }

        private async Task ResumeAfterOrderFormCompleted(IDialogContext context, IAwaitable<OrderForm> result)
        {
            try
            {
                var order = await result;
                await ResumeOrder(context, order);
            }
            catch (FormCanceledException<OrderForm>)
            {
                await context.PostAsync("Une erreur s'est produite dans le formulaire.");
            }
        }

        private async Task ResumeOrder(IDialogContext context, OrderForm order)
        {
            await context.PostAsync($"Vous avez commandé {order.Count}x {order.Item}");
            await context.PostAsync($"Vous serez livré à partir de {order.DeliveryDate.DateTime.ToString("HH\\hmm")} le {order.DeliveryDate.DateTime.ToString("dd/MM/yyyy")}");
        }

        #endregion

        #region Réserver

        [WitIntent("Reserver")]
        public async Task Reserver(IDialogContext context, WitResult result)
        {
            var form = ReservationForm.ReadFromWit(result);

            if (string.IsNullOrWhiteSpace(form.RestaurantName) || form.PeopleCount == 0 || form.ReservationDate == null)
            {
                var reservationForm = new FormDialog<ReservationForm>(form, ReservationForm.BuildForm, FormOptions.PromptInStart);
                context.Call(reservationForm, ResumeAfterReservationFormCompleted);

                return;
            }

            await ResumeReservation(context, form);
        }

        private async Task ResumeAfterReservationFormCompleted(IDialogContext context, IAwaitable<ReservationForm> result)
        {
            try
            {
                var reservation = await result;
                await ResumeReservation(context, reservation);
            }
            catch (FormCanceledException<ReservationForm>) 
            {
                await context.PostAsync("Une erreur s'est produite dans le formulaire.");
            }
        }

        private async Task ResumeReservation(IDialogContext context, ReservationForm reservation)
        {
            await context.PostAsync($"Je vous ai réservé une table pour {reservation.PeopleCount} chez {reservation.RestaurantName} à {reservation.ReservationDate.DateTime.ToString("HH\\hmm")} le {reservation.ReservationDate.DateTime.ToString("dd/MM/yyyy")}");
        }

        #endregion
    }
}