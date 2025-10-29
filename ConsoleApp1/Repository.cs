using ConsoleApp1.Data;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class Repository
    {
        private TestContext context;
        public Repository()
        {
            context = new TestContext();
        }
        public async Task AddAsync<T>(T dataObject) where T : class, IDbEntity
        {
            context.Set<T>().Add(dataObject);
            await context.SaveChangesAsync();
        }
        public async Task DeleteByIdAsync<T>(int id) where T : class, IDbEntity
        {
            var entity = context.Set<T>().FirstOrDefault(e => e.Id == id);
            if (entity is not null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

            }
        }
        public T GetById<T>(int id) where T : class, IDbEntity
        {
            var entity = context.Set<T>().FirstOrDefault(e => e.Id == id);
            return entity;
        }
        public List<Client> GetAll()
        {
            var client = context.Clients.ToList();
            return client;
        }
        public async Task UpdateAsync(Client client)
        {
            context.Entry(client).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
}
}
