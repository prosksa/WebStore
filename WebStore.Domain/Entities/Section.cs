using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
	/// <inheritdoc cref="NamedEntity" />
	/// <summary>
	/// Сущность секция
	/// </summary>
	public class Section : NamedEntity, IOrderedEntity
	{
		/// <summary>
		/// Родительская секция (при наличии)
		/// </summary>
		public int? ParentId { get; set; }

		[ForeignKey("ParentId")]
		public virtual Section ParentSection { get; set; }

		public int Order { get; set; }

		public virtual ICollection<Product> Products { get; set; }
	}
}