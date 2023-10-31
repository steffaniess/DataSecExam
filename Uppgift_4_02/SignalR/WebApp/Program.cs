namespace SignalR_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSignalR();

            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "OpenArms",
            //                      policy =>
            //                      {
            //                          policy.WithOrigins("http://localhost/", "https://localhost:7122")
            //                          .AllowAnyHeader()
            //                          .AllowAnyMethod()
            //                          .AllowCredentials();
            //                      });
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'none'; script-src 'self'; style-src 'self'; img-src 'self'; connect-src 'self' wss://localhost:7216/,com;");
                await next();
            });

            app.UseStaticFiles();

            app.Run();
        }
    }
}