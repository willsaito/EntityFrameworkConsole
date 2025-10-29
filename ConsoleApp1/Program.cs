using ConsoleApp1;
using ConsoleApp1.Data;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        using (TestContext context = new TestContext())
        {
            context.Database.EnsureCreated();
        }

        bool running = true;
        ConsoleInterface consoleInterface = new ConsoleInterface();
        
      
        while (running)
        {
            running = consoleInterface.mainMenu();
        }
    }
}