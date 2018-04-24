using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Framework.Builder.Witai;
using Microsoft.Bot.Framework.Builder.Witai.Extensions;
using RestaurantBot.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBot.Dialogs.Forms
{
    public static class DateTimeRangeWithPrecisionValidator
    {
        public static async Task<ValidateResult> ValidateDate(ICustomForm state, object value)
        {
            var result = await WitRequestFromString.ToWitResult(Convert.ToString(value));

            if (result.TryFindEntities(WitBuiltinEntities.DateTime, out var dateTimeEntities))
            {
                var dates = DateTimeRangeWithPrecisionHelper.GetDatesFromWitEntities(dateTimeEntities);

                var date = dates.First();

                return new ValidateResult
                {
                    IsValid = true,
                    Value = new DateTimeA(date.StartDate)
                };
            }

            return new ValidateResult
            {
                IsValid = false,
                Feedback = "Veuillez saisir une date valide."
            };
        }
    }
}