using PhotoBoom.Core.DataAccess.EntityFramework;
using PhotoBoom.Entity;

namespace PhotoBoom.DataAccess
{
	public class EfTagDal : EfEntityRepositoryBase<Tag, PhotoBoomDbContext>, ITagDal
	{
		
	}
}