using BotLuisMeetup.Dialog.Luis;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;

namespace BotLuisMeetup.Dialog
{
    [Serializable]
    public class RootDialog : IDialog<string>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;

            await context.PostAsync($"Bonjour {context.Activity.From.Name} !");
            await context.PostAsync($"Bienvenue à LuisPizza. Que puis-je faire pour vous ?");
            context.Call(new LuisRootDialog(), ResumeAfterDialog);
        }

        private async Task ResumeAfterDialog(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            context.Done(message);
        }

        //private async Task ResumeAfterDialog(IDialogContext context, IAwaitable<string> result)
        //{
        //    var message = await result;

        //    await context.PostAsync(message);
        //    context.Wait(MessageReceivedAsync);
        //}
    }
}
