using System;
using System.Collections.Generic;
using System.Text;
using PhotoBoom.Core.DataAccess.EntityFramework;
using PhotoBoom.Entity;

namespace PhotoBoom.DataAccess
{
	public class EfPhotoDal : EfEntityRepositoryBase<Photo, PhotoBoomDbContext>, IPhotoDal
	{
	}
}
