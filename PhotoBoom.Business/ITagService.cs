using System;
using System.Collections.Generic;
using System.Text;
using PhotoBoom.Entity;

namespace PhotoBoom.Business
{
	public interface ITagService
	{
		List<Tag> GetAll();
		Tag GetById(int id);
		List<Tag> GetByPhotoId(int photoId);
		void Add(Tag tag);
		void Update(Tag tag);
		void Delete(int tag);
	}
}
