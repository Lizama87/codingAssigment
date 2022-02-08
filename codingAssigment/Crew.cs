using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace codingAssigment
{
	public class Crew
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Ranch { get; set; }
		public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
	}
}
