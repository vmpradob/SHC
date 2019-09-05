namespace SHC.Models
{
	public class Specialty
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
