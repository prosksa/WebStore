namespace WebStore.Domain.Entities.Base.Interfaces
{

	/// <summary>
	/// Entity with order
	/// </summary>
	public interface IOrderedEntity
	{
		int Order { get; set; }
	}
}