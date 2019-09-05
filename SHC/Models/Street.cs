namespace SHC.Models
{
	public class Street
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual Sector Sector { get; set; }

		public Street()
		{
			Sector = new Sector();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
