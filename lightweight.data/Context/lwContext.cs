using lightweight.data.Model;
using Microsoft.EntityFrameworkCore;

namespace lightweight.data.Context
{
    public class lwContext:DbContext
    {
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=postgres;User ID=postgres;password=159753;");
           
        }
         public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<Users>().HasData(new Users {id = 1,email = "i@berkanarikan.com.tr",lastName = "ARIKAN",firstName = "Berkan",password = "vFTlduBZkNsjuhjbDazejw==" });
         }
        
       
    }
}