using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
	public interface IEmployeesData
	{
		IEnumerable<EmployeeView> GetAll();

		EmployeeView GetById(int id);

		void Commit();

		void AddNew(EmployeeView model);

		void Delete(int id);
	}
}
