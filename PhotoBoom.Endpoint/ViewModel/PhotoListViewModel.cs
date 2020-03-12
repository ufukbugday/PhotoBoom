using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoBoom.Entity;

namespace PhotoBoom.Endpoint.ViewModel
{
	public class PhotoListViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string TagStr { get; set; }
		public string PhotoPath { get; set; }
	}
}
