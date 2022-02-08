namespace codingAssigment
{
	public class BodyCrew
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Ranch { get; set; }
		public IEnumerable<int> EmployeesIDs { get; set; } = new List<int>();
	}
}
