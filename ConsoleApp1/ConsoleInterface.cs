using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ConsoleInterface
    {
        ItemRepository itemRepository = new ItemRepository();
        Repository repository = new Repository();
        public void ListItems()
        {
            var items = itemRepository.GetAll();

            Console.WriteLine(string.Format("{0,4} {1,-25} {2,-35} {3}", ["ID", "Name", "Description", "Weight"]));
            foreach (Item p in items)
            {
                Console.WriteLine(string.Format("{0,4} {1,-25} {2,-35} {3}", [p.Id, p.Name, p.Description, p.Weight]));
            }
        }
        public void ListClients()
        {

        }
        public string ReadLine()
        {
            string input = Console.ReadLine();
            if (input == null || input == "")
            {
                while (input == null || input == "")
                {
                    Console.WriteLine("Please insert a valid input");
                    input = Console.ReadLine();
                }
            }
            return input;
        }
        public async void InsertItems()
        {
            Console.WriteLine("Enter item name:\n");
            string name = ReadLine();
            Console.WriteLine("Enter item description:\n");
            string description = ReadLine();
            Console.WriteLine("Enter item weight:\n");
            int.TryParse(Console.ReadLine(), out int weight);

            Item newItem = new Item()
            {
                Name = name,
                Description = description,
                Weight = weight
            };
            await repository.AddAsync(newItem);

        }
        public async void EditItem()
        {
            Console.WriteLine("Enter item ID to edit it:\n");
            int id = int.Parse(ReadLine());
            try
            {
                Item item = itemRepository.GetById(id);
                Console.WriteLine($"Enter name:\nCurrent name: {item.Name}");
                string name = ReadLine();
                Console.WriteLine($"Enter item description:\nCurrent description: {item.Description}");
                string description = ReadLine();
                Console.WriteLine($"Enter item weight:\nCurrent weight: {item.Weight}");
                int.TryParse(Console.ReadLine(), out int weight);

                item.Name = name;
                item.Description = description;
                item.Weight = weight;

                await itemRepository.UpdateAsync(item);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }
        //public async void DeleteItem()
        //{
        //    Console.WriteLine("Enter item ID to delete it:\n");
        //    int id = int.Parse(ReadLine());
        //    await repository.DeleteByIdAsync(id);
        //}
        public bool mainMenu()
        {
            Console.WriteLine("Select an option:\nc: manage client table\ni: manage item table\nx: exit"); // "Items:\n:l: list items\nn: insert new item\ne: edit item\nd: delete item\nx: exit\n");
            string input = Console.ReadLine();

            switch (input)
            {
                case "c":
                    ManageClients("clients");
                    return true;
                case "i":
                    ManageItems("items");
                    return true;
                case "x":
                    return false;
                    
                default:
                    Console.WriteLine("Invalid command");
                    Console.WriteLine("\n");
                    return true;
            }
        }
        void ManageItems(string placeholder)
        {
            Console.WriteLine("Items:\n:l: list items\nn: insert new item\ne: edit item\nd: delete item\nx: exit to main menu\n");
            string input = Console.ReadLine();
            switch (input)
            {
                case "l":

                    ListItems();
                    Console.WriteLine("\n");

                    break;
                case "n":
                    InsertItems();
                    Console.WriteLine("\n");

                    break;
                case "x":
                    InsertItems();
                    break;
                case "d":
                    //consoleInterface.DeleteItem();
                    break;
                case "e":
                    EditItem();
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    Console.WriteLine("\n");

                    break;

            }
        }
    }
}
