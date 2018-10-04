using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
	public class Section : NamedEntity, IOrderedEntity
	{
		public int? ParentId { get; set; }

		[ForeignKey("ParentId")]
		public virtual Section ParentSection { get; set; }

		public int Order { get; set; }

		public virtual ICollection<Product> Products { get; set; }
	}
}