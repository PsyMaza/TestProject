using DanfossProject.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanfossProject.Data.Models.CreateModel
{
	public class WaterMeterCreateModel
	{
		public int Id { get; set; }
		
		[Required(ErrorMessage = "Заводской номер не может быть пустым")]
		[StringLength(15, ErrorMessage = "Длина строки не может содержать более 15 символов")]
		[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Некорректный номер")]
		public string SerialNumber { get; set; }

		[Required(ErrorMessage = "Показания счетчика не могут быть пустыми")]
		[Range(0, int.MaxValue, ErrorMessage = "Некорректное число")]
		public int CounterValue { get; set; }
	}
}
