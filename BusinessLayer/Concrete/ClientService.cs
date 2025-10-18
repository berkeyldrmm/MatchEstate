using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using Shared.Dtos.Client;

namespace BusinessLayer.Concrete
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
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

        public async Task<IEnumerable<ClientPageDTO>> GetClientsOfUser(string userId)
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
