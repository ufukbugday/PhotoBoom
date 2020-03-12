using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PhotoBoom.Core.Entity;

namespace PhotoBoom.Entity
{
	public class Tag : IEntity
	{
		public int Id { get; set; }
		public int PhotoId { get; set; }
		public string Name { get; set; }
	}
}
