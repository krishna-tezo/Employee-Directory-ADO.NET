using EmployeeDirectory.Interfaces;
using EmployeeDirectory.UI.Menus;
using EmployeeDirectory.UI.UIServices;
using EmployeeDirectory.UI;
using Microsoft.Extensions.DependencyInjection;
using EmployeeDirectory.Services;
using EmployeeDirectory.Controllers;
using EmployeeDirectory.UI.Interfaces;
using EmployeeDirectory.UI.Controllers;
using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.Services.Services;
using Microsoft.Extensions.Configuration;
using EmployeeDirectory.Data.Services;
using EmployeeDirectory.Data;

namespace EmployeeDirectory.Core
{
    public class StartupService
    {
        private IServiceCollection services;
        public StartupService(IServiceCollection services)
        {
            this.services = services;
           
        }

        public ServiceProvider Configure()
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string connectionString = configBuilder.GetSection("ConnectionStrings")["MyDBConnectionString"];
            if(connectionString != null)
            {
                services.AddScoped<IDbConnection> (db=> new DbConnection(connectionString));
            }
            else
            {
                throw new Exception("Error");
            }
            services.AddSingleton<IEmployeeDataService, EmployeeDataService>();
            services.AddSingleton<IRoleDataService, RoleDataService>();
            services.AddSingleton<IProjectDataService, ProjectDataService>();
            services.AddSingleton<ICommonDataService , CommonDataService>();

            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<IProjectService, ProjectService>();
            services.AddSingleton<IUIService, UIService>();
            services.AddSingleton<IEmployeeMenu, EmployeeMenu>();
            services.AddSingleton<IRoleMenu, RoleMenu>();
            services.AddSingleton<IValidator, Validator>();
            services.AddSingleton<IEmployeeController, EmployeeController>();
            services.AddSingleton<IRoleController, RoleController>();
            services.AddSingleton<IProjectController, ProjectController>();
            services.AddSingleton<MainMenu>();
            

            return services.BuildServiceProvider();
        }
    }
}