using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Factory;
using BusinessLayer.Concrete;
using BusinessLayer.Concrete.Factory;
using BusinessLayer.Concrete.PropertyServices;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Entities;
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
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IMatchingService, MatchingService>();
            services.AddScoped<IPropertyStatusService, PropertyStatusService>();
            services.AddScoped<IImageService, ImageService>();

            services.AddScoped<IPropertyService<Shop>, ShopService>();
            services.AddScoped<IPropertyService<Apartment>, ApartmentService>();
            services.AddScoped<IPropertyService<CommercialUnit>, CommercialUnitService>();
            services.AddScoped<IPropertyService<Farmland>, FarmlandService>();
            services.AddScoped<IPropertyService<Land>, LandService>();
            
            services.AddScoped<IPropertyServiceFactory, PropertyServiceFactory>();

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
