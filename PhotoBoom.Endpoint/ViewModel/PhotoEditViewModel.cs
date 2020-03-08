using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBoom.Endpoint.ViewModel
{
	public class PhotoEditViewModel : PhotoCreateViewModel
	{
		public int Id { get; set; }
		public string ExistingPhotoPath { get; set; }
	}
}
