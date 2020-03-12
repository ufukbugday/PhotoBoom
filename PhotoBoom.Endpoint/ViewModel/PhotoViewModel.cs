using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhotoBoom.Entity;

namespace PhotoBoom.Endpoint.ViewModel
{
	public class PhotoViewModel
	{
		public string Title { get; set; }
		public string TagStr { get; set; }
		public IFormFile Content { get; set; }
	}
}
