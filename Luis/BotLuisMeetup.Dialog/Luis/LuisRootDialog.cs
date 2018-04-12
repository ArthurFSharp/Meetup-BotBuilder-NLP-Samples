using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

namespace BotLuisMeetup.Dialog.Luis
{
    [Serializable]
    [LuisModel("4644cc35-203b-4ce3-bf6e-acea9403df2d", "83b4bc108ec947cd8ecf7799afafdad2")]
    public partial class LuisRootDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Je ne comprend pas ce que vous dites.");
        }
    }
}
