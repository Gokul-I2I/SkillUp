using SkillUp.Pages.Login;

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
            builder.Services.AddHttpClient<LoginModel>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    policy => policy.WithOrigins("https://localhost:7239")
                                     .AllowAnyMethod()
                                     .AllowAnyHeader());
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();  // Ensure authentication middleware is used
            app.UseAuthorization();

            // Default route should map to login page
            app.MapGet("/", async context =>
            {
                context.Response.Redirect("Login/Login");
            });

            // Map Razor Pages
            app.MapRazorPages();
            app.Run();
        }
    }
}
