using entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tables
        public DbSet<Country>? Countries { get; set; }
        public DbSet<CountryName>? CountryNames { get; set; }
        public DbSet<State>? States { get; set; }
        public DbSet<StateName>? StateNames { get; set; }
        public DbSet<City>? Cities { get; set; }
        public DbSet<CityName>? CityNames { get; set; }
        public DbSet<Timezone>? Timezones { get; set; }
        public DbSet<Language>? Languages { get; set; }
    }
}
