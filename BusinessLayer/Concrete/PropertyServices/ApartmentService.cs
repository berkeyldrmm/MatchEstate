using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.PropertyServices
{
    public class ApartmentService : IPropertyService<Apartment>
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentService(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<bool> AddProperty(Apartment apartment)
        {
            return await _apartmentRepository.Insert(apartment);
        }

        public async Task<bool> UpdateProperty(Apartment property)
        {
            return await _apartmentRepository.Update(property);
        }
    }
}
