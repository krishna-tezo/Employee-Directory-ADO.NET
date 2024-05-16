using EmployeeDirectory.Interfaces;
using EmployeeDirectory.UI.Menus;
using EmployeeDirectory.UI.UIServices;
using EmployeeDirectory.UI;
using Microsoft.Extensions.DependencyInjection;
using EmployeeDirectory.Services;
using EmployeeDirectory.Controllers;
using EmployeeDirectory.DATA;
using EmployeeDirectory.UI.Interfaces;
using EmployeeDirectory.UI.Controllers;
using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.Services.Services;

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
            services.AddSingleton<IJsonDataHandler, JsonDataHandler>();
            services.AddSingleton<IEmployeeDataService, EmployeeDataService>();
            services.AddSingleton<IRoleDataService, RoleDataService>();
            services.AddSingleton<IProjectDataService, ProjectDataService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<IProjectService, ProjectService>();
            services.AddSingleton<IUIService, UIService>();
            services.AddSingleton<IEmployeeMenu, EmployeeMenu>();
            services.AddSingleton<IRoleMenu, RoleMenu>();
            services.AddSingleton<IValidator, Validator>();
            services.AddSingleton<IEmployeeController, EmployeeController>();
            services.AddSingleton<IRoleController, RoleController>();
            services.AddSingleton<MainMenu>();

            return services.BuildServiceProvider();
        }
    }
}