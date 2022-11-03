using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
	partial class MediaCard
	{
		[Parameter]
		public string Image { get; set; } = "_content/PizzaApp/images/default.jpg";

		[Parameter]
		public string Title { get; set; } = string.Empty;

		[Parameter] 
		public string Message { get; set; } = string.Empty;

		[Parameter]
		public string RedirectURL { get; set; } = string.Empty;

        [Parameter]
        public string RedirectText { get; set; } = string.Empty;

        [Parameter]
		public int Height { get; set; } = 200;
	}
}
