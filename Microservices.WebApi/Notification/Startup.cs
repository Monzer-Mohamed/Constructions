using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ;
using RabbitMQEnhanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR();
            services.AddSingleton<IEmailServices,EmailServices>();

            //services.SendMessage<NotificationMessage>();

            services.AddRabbitMQ("Notification", () =>
            {
                CofigurationSettingModel cofigurationSettingModel = new CofigurationSettingModel();
                Configuration.Bind("RabbitMQ", cofigurationSettingModel);
                return cofigurationSettingModel;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notify");
            });

            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            IEmailServices emailServices  = app.ApplicationServices.GetRequiredService<IEmailServices>();
            IHubContext<NotificationHub> hubContext = app.ApplicationServices.GetRequiredService<IHubContext<NotificationHub>>();
            eventBus.Subscribe<NotificationMessage>((model) =>
                {
                    return emailServices.sendEmail(model);
                    //return hubContext.Clients.All.SendAsync("ReceiveMessage", model);
                }
            );
        }
    }
}
