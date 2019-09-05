namespace SHC.Models
{
	public class Community
	{
		public int Id { get; set; }
		public string Name { get; set; }
		
		public virtual Parish Parish { get; set; }

		public Community()
		{
			Parish = new Parish();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
