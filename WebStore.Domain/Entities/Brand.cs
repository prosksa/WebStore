using System.Collections.Generic;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
	/// <inheritdoc cref="NamedEntity" />
	/// <summary>
	/// Сущность бренд
	/// </summary>
	public class Brand : NamedEntity, IOrderedEntity
	{
		public int Order { get; set; }
		public virtual ICollection<Product> Products { get; set; }
	}
}
