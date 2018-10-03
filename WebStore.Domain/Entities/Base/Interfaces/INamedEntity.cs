namespace WebStore.Domain.Entities.Base.Interfaces
{

	/// <summary>
	/// Entity with name
	/// </summary>
	public interface INamedEntity : IBaseEntity
	{
		/// <summary>
		/// Наименование
		/// </summary>
		string Name { get; set; }
	}
}