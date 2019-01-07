using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecordKeeper.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Authorization;


[assembly: HostingStartup(typeof(RecordKeeper.Areas.Identity.IdentityHostingStartup))]
namespace RecordKeeper.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RecordKeeperIdentityDbContext>(options =>
                    options.UseSqlite("Data SOurce=RecordKeeper.db"));
                        //context.Configuration.GetConnectionString("RecordKeeperIdentityDbContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<RecordKeeperIdentityDbContext>();
                //services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
                //    .AddEntityFrameworkStores<RecordKeeperIdentityDbContext>();
            });
        }
    }
}