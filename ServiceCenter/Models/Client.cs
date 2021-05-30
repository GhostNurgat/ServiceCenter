using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Поле \"Фамилия\" является обезательным.")]
        [StringLength(50, ErrorMessage = "Превышено макс. длины строки для поля \"Фамилия\"!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле \"Имя\" является обезательным.")]
        [StringLength(50, ErrorMessage = "Превышено макс. длины строки для поля \"Имя\"!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле \"Отчество\" является обезательным.")]
        [StringLength(50, ErrorMessage = "Превышено макс. длины строки для поля \"Отчество\"!")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Дата рождения является обезательное поле")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Поле \"Адрес\" является обезательным.")]
        [StringLength(60, ErrorMessage = "Превышено макс. длины строки для поля \"Адрес\"!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Поле \"Вид техники\" является обезательным.")]
        public string Technology { get; set; }

        [Required(ErrorMessage = "Поле \"Бренд\" является обезательным.")]
        public string BrandName { get; set; }

        [Required(ErrorMessage = "Поле \"Название техники\" является обезательным.")]
        [StringLength(65, ErrorMessage = "Превышено макс. длины строки для поля \"Название техники\"!")]
        public string TechnologyName { get; set; }

        [Required(ErrorMessage = "Поле \"Номер телефона\" является обезательным.")]
        [StringLength(25, ErrorMessage = "Превышено макс. длины строки для поля \"Номер телефона\"!")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Неверный номер телефона!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле \"Email\" является обезательным.")]
        [StringLength(35, ErrorMessage = "Превышено макс. длины строки для поля \"Email\"!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Неверный Email!")]
        public string Email { get; set; }

        public virtual Order Order { get; set; }
    }
}
