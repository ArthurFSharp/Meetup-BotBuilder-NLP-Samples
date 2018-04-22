using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Bot.Builder.Dialogs;
using System.Configuration;
using System.Web.Http;

namespace RestaurantBot
{
    public static class AutofacConfig
    {
        public static void Register()
        {
            Conversation.UpdateContainer(builder =>
            {
                builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

                RegisterAppInsights(builder);
            });

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Conversation.Container);
        }

        private static void RegisterAppInsights(ContainerBuilder builder)
        {
            builder.Register(c => new TelemetryClient(new TelemetryConfiguration(ConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"])));
        }
    }
}