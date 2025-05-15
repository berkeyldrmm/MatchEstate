using DTOLayer.Dtos;
using EntityLayer.Entities;

namespace BusinessLayer.Abstract
{
    public interface IClientService : IGenericService<Client, string>
    {
        public Task<IEnumerable<Client>> GetClientsOfUser(string userId);
        void DeleteRange(string userId, IEnumerable<string> Ids);
        public IEnumerable<ClientPageDTO> SearchClient(string userId, string search);
        public bool ControlUserPhoneNumber(string userId, string phoneNumber);
    }
}
