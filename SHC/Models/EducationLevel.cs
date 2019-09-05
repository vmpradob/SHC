namespace SHC.Models
{
	public class EducationLevel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
