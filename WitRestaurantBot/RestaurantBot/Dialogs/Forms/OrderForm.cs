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
            var form = new OrderForm();

            if (witResult.TryFindEntity("food", out var foodEntity))
            {
                form.Item = foodEntity.Value;
            }

            if (witResult.TryFindEntity("wit/number", out var itemCount))
            {
                form.Count = int.Parse(itemCount.Value);
            }
            else
            {
                form.Count = 1;
            }
            
            if (witResult.TryFindEntity("wit/datetime", out var dateTimeEntity) && DateTimeParser.TryParse(dateTimeEntity, out var range))
            {
                form.DeliveryDate = range.StartDate.DateTime;
            }

            return form;
        }
    }
}