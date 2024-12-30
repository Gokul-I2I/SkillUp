using Microsoft.EntityFrameworkCore;
using skillup.Api.Service;
using SkillUpBackend.Mapper;
using SkillUpBackend.Repository;
using SkillUpBackend.Service;
using SkillUpBackend.Utils;


namespace SkillUpBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddRazorPages();
            builder.Services.AddHttpClient();


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddRazorPages();


            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<PasswordHelper>();
            builder.Services.AddScoped<IMentorService, MentorService>();
            builder.Services.AddScoped<IMentorRepository, MentorRepository>();

            builder.Services.AddScoped<IUserSubtopicService, UserSubtopicService>();
            builder.Services.AddScoped<IBatchRepository, BatchRepository>();
            builder.Services.AddScoped<IBatchService, BatchService>();
            builder.Services.AddScoped<IStreamRepository, StreamRepository>();
            builder.Services.AddScoped<IStreamService, StreamService>();
            builder.Services.AddScoped<BatchMapper>();
            builder.Services.AddScoped<UserMapper>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapRazorPages();
            app.Run();
        }
    }
}
