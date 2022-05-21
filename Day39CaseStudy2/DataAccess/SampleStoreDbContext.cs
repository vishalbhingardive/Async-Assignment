using Day39CaseStudy.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Day39CaseStudy.DataAccess;

public class SampleStoreDbContext : DbContext
{
    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Product> Products { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=WAIANGDESK23\MSSQLSERVER01;Initial Catalog=SampleStore;Integrated Security=True");
    }
}
