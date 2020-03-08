using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoBoom.Core.Entity;

namespace PhotoBoom.Entity
{
	public class Photo : IEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Tag { get; set; }
		public string PhotoPath { get; set; }
	}
}
