using Microsoft.EntityFrameworkCore;

namespace University_Orientation_Website.Models {
    public class SqliteDbContext :  DbContext {
        public DbSet<Staff> Staff { get; set; }
        //public Staff Staff { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Filename=./dm.db");
        }
    }
}