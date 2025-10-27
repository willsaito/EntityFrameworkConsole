using ConsoleApp1.Models;
using ConsoleApp1.Data;
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
        public async Task AddAsync(Item item)
        {
            context.Items.Add(item);
            await context.SaveChangesAsync();
        }
        public async Task DeleteByIdAsync(int id)
        {
            var item = GetById(id);
            if (item is not null)
            {
                context.Items.Remove(item);
                await context.SaveChangesAsync();

            }
        }
        public Item GetById(int id)
        {
            var item = context.Items.FirstOrDefault(e => e.Id == id);
            return item;
        }
        public List<Item> GetAll()
        {
            var items = context.Items.ToList();
            return items;
        }
        public async Task UpdateAsync(Item item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
      
    }

}
