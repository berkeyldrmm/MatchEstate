using DTOLayer;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IClientRepository : IGenericRepository<Client, string>
    {
        public Task<IEnumerable<Client>> GetClientsOfUser(string userId);
        IEnumerable<Client> GetRange(string userId, IEnumerable<string> Ids);
        public IEnumerable<ClientPageDTO> SearchClient(string userId, string search);
        public bool ControlUserPhoneNumber(string userId, string phoneNumber);

    }
}
