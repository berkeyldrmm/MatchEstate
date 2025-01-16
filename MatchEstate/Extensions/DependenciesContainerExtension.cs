using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DTOLayer;

namespace RealEstate.Extensions
{
    public static class DependenciesContainerExtension
    {
        public static void DependenciesContainer(this IServiceCollection services)
        {
            services.AddScoped<IListingService, ListingService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IMatchingService, MatchingService>();

            services.AddScoped<IClientDal, ClientRepository>();
            services.AddScoped<ILandDal, LandRepository>();
            services.AddScoped<IApartmentDal, ApartmentRepository>();
            services.AddScoped<ICommercialUnitDal, CommercialUnitRepository>();
            services.AddScoped<IShopDal, ShopRepository>();
            services.AddScoped<IListingDal, ListingRepository>();
            services.AddScoped<IClientDal, ClientRepository>();
            services.AddScoped<IRequestDal, RequestRepository>();
            services.AddScoped<IFarmlandDal, FarmlandRepository>();
            services.AddScoped<IStatisticsDal, StatisticsRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
