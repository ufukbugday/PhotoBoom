using System;
using System.Collections.Generic;
using System.Text;
using PhotoBoom.DataAccess;
using PhotoBoom.Entity;

namespace PhotoBoom.Business
{
	public class TagManager : ITagService
	{
		private readonly ITagDal tagDal;

		public TagManager(ITagDal tagDal)
		{
			this.tagDal = tagDal;
		}

		public List<Tag> GetAll()
		{
			return tagDal.GetList();

		}

		public Tag GetById(int id)
		{
			return tagDal.Get(c => c.Id == id);
		}

		public List<Tag> GetByPhotoId(int photoId)
		{
			return tagDal.GetList(c => c.PhotoId == photoId);
		}

		public void Add(Tag tag)
		{
			tagDal.Add(tag);

		}

		public void Update(Tag photo)
		{
			tagDal.Update(photo);
		}

		public void Delete(int id)
		{
			tagDal.Delete(new Tag() { Id = id });
		}
	}
}
