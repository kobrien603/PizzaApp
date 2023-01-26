using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Shared.Models
{
	public class ValidResponse
	{
		public bool IsValid { get; set; }
		public string ResponseMessage { get; set; } = string.Empty;
	}

	public class ValidResponse<T>
	{
		public bool IsValid { get; set; }
		public T Data { get; set; }
		public string ResponseMessage { get; set; } = string.Empty;
	}
}
