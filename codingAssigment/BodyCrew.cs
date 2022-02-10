using Newtonsoft.Json.Converters;

namespace codingAssigment
{
	public class BodyCrew
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Ranch { get; set; }
		public IEnumerable<int> Employees { get; set; } = new List<int>();
	}

}
