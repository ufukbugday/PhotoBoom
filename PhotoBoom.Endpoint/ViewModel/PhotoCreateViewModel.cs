using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PhotoBoom.Endpoint.ViewModel
{
	public class PhotoCreateViewModel
	{
		[Required]
		[MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
		public string Title { get; set; }
		[Required]
		[MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
		public string Tag { get; set; }
		[Required]	
		public IFormFile Photo { get; set; }
    }
}
