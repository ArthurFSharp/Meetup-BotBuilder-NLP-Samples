using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Framework.Builder.Witai;
using Microsoft.Bot.Framework.Builder.Witai.Extensions;
using Microsoft.Bot.Framework.Builder.Witai.Models;
using Microsoft.Bot.Framework.Builder.Witai.Parsers;
using RestaurantBot.Services.FormFlow;
using System;

namespace RestaurantBot.Dialogs.Forms
{
    [Serializable]
    public class DateTimeA
    {
        public DateTimeA(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public DateTime DateTime { get; set; }
    }

    [Serializable]
    public class OrderForm : ICustomForm
    {
        [Prompt("Qu'est ce que vous souhaitez manger ?")]
        public string Item { get; set; }

        [Prompt("Pour combien de personne ?")]
        public int Count { get; set; } = 0;

        [Prompt("Quand est-ce que vous souhaiteriez être servi ?")]
        public DateTimeA DeliveryDate { get; set; }

        public static OrderForm ReadFromWit(WitResult witResult)
        {
            var form = new OrderForm();

            if (witResult.TryFindEntity("food", out var foodEntity))
            {
                form.Item = foodEntity.Value;
            }

            if (witResult.TryFindEntity("number", out var itemCount))
            {
                form.Count = int.Parse(itemCount.Value);
            }

            if (witResult.TryFindEntity(WitBuiltinEntities.DateTime, out var dateTimeEntity) && DateTimeParser.TryParse(dateTimeEntity, out var range))
            {
                form.DeliveryDate = new DateTimeA(range.StartDate.DateTime);
            }

            return form;
        }

        #region Snippets

        //public static IForm<OrderForm> BuildForm()
        //{
        //    var form = new FormBuilder<OrderForm>()
        //        .Field(nameof(OrderForm.Item), active: HasNotFoundFoodItem)
        //        .Field(nameof(OrderForm.Count), active: HasNotFoundQuantity)
        //        .DateTimeRangeField(nameof(OrderForm.DeliveryDate), active: HasNotFoundDate, validate: DateTimeRangeWithPrecisionValidator.ValidateDate)
        //        .Build();

        //    return form;
        //}

        //private static bool HasNotFoundFoodItem(OrderForm form) => string.IsNullOrEmpty(form.Item);

        //private static bool HasNotFoundQuantity(OrderForm form) => form.Count == 0;

        //private static bool HasNotFoundDate(OrderForm form) => form.DeliveryDate == null;

        #endregion
    }
}