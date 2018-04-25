using Microsoft.Bot.Framework.Builder.Witai;
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

            if (witResult.TryFindEntity("people_count", out var countEntity))
            {
                form.PeopleCount = int.Parse(countEntity.Value);
            }

            if (witResult.TryFindEntity(WitBuiltinEntities.DateTime, out var dateTimeEntity) && DateTimeParser.TryParse(dateTimeEntity, out var range))
            {
                form.ReservationDate = range.StartDate.DateTime;
            }

            return form;
        }
    }
}