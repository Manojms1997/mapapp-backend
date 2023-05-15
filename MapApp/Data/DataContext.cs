using Microsoft.EntityFrameworkCore;

namespace MapApp.Data
{
	public class DataContext: DbContext
	{
		public DataContext(DbContextOptions options) : base(options) { }

		public DbSet<EmergencyServiceLocation> EmergencyServiceLocations { get; set; }
	}
}
