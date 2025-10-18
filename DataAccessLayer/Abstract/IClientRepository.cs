using EntityLayer.Entities;
using Shared.Dtos.Client;

namespace DataAccessLayer.Abstract
{
    public interface IClientRepository : IGenericRepository<Client, string>
    {
        public Task<IEnumerable<ClientPageDTO>> GetClientsOfUser(string userId);
        IEnumerable<Client> GetRange(string userId, IEnumerable<string> Ids);
        public IEnumerable<ClientPageDTO> SearchClient(string userId, string search);
        public Task<bool> ControlUserPhoneNumber(string userId, string phoneNumber);

    }
}
