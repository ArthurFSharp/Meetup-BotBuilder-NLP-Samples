using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using System.Linq;

namespace RestaurantBot.Services.FormFlow
{
    public static class FormFlowExtensions
    {
        public static IFormBuilder<T> DateTimeRangeField<T>(this IFormBuilder<T> formBuilder, string name, ActiveDelegate<T> active = null, ValidateAsyncDelegate<T> validate = null)
            where T : class
        {
            var prompt = typeof(T).GetProperty(name).GetCustomAttributes(typeof(PromptAttribute), false).Cast<PromptAttribute>().First();

            return formBuilder.Field(new FieldReflector<T>(name)
                                            .SetType(typeof(string))
                                            .SetPrompt(prompt)
                                            .SetFieldDescription(name)
                                            .SetActive(active)
                                            .SetValidate(validate));
        }
    }
}