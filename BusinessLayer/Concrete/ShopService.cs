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
    public class ShopService : IShopService
    {
        private readonly IShopDal _shopRepository;

        public ShopService(IShopDal shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public bool DeleteAsync(Shop item)
        {
            return _shopRepository.Delete(item);
        }


        public Task<IEnumerable<Shop>> GetAll()
        {
            return _shopRepository.ReadAll();
        }

        public Task<Shop> GetOne(string id)
        {
            return _shopRepository.Read(id);
        }

        public async Task<bool> Insert(Shop item)
        {
            return await _shopRepository.Insert(item);
        }

        public Task<bool> Update(Shop item)
        {
            return _shopRepository.Update(item);
        }
    }
}
