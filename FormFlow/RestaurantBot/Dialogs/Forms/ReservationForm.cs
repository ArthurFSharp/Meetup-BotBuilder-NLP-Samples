using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Framework.Builder.Witai;
using Microsoft.Bot.Framework.Builder.Witai.Extensions;
using Microsoft.Bot.Framework.Builder.Witai.Models;
using Microsoft.Bot.Framework.Builder.Witai.Parsers;
using RestaurantBot.Services.FormFlow;
using System;
using System.Linq;

namespace RestaurantBot.Dialogs.Forms
{
    [Serializable]
    public class ReservationForm : ICustomForm
    {
        [Prompt("Dans quel restaurant souhaitez vous manger ?")]
        public string RestaurantName { get; set; }

        [Prompt("Pour combien de personnes ?")]
        public int PeopleCount { get; set; } = 0;

        [Prompt("Indiquez moi une date : ")]
        public DateTimeA ReservationDate { get; set; }

        public static ReservationForm ReadFromWit(WitResult witResult)
        {
            var form = new ReservationForm();

            if (witResult.TryFindEntity("restaurantname", out var restaurantNameEntity))
            {
                form.RestaurantName = restaurantNameEntity.Value;
            }

            if (witResult.TryFindEntities("number", out var countEntities))
            {
                var countEntity = countEntities.Skip(1).FirstOrDefault();
                if (countEntity != null)
                {
                    form.PeopleCount = int.Parse(countEntity.Value);
                }
            }

            if (witResult.TryFindEntity(WitBuiltinEntities.DateTime, out var dateTimeEntity) && DateTimeParser.TryParse(dateTimeEntity, out var range))
            {
                form.ReservationDate = new DateTimeA(range.StartDate.DateTime);
            }

            return form;
        }

        public static IForm<ReservationForm> BuildForm()
        {
            var form = new FormBuilder<ReservationForm>()
                .Field(nameof(ReservationForm.RestaurantName), active: HasNotRestaurantName)
                .Field(nameof(ReservationForm.PeopleCount), active: HasNotClientQuantity)
                .DateTimeRangeField(nameof(ReservationForm.ReservationDate), active: HasNotFoundDate, validate: DateTimeRangeWithPrecisionValidator.ValidateDate)
                .Build();

            return form;
        }

        private static bool HasNotRestaurantName(ReservationForm form) => string.IsNullOrEmpty(form.RestaurantName);

        private static bool HasNotClientQuantity(ReservationForm form) => form.PeopleCount == 0;

        private static bool HasNotFoundDate(ReservationForm form) => form.ReservationDate == null;

    }
}