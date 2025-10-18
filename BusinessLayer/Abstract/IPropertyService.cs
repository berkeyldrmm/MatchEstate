using EntityLayer.Abstract;

namespace BusinessLayer.Abstract
{
    public interface IPropertyService<TProperty> where TProperty : IProperty
    {
        public Task<bool> AddProperty(TProperty property);
        public Task<bool> UpdateProperty(TProperty property);
    }
}
