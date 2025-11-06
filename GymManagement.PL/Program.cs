using AutoMapper;
using GymManagement.BLL.AutoMapper;
using GymManagement.BLL.Interfaces;
using GymManagement.BLL.Services;
using GymManagement.DAL.Data.Context;
using GymManagement.DAL.Data.DataSeed;
using GymManagement.DAL.Repositories.Implementations;
using GymManagement.DAL.Repositories.Interfaces;
using GymManagement.PL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GymManagement.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("GymManagementConnectionString"))
                .UseLazyLoadingProxies();
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IMembershipService, MembershipService>();
            builder.Services.AddScoped<IMemberSessionService, MemberSessionService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(SessionMappingProfile).Assembly);
            });
            //builder.Services.AddAutoMapper(cfg => AppDomain.CurrentDomain.GetAssemblies());
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<GymDbContext>();
                context.Database.Migrate();
                GymDbContextDataSeeding.SeedData(context);
            }


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
            app.UseStaticFiles();
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
