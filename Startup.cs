using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SwiftWheels.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {


        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer("Server=CANDAN\\SQLEXPRESS01;Database=SwiftWheelsDB;Trusted_Connection=True;"));


        services.AddMvc(); // MVC yapısını ekliyoruz
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseStaticFiles();
        app.UseAuthentication(); // app.UseRouting()'ten sonra, app.UseAuthorization()'dan önce
        app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=Account}/{action=Login}/{id?}");
        });
        

    }
}
