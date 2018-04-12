using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Bot.Builder.Dialogs;
using System.Web.Http;

namespace BotLuisMeetup
{
    public static class AutofacConfig
    {
        public static void Register()
        {
            Conversation.UpdateContainer(builder =>
            {
                builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

                RegisterConfiguration(builder);
            });

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Conversation.Container);
        }

        private static void RegisterConfiguration(ContainerBuilder builder)
        {

        }
    }
}