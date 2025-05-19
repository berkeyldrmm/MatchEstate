using EntityLayer.Entities;
using Shared.Dtos.Client;

namespace BusinessLayer.Abstract
{
    public interface IClientService : IGenericService<Client, string>
    {
        public Task<IEnumerable<Client>> GetClientsOfUser(string userId);
        void DeleteRange(string userId, IEnumerable<string> Ids);
        public IEnumerable<ClientPageDTO> SearchClient(string userId, string search);
    }
}
