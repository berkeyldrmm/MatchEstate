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
    public class ShopService : IPropertyService<Shop>
    {
        private readonly IShopRepository _shopRepository;

        public ShopService(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<bool> AddProperty(Shop shop)
        {
            return await _shopRepository.Insert(shop);
        }

        public async Task<bool> UpdateProperty(Shop shop)
        {
            return await _shopRepository.Update(shop);
        }
    }
}
