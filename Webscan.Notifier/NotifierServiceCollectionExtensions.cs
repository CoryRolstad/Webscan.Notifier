using Microsoft.Extensions.DependencyInjection;
using System;

namespace Webscan.Notifier
{
    public static class NotifierServiceCollectionExtensions
    {
        public static IServiceCollection AddNotifierService(this IServiceCollection services, NotifierSettings notifierSettings)
        {
            ValidateSettings(services, notifierSettings);

            //services.AddSingleton<NotifierSettings>(notifierSettings);
            services.Configure<NotifierSettings>("NotifierSettings", settings => {
                settings.Port = notifierSettings.Port;
                settings.UserName = notifierSettings.UserName;
                settings.Password = notifierSettings.Password;
                settings.Sender = notifierSettings.Sender;
                settings.SmtpServer = notifierSettings.SmtpServer;
            }); 
            services.AddTransient<INotifierService, NotifierService>();
            return services; 
        }

        private static void ValidateSettings(IServiceCollection services, NotifierSettings notifierSettings)
        {
            if (services == null) throw new ArgumentNullException($"{nameof(services)} cannot be null");
            if (notifierSettings == null) throw new ArgumentNullException($"{nameof(notifierSettings)} cannot be null");   
            if (string.IsNullOrWhiteSpace(notifierSettings.UserName)) throw new ArgumentNullException($"{nameof(notifierSettings.UserName)} cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(notifierSettings.Password)) throw new ArgumentNullException($"{nameof(notifierSettings.Password)} cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(notifierSettings.Sender)) throw new ArgumentNullException($"{nameof(notifierSettings.Sender)} cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(notifierSettings.SmtpServer)) throw new ArgumentNullException($"{nameof(notifierSettings.SmtpServer)} cannot be null or whitespace.");
            if (notifierSettings.Port == 0) throw new ArgumentNullException($"{nameof(notifierSettings.Port)} cannot be 0.");
        }
    }
}
