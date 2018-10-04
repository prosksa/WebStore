using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
	public class EmployeeView
	{
		public int Id { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
		[Display(Name = "Name")]
		[StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessage = "The name must be at least 2 and no more than 200 characters")]
		public string FirstName { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Surname is required")]
		[Display(Name = "Surname")]
		public string SurName { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Age is required")]
		[Display(Name = "Age")]
		public int Age { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Position is required")]
		[Display(Name = "Position")]
		public string Position { get; set; }
	}
}
