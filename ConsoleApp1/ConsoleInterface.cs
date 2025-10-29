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
        ClientRepository clientRepository = new ClientRepository();
        public void ListItems()
        {
            var items = itemRepository.GetAll();

            Console.WriteLine(string.Format("{0,4} {1,4} {2,-25} {3,-35} {4}", ["ID", "ClientID", "Name", "Description", "Weight"]));
            foreach (Item p in items)
            {
                Console.WriteLine(string.Format("{0,4} {1, 4} {2,-25} {3,-35} {4}", [p.Id, p.ClientID, p.Name, p.Description, p.Weight]));
            }
        }
        public void ListClients()
        {
            var clients = clientRepository.GetAll();

            Console.WriteLine(string.Format("{0,4} {1,-25} {2,-35}", ["ID", "Name", "Age"]));
            foreach (Client p in clients)
            {
                Console.WriteLine(string.Format("{0,4} {1,-25} {2,-35}", [p.Id, p.Name, p.Age]));
            }
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
            await itemRepository.AddAsync(newItem);

        }
        public async void InsertClient()
        {
            var items = itemRepository.GetAll();

            Console.WriteLine("Enter client name:\n");
            string name = ReadLine();
            Console.WriteLine("Enter client age:\n");
            int.TryParse(Console.ReadLine(), out int age);

            Console.WriteLine(string.Format("{0,4} {1,-25}", ["ID", "Name"]));
            foreach (Item p in items)
            {
                if (p.ClientID == null)
                { 
                    Console.WriteLine(string.Format("{0,4} {1,-25}", [p.Id, p.Name]));
                }
            }
            Console.WriteLine("Add items to client(type item ID - use commas if more than one):");
            string itemsId = ReadLine();

            Client newClient = new Client()
            {
                Name = name,
                Age = age
            };
        

        await clientRepository.AddAsync(newClient);

            //assign client to items:
            int[] idsArray = itemsId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(s => int.Parse(s))
                                     .ToArray();
            foreach (int id in idsArray)
            {
                await itemRepository.updateClientID(id, newClient.Id);
            }

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
        public async void EditClient()
        {
            Console.WriteLine("Enter client ID to edit it:\n");
            int id = int.Parse(ReadLine());
            try
            {
                Client client = clientRepository.GetById(id);
                Console.WriteLine($"Enter name:\nCurrent name: {client.Name}");
                string name = ReadLine();
                Console.WriteLine($"Enter client age:\nCurrent age: {client.Age}");
                int.TryParse(Console.ReadLine(), out int age);

                client.Name = name;
                client.Age = age;

                await clientRepository.UpdateAsync(client);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        public async void DeleteItem()
        {
            Console.WriteLine("Enter item ID to delete it:\n");
            int id = int.Parse(ReadLine());
            await itemRepository.DeleteByIdAsync(id);
        }
        public async void DeleteClient()
        {
            Console.WriteLine("Enter client ID to delete it:\n");
            int id = int.Parse(ReadLine());
            await clientRepository.DeleteByIdAsync(id);
        }
        public bool MainMenu()
        {
            Console.WriteLine("Main Menu\n");
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
            bool manageItems = true;
            while (manageItems)
            {
                Console.WriteLine("Items:\nl: list items\nn: insert new item\ne: edit item\nd: delete item\nx: back to main menu\n");
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
                        manageItems = false;
                        break;
                    case "d":
                        DeleteItem();
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
        void ManageClients(string placeholder)
        {
            bool manageClients = true;

            while (manageClients)
            {


                Console.WriteLine("Clients:\nl: list clients\ni: list client's items\nn: insert new client\ne: edit client\nd: delete client\nx: back to main menu\n");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "l":

                        ListClients();
                        Console.WriteLine("\n");

                        break;
                    case "i":
                        ListClientsItems();
                        break;
                    case "n":
                        InsertClient();
                        Console.WriteLine("\n");

                        break;
                    case "x":
                        manageClients = false;
                        
                        break;
                    case "d":
                        DeleteClient();
                        break;
                    case "e":
                        EditClient();
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        Console.WriteLine("\n");

                        break;

                }
            }
        }

        private void ListClientsItems()
        {
            Console.WriteLine("Enter client ID:\n");
            int.TryParse(Console.ReadLine(), out int clientId);

            var itemList = itemRepository.GetByClientID(clientId);

            Console.WriteLine($"{clientRepository.GetById(clientId).Name} is carrying:\n");
            if ( itemList.Count == 0)
            {
                Console.WriteLine("Nothing\n");
            }
            else { 
                foreach (Item item in itemList)
                {
                    Console.WriteLine(item.Name);
                }
                Console.WriteLine("\n");
            }
        }
    }
}
