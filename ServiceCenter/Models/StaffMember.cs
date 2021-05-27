using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServiceCenter.Models
{
    public class StaffMember
    {
        [Key]
        public int StaffId { get; set; }

        [StringLength(50, ErrorMessage = "Превышено макс. длины строки для поля \"Фамилия\"!")]
        [Required(ErrorMessage = "Поле \"Фамилия\" является обезательным.")]
        public string Surname { get; set; }

        [StringLength(50, ErrorMessage = "Превышено макс. длины строки для поля \"Имя\"!")]
        [Required(ErrorMessage = "Поле \"Имя\" является обезательным.")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Превышено макс. длины строки для поля \"Отчество\"!")]
        [Required(ErrorMessage = "Поле \"Отчество\" является обезательным.")]
        public string Patronymic { get; set; }

        [StringLength(20, ErrorMessage = "Превышено макс. длины строки для поля \"Должность\"!")]
        [Required(ErrorMessage = "Поле \"Должность\" является обезательным.")]
        public string Position { get; set; }

        [StringLength(15, ErrorMessage = "Превышено макс. длины строки для поля \"Номер телефона\"")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Неверный формат номера телефона!")]
        [Required(ErrorMessage = "Поле \"Номер телефона\" является обезательным.")]
        public string Phone { get; set; }

        public ICollection<Order> Orders { get; set; }
        public StaffMember()
        {
            Orders = new List<Order>();
        }
    }
}
