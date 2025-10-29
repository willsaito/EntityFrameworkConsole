using ConsoleApp1.Data;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ClientRepository
    {
        private TestContext context;
        public ClientRepository()
        {
            context = new TestContext();
        }
        public async Task AddAsync(Client client)
        {
            context.Clients.Add(client);
            await context.SaveChangesAsync();
        }
        public async Task DeleteByIdAsync(int id)
        {
            var client = GetById(id);
            if (client is not null)
            {
                context.Clients.Remove(client);
                await context.SaveChangesAsync();

            }
        }
        public Client GetById(int id)
        {
            var client = context.Clients.FirstOrDefault(e => e.Id == id);
            return client;
        }
        public List<Client> GetAll()
        {
            var clients = context.Clients.ToList();
            return clients;
        }
        public async Task UpdateAsync(Client clients)
        {
            context.Entry(clients).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
