using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Models
{
    public class TypeTechnology
    {
        [Key]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Поле \"Название типы техники\" является обязательным!")]
        public string TypeName { get; set; }

        public virtual List<Service> Services { get; set; } = new List<Service>();
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
