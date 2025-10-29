using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class DataManager
    {
        public bool InsertItems<T>(T dataObject) where T : IDbEntity
        {
            if (dataObject == null)
            {
                Console.WriteLine("Error: Cannot insert a null object.");
                return false;
            }
            return true;
        }
    }
}
