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
		/// Вывод списка
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View(_employeesData.GetAll());
		}

		/// <summary>
		/// Детали о сотруднике
		/// </summary>
		/// <param name="id">Id сотрудника</param>
		/// <returns></returns>
		[Route("{id}")]
		public IActionResult Details(int id)
		{
			//Получаем сотрудника по Id
			var employee = _employeesData.GetById(id);

			//Если такого не существует
			if (ReferenceEquals(employee, null))
				return NotFound();//возвращаем результат 404 Not Found

			//Иначе возвращаем сотрудника
			return View(employee);
		}

		/// <summary>
		/// Добавление или редактирование сотрудника
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Route("edit/{id?}")]
		public IActionResult Edit(int? id)
		{
			EmployeeView model;
			if (id.HasValue)
			{
				model = _employeesData.GetById(id.Value);
				if (ReferenceEquals(model, null))
					return NotFound();//возвращаем результат 404 Not Found
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
				ModelState.AddModelError("Age", "Ошибка возраста!");
			}
			//Проверяем модель на валидность
			if (ModelState.IsValid)
			{
				if (model.Id > 0)
				{
					var dbItem = _employeesData.GetById(model.Id);

					if (ReferenceEquals(dbItem, null))
						return NotFound();//возвращаем результат 404 Not Found

					dbItem.FirstName = model.FirstName;
					dbItem.SurName = model.SurName;
					dbItem.Age = model.Age;
					dbItem.Patronymic = model.Patronymic;
					dbItem.Position = dbItem.Position;
				}
				else
				{
					_employeesData.AddNew(model);
				}
				_employeesData.Commit();

				return RedirectToAction(nameof(Index));
			}
			//если не валидна, возвращаем её на представление
			return View(model);
		}


		/// <summary>
		/// Удаление сотрудника
		/// </summary>
		/// <param name="id">Id сотрудника</param>
		/// <returns></returns>
		[Route("delete/{id}")]
		public IActionResult Delete(int id)
		{
			_employeesData.Delete(id);
			return RedirectToAction(nameof(Index));
		}

	}
}