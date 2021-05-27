using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCenter.Models
{
    public class Order
    {
        [Key]
        [ForeignKey("Client")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Поле \"Услуга\" является обезательным.")]
        [StringLength(50, ErrorMessage = "Превышено макс. длины строки для поля \"Услуга\"!")]
        public string Service { get; set; }

        [Required(ErrorMessage = "Поле \"Цена\" является обезательным.")]
        [DataType(DataType.Currency, ErrorMessage = "Значения не является стоимостью!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Поле \"Гарантия\" является обезательным.")]
        [StringLength(3, ErrorMessage = "Превышено макс. длины строки для поля \"Гарантия\"!")]
        public string Guarantee { get; set; }

        [Required(ErrorMessage = "Поле \"Сотрудник\" является обезательным.")]
        public int? StaffId { get; set; }

        [Required(ErrorMessage = "Поле \"Дата приёма\" является обезательным.")]
        [DataType(DataType.Date)]
        public DateTime DateOrder { get; set; }

        [Required(ErrorMessage = "Поле \"Завершено\" является обезательным.")]
        public bool Done { get; set; }

        [Required(ErrorMessage = "Поле \"К оплате\" является обезательным.")]
        [DataType(DataType.Currency, ErrorMessage = "Значения не является число!")]
        public decimal Payment { get; set; }

        public Client Client { get; set; }
        public StaffMember StaffMember { get; set; }

    }
}
