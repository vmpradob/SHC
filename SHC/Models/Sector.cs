namespace SHC.Models
{
	public class Sector
	{
		public int Id { get; set; }
		public string Name { get; set; }
		
		public virtual Community Community{ get; set; }

		public Sector()
		{
			Community = new Community();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
