using Microsoft.Bot.Framework.Builder.Witai;
using Microsoft.Bot.Framework.Builder.Witai.Extensions;
using Microsoft.Bot.Framework.Builder.Witai.Models;
using Microsoft.Bot.Framework.Builder.Witai.Parsers;
using System;

namespace RestaurantBot.Dialogs.Forms
{
    public class OrderForm
    {
        public string Item { get; set; }
        public int Count { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public static OrderForm ReadFromWit(WitResult witResult)
        {
            throw new NotImplementedException();
        }
    }
}