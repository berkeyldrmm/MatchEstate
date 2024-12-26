using BusinessLayer.Abstract;
using DataAccessLayer;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentDal _apartmentRepository;

        public ApartmentService(IApartmentDal apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public bool DeleteAsync(Apartment item)
        {
            return _apartmentRepository.Delete(item);
        }

        public Task<IEnumerable<Apartment>> GetAll()
        {
            return _apartmentRepository.ReadAll();
        }

        public Task<Apartment> GetOne(string id)
        {
            return _apartmentRepository.Read(id);
        }

        public async Task<bool> Insert(Apartment item)
        {
            return await _apartmentRepository.Insert(item);
        }

        public Task<bool> Update(Apartment item)
        {
            return _apartmentRepository.Update(item);
        }
    }
}
