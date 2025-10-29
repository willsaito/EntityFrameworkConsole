using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public int Age {get; set; }

        public ICollection<Item>? Items { get; set; }


    }
}
