using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoBoom.Entity;

namespace PhotoBoom.Endpoint.ViewModel
{
	public class PhotoDetailsViewModel
	{
		public Photo Photo { get; set; }
		public string TagStr { get; set; }
		public string PageTitle { get; set; }
	}
}
