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

        public virtual Service Service { get; set; }
        public virtual Order Order { get; set; }
    }
}
