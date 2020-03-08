using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhotoBoom.DataAccess;
using PhotoBoom.Entity;

namespace PhotoBoom.Business
{
	public class PhotoManager : IPhotoService
	{
		private readonly IPhotoDal photoDal;

		public PhotoManager(IPhotoDal photoDal)
		{
			this.photoDal = photoDal;
		}

		public List<Photo> GetAll()
		{
			return photoDal.GetList();

		}

		public Photo GetById(int id)
		{
			return photoDal.Get(c=>c.Id == id);
		}

		public void Add(Photo photo)
		{
			photoDal.Add(photo);

		}

		public void Update(Photo photo)
		{
			photoDal.Update(photo);
		}

		public void Delete(int photo)
		{
			photoDal.Delete(new Photo { Id = photo });
		}
	}
}
