using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;

namespace RestaurantBot.Dialogs.Forms
{
    public class OrderForm
    {
        public string Item { get; set; }
        public int? Count { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public static OrderForm ReadFromLuis(LuisResult luisResult)
        {
            var form = new OrderForm();

            if (luisResult.TryFindEntity("Food", out var foodEntity))
            {
                form.Item = foodEntity.Entity;
            }

            if (luisResult.TryFindEntity("Count", out var itemCount))
            {
                form.Count = int.Parse(itemCount.Entity);
            }

            EntityRecommendation dateTimeEntity;
            if (luisResult.TryFindEntity("builtin.datetimeV2.datetimerange", out dateTimeEntity) ||
                luisResult.TryFindEntity("builtin.datetimeV2.datetime", out dateTimeEntity))
            {
                form.DeliveryDate = LuisHelper.ResolveDateTime(dateTimeEntity);
            }

            return form;
        }
    }
}