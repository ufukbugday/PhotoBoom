using System;
using System.Collections.Generic;
using System.Text;
using PhotoBoom.Core.DataAccess;
using PhotoBoom.Entity;

namespace PhotoBoom.DataAccess
{
	public interface IPhotoDal : IEntityRepository<Photo>
	{
	}
}
