namespace SHC.Models
{
	public class Test
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Result { get; set; }
		public string Observations { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
