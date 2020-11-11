
Webscan.Notifier MicroService Library
----------------------------------------------
The Webscan.Notifier is a Microservice that allows you to send email notification.

Things to keep in mind
----------------------------------------------
You must wrap the calls in error handling.

Logging
--------
The library offers logging capabilities for debugging and troubleshooting. Debug level will offer application workflow and Trace will offer both application workflow and variable information. 

Configuration Format (Appsettings.json)
----------------------------------------
Use the Webscan.Notifier MicroService Settings format below within your appsettings Json

```
{
  ...
  "NotifierSettings": {
    "Sender": "me@gmail.com",
    "Reciever": "you@gmail.com",
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "UserName": "username",
    "Password": "password"
  },
  ...
}
```

Example of how to use the Webscan.Notifier MicroService Library
---------------------------------------
The Webscan.Notifier MicroService Library can be injected into your project by leveraging the extension method while passing in the configuration.
This example shows the configuration being pulled from the appsettings.json, but it can be pulled from any other configuration.
As long as it is an Action\<T\>

```
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ...
            //The only line you need to Add the Webscan.Notifier Service to your project.
            services.AddNotifierService(Configuration.GetSection("NotifierSettings").Get<NotifierSettings>());        
            ...
        }
```
The Webscan.Notifier MicroService Library is now in your DI Container and is fully configured and ready to be injected into your application and controllers
Here is an example of a controller utilizing the Webscan.Notifier MicroService library after injecting it with DI.

```
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Webscan.Notifier;

namespace ScannerDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INotifierService _notifierService; 

        public IndexModel(ILogger<IndexModel> logger, INotifierService notifierService)
        {
            _notifierService = notifierService;  
            _logger = logger;
        }

        public async void OnGet()
        {
           
        }
    }
}
```