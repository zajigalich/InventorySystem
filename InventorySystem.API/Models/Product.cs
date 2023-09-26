using System.ComponentModel.DataAnnotations;

namespace InventorySystem.API.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[MaxLength(300)]
		public string Description { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public int Quantity { get; set; }
	}
}
