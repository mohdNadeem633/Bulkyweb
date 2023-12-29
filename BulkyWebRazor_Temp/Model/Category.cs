using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyWebRazor_Temp.Model
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }

		[Required]
		[MaxLength(30)]
		[DisplayName("Order Name")]
		public string? Name { get; set; }

		[DisplayName("Display Order")]

		[Range(1, 100, ErrorMessage = "Please Enter order between 1-100")]
		public int DisplayOrder { get; set; }
	}
}
