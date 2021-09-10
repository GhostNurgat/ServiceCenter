using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Models
{
    public class Service
    {
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Поле \"Сервис\" является обязательным!")]
        [StringLength(60, ErrorMessage = "Превышено макс. длинны строки")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Укажите тип техники, пожалуйста.")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Поле \"Цена\" является обязательным!")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public virtual TypeTechnology TypeTechnology { get; set; }
    }
}
