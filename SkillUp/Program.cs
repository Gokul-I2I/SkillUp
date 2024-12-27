using SkillUp.Pages.Login;
using SkillUp.Services;

namespace SkillUp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages().AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = true;
    });
            builder.Services.AddHttpClient();
            builder.Services.AddControllers();
            builder.Services.AddHttpClient<ApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7202/"); 
            });

            builder.Services.AddHttpClient<LoginModel>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });
            var app = builder.Build();

            app.UseCors("AllowAll");
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // Default route should map to login page
            app.MapGet("/", async context =>
            {
                context.Response.Redirect("Login/login");
            });

            // Map Razor Pages
            app.MapRazorPages();
            app.Run();
        }
    }
}