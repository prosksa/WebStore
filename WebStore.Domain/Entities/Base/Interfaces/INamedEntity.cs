namespace WebStore.Domain.Entities.Base.Interfaces
{

	/// <summary>
	/// Entity with name
	/// </summary>
	public interface INamedEntity : IBaseEntity
	{
		string Name { get; set; }
	}
}