using BusinessLayer.Abstract;
using DataAccessLayer;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DTOLayer;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public bool ControlUserPhoneNumber(string userId, string phoneNumber)
        {
            return _clientRepository.ControlUserPhoneNumber(userId, phoneNumber);
        }

        public void DeleteRange(string userId, IEnumerable<string> Ids)
        {
            var items = _clientRepository.GetRange(userId, Ids);
            _clientRepository.DeleteRange(items);
        }

        public Task<IEnumerable<Client>> GetAll()
        {
            return _clientRepository.ReadAll();
        }

        public async Task<IEnumerable<Client>> GetClientsOfUser(string userId)
        {
            return await _clientRepository.GetClientsOfUser(userId);
        }

        public Task<Client> GetOne(string id)
        {
            return _clientRepository.Read(id);
        }

        public IEnumerable<ClientPageDTO> SearchClient(string userId, string search)
        {
            return _clientRepository.SearchClient(userId, search);
        }
    }
}
