using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DanfossProject.Data.Models.Entities
{
	public class AddressModel
	{
		[Required(ErrorMessage = "Название страны не может быть пустым")]
		[RegularExpression(@"^[а-яА-ЯёЁa-zA-Z0-9-]+$", ErrorMessage = "Некорректное название страны")]
		public string Country { get; set; }

		[Required(ErrorMessage = "Индекс не может быть пустым")]
		public int ZIPCode { get; set; }

		[Required(ErrorMessage = "Название города не может быть пустым")]
		[RegularExpression(@"^[а-яА-ЯёЁa-zA-Z0-9-]+$", ErrorMessage = "Некорректное название города")]
		public string City { get; set; }
				
		[RegularExpression(@"^[а-яА-ЯёЁa-zA-Z0-9-\040]+$", ErrorMessage = "Некорректное название улицы")]
		public string Street { get; set; }

		[Required(ErrorMessage = "Номер дома не может быть пустым")]
		[RegularExpression(@"^[а-яА-ЯёЁa-zA-Z0-9/-]+$", ErrorMessage = "Некорректный номер дома")]
		public string HouseNumber { get; set; }

		//[Required(ErrorMessage = "Номер квартиры не может быть пустым")]
		//[RegularExpression(@"^[а-яА-ЯёЁa-zA-Z0-9/-]+$", ErrorMessage = "" +
		//	"Некорректное номер квартиры")]
		//public string AppartamentNumber { get; set; }
	}
}
