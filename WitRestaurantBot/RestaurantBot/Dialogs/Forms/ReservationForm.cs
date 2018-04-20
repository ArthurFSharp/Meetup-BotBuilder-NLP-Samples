using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Framework.Builder.Witai.Extensions;
using Microsoft.Bot.Framework.Builder.Witai.Models;
using Microsoft.Bot.Framework.Builder.Witai.Parsers;
using System;
using System.Linq;

namespace RestaurantBot.Dialogs.Forms
{
    public class ReservationForm
    {
        public string RestaurantName { get; set; }
        public int? PeopleCount { get; set; }
        public DateTime? ReservationDate { get; set; }

        public static ReservationForm ReadFromWit(WitResult witResult)
        {
            var form = new ReservationForm();

            if (witResult.TryFindEntity("restaurantname", out var restaurantNameEntity))
            {
                form.RestaurantName = restaurantNameEntity.Value;
            }
            
            if (witResult.TryFindEntities("wit/number", out var countEntities))
            {
                var countEntity = countEntities.Skip(1).FirstOrDefault();
                if (countEntity != null)
                {
                    form.PeopleCount = int.Parse(countEntity.Value);
                }
            }

            if (witResult.TryFindEntity("wit/datetime", out var dateTimeEntity) && DateTimeParser.TryParse(dateTimeEntity, out var range))
            {
                form.ReservationDate = range.StartDate.DateTime;
            }

            return form;
        }
    }
}