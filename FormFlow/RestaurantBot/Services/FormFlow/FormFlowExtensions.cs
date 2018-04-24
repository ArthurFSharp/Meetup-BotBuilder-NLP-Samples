using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;

namespace RestaurantBot.Services.FormFlow
{
    public static class FormFlowExtensions
    {
        public static IFormBuilder<T> DateTimeRangeField<T>(this IFormBuilder<T> formBuilder, string name, ActiveDelegate<T> active = null, ValidateAsyncDelegate<T> validate = null)
            where T : class
        {
            return formBuilder.Field(new FieldReflector<T>(name)
                                            .SetType(typeof(string))
                                            .SetFieldDescription(name)
                                            .SetActive(active)
                                            .SetValidate(validate));
        }
    }
}