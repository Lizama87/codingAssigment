using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace codingAssigment
{
	public class Employee
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public bool IsCrates { get; set; }
		[JsonIgnore]
		public int? CrewId { get; set; } = null;
		[JsonIgnore]
		public Crew? Crew { get; set; } = null;

	}
}
