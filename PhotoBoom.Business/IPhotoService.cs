using System;
using System.Collections.Generic;
using System.Text;
using PhotoBoom.Entity;

namespace PhotoBoom.Business
{
	public interface IPhotoService
	{
		List<Photo> GetAll();
		Photo GetById(int id);
		void Add(Photo photo);
		void Update(Photo photo);
		void Delete(int photo);
    }
}
