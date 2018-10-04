using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
	[Route("users")]
	public class EmployeesController : Controller
	{
		private readonly IEmployeesData _employeesData;

		public EmployeesController(IEmployeesData employeesData)
		{
			_employeesData = employeesData;
		}

		/// <summary>
		/// List output
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View(_employeesData.GetAll());
		}

		/// <summary>
		/// Employee details
		/// </summary>
		/// <param name="id">Employee Id</param>
		/// <returns></returns>
		[Route("{id}")]
		public IActionResult Details(int id)
		{
			var employee = _employeesData.GetById(id);

			if (ReferenceEquals(employee, null))
				return NotFound();

			return View(employee);
		}

		/// <summary>
		/// Add or edit an employee
		/// </summary>
		/// <param name="id">Employee Id</param>
		/// <returns></returns>
		[Route("edit/{id?}")]
		public IActionResult Edit(int? id)
		{
			EmployeeView model;
			if (id.HasValue)
			{
				model = _employeesData.GetById(id.Value);
				if (ReferenceEquals(model, null))
					return NotFound();
			}
			else
			{
				model = new EmployeeView();
			}
			return View(model);
		}

		[HttpPost]
		[Route("edit/{id?}")]
		public IActionResult Edit(EmployeeView model)
		{
			if (model.Age < 18 && model.Age > 75)
			{
				ModelState.AddModelError("Age", "Age error!");
			}

			if (ModelState.IsValid)
			{
				if (model.Id > 0)
				{
					var dbItem = _employeesData.GetById(model.Id);

					if (ReferenceEquals(dbItem, null))
						return NotFound();

					dbItem.FirstName = model.FirstName;
					dbItem.SurName = model.SurName;
					dbItem.Age = model.Age;
					dbItem.Position = dbItem.Position;
				}
				else
				{
					_employeesData.AddNew(model);
				}
				_employeesData.Commit();

				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}


		/// <summary>
		/// Employee delete
		/// </summary>
		/// <param name="id">Employee Id</param>
		/// <returns></returns>
		[Route("delete/{id}")]
		public IActionResult Delete(int id)
		{
			_employeesData.Delete(id);
			return RedirectToAction(nameof(Index));
		}

	}
}