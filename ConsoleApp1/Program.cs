using ConsoleApp1.Data;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Headers;

using TestContext context = new TestContext();

bool running = true;

void listItems()
{
    var items = context.Items;

    Console.WriteLine(string.Format("{0,4} {1,-25} {2,-35} {3}", ["ID","Name", "Description", "Weight"]));
    foreach (Item p in items)
    {
        Console.WriteLine(string.Format("{0,4} {1,-25} {2,-35} {3}", [p.Id, p.Name, p.Description, p.Weight]));
    }
}
string readLine()
{
    string input= Console.ReadLine();
    if (input == null | input == "")
    {
        while (input == null | input == "")
        {
            Console.WriteLine("Please insert a valid input");
            input = Console.ReadLine();
        }
    }
    return input;
}
async void insertItems()
{
    Console.WriteLine("Enter item name:\n");
    string name = readLine();
    Console.WriteLine("Enter item description:\n");
    string description = readLine();
    Console.WriteLine("Enter item weight:\n");
    int.TryParse(Console.ReadLine(), out int weight);

    Item newItem = new Item()
    {
        Name = name,
        Description = description,
        Weight = weight
    };
   await AddAsync(newItem);

}
async void editItem()
{
    Console.WriteLine("Enter item ID to edit it:\n");
    int id = int.Parse(readLine());
    try
    {
        Item item = GetById(id);
        Console.WriteLine($"Enter name:\nCurrent name: {item.Name}");
        string name = readLine();
        Console.WriteLine($"Enter item description:\nCurrent description: {item.Description}");
        string description = readLine();
        Console.WriteLine($"Enter item weight:\nCurrent weight: {item.Weight}");
        int.TryParse(Console.ReadLine(), out int weight);

        item.Name = name;
        item.Description = description;
        item.Weight = weight;

        await UpdateAsync(item);
   
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }

    
}
async Task UpdateAsync(Item item)
{
    context.Entry(item).State = EntityState.Modified;
    await context.SaveChangesAsync();
}
async void deleteItem()
{
    Console.WriteLine("Enter item ID to delete it:\n");
    int id = int.Parse(readLine());
    await DeleteByIdAsync(id);
}
async Task AddAsync(Item item)
{
    context.Items.Add(item);
   await context.SaveChangesAsync();
}
async Task DeleteByIdAsync(int id)
{
    var item = GetById(id);
    if (item is not null)
    {
        context.Items.Remove(item);
        await context.SaveChangesAsync();

    }
}
Item GetById(int id)
{
    var item =  context.Items.FirstOrDefault(e => e.Id == id);
    return item;
}
while (running)
{
    Console.WriteLine("Type: \nl: list items\nn: insert new item\ne: edit item\nd: delete item\nx: exit\n");
    string input = Console.ReadLine();

    switch (input) 
    {
        case "l":
            listItems();
            Console.WriteLine("\n");

            break;
        case "n":
            insertItems();
            Console.WriteLine("\n");

            break;
        case "x":
            running = false;
            break;
        case "d":
            deleteItem();
            break;
        case "e":
            editItem();
            break;
        default:
            Console.WriteLine("Invalid command");
            Console.WriteLine("\n");

            break;

    }

}