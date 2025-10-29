using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Item :IDbEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide the item's name")]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public int Weight { get; set; }
    }
}
