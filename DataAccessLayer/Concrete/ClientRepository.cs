using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Client;

namespace DataAccessLayer.Concrete
{
    public class ClientRepository : GenericRepository<Client, string>, IClientRepository
    {
        public ClientRepository(MatchEstateDbContext context) : base(context)
        {
        }
        public IQueryable<Client> EntityOfUser(string userId) => Entity.Where(d => d.UserId == userId);

        public async Task<IEnumerable<Client>> GetClientsOfUser(string userId)
        {
            return await EntityOfUser(userId).ToListAsync();
        }

        public IEnumerable<Client> GetRange(string userId, IEnumerable<string> Ids)
        {
            return EntityOfUser(userId).Where(client => Ids.Contains(client.Id)).ToList();
        }

        public IEnumerable<ClientPageDTO> SearchClient(string userId, string search)
        {
            var clientQuery = EntityOfUser(userId);
            if (search != null)
            {
                clientQuery = clientQuery.Where(client => client.NameSurname.Contains(search) || client.PhoneNumber.Contains(search));
            }
            IEnumerable<ClientPageDTO> clients = clientQuery.Select(c => new ClientPageDTO
            {
                Id = c.Id,
                NameSurname = c.NameSurname,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            });

            return clients;
        }

        public async Task<bool> ControlUserPhoneNumber(string userId, string phoneNumber)
        {
            Client? client = await EntityOfUser(userId).FirstOrDefaultAsync(c=>c.PhoneNumber == phoneNumber);
            return client == null;
        }
    }
}
