using System;
using Calendarro.Areas.Identity.Data;
using Calendarro.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Calendarro.Areas.Identity.IdentityHostingStartup))]
namespace Calendarro.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<LoginContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("LoginContextConnection")));

                services.AddDefaultIdentity<CalendarroUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<LoginContext>();
            });
        }
    }
}