using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleCRM.Contracts;
using SimpleCRM.DAL.Contracts;
using SimpleCRM.DAL.Implementations;
using SimpleCRM.Implementations;
using SimpleCRM.Profiles;

namespace SimpleCRM
{
	public class Startup
	{
		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<DataContext>(options =>
				options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")),
				ServiceLifetime.Transient);

			//services.AddAutoMapper(typeof(Startup));

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			var mapperConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MainProfile());
			});

			var mapper = mapperConfig.CreateMapper();

			services.AddSingleton(mapper);
			services.AddScoped<IDbRepository, DbRepository>();
			services.AddTransient<ITaskService, TaskService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			//app.UseCookiePolicy();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
