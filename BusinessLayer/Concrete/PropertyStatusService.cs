using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;

namespace BusinessLayer.Concrete;

public class PropertyStatusService : IPropertyStatusService
{
    private readonly IPropertyStatusRepository _propertyStatusRepository;

    public PropertyStatusService(IPropertyStatusRepository propertyStatusRepository)
    {
        _propertyStatusRepository = propertyStatusRepository;
    }
    public Task<IEnumerable<PropertyStatus>> GetAll()
    {
        return _propertyStatusRepository.ReadAll();
    }

    public Task<PropertyStatus> GetOne(int id)
    {
        return _propertyStatusRepository.Read(id);
    }
}
