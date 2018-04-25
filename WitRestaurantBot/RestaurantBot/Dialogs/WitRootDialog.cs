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
    [WitModel("<WIT-API-KEY>")]
    public class WitRootDialog : WitDialog<object>
    {
        [WitIntent("")]
        [WitIntent("None")]
        public async Task None(IDialogContext context, WitResult result)
        {
            await context.PostAsync("Je n'ai pas compris ce que vous avez dit.");
        }
    }
}