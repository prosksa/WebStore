using System.Collections.Generic;
using System.Linq;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Implementations
{
	public class InMemoryEmployeesData : IEmployeesData
	{
		private readonly List<EmployeeView> _employees;

		public InMemoryEmployeesData()
		{
			_employees = new List<EmployeeView>(3)
			{
				new EmployeeView(){Id = 1, FirstName = "John", SurName = "Doe", Age = 22, Position = "Director"},
				new EmployeeView(){Id = 2, FirstName = "Jack", SurName = "Jacklebons", Age = 30, Position = "Programmer"},
				new EmployeeView(){Id = 3, FirstName = "Robert", SurName = "Denuovo", Age = 50, Position = "Stuff Manager"}
			};
		}
		public IEnumerable<EmployeeView> GetAll()
		{
			return _employees;
		}

		public EmployeeView GetById(int id)
		{
			return _employees.FirstOrDefault(e => e.Id.Equals(id));
		}

		public void Commit()
		{
		}

		public void AddNew(EmployeeView model)
		{
			model.Id = _employees.Max(e => e.Id) + 1;
			_employees.Add(model);
		}

		public void Delete(int id)
		{
			var employee = GetById(id);
			if (employee != null)
			{
				_employees.Remove(employee);
			}
		}
	}
}
