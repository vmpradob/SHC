namespace SHC.Models
{
	public class Address
	{
		public int Id { get; set; }
		
		public virtual Parish Parish { get; set; }
		public virtual Community Community { get; set; }
		public virtual Sector Sector { get; set; }
		public virtual Street Street { get; set; }

		public Address()
		{
			Parish = new Parish();
			Community = new Community();
			Sector = new Sector();
			Street = new Street();
		}

		public override string ToString()
		{
			if (Street == null)
			{
				return string.Format("{0}, {1}, {2}.", Parish.Name, Community.Name, Sector.Name);
			}

			if (Sector == null)
			{
				return string.Format("{0}, {1}.", Parish.Name, Community.Name);
			}

			return string.Format("{0}, {1}, {2}, {3}.", Parish.Name, Community.Name, Sector.Name, Street.Name);
		}
	}
}
