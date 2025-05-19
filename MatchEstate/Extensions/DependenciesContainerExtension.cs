using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Shared;

namespace RealEstate.Extensions
{
    public static class DependenciesContainerExtension
    {
        public static void DependenciesContainer(this IServiceCollection services)
        {
            services.AddScoped<IPropertyListingService, PropertyListingService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IPropertyRequestService, PropertyRequestService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IMatchingService, MatchingService>();
            services.AddScoped<IPropertyStatusService, PropertyStatusService>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ILandRepository, LandRepository>();
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<ICommercialUnitRepository, CommercialUnitRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IPropertyListingRepository, PropertyListingRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IPropertyRequestRepository, PropertyRequestRepository>();
            services.AddScoped<IFarmlandRepository, FarmlandRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();
            services.AddScoped<IPropertyStatusRepository, PropertyStatusRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
