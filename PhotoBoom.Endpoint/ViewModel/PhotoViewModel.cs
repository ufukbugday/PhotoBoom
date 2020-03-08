using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PhotoBoom.Endpoint.ViewModel
{
	public class PhotoViewModel
	{
		public string Title { get; set; }
		public string Tag { get; set; }
		public IFormFile Content { get; set; }
	}
}
