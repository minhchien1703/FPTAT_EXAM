using Example_C_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Example_C_2.Context
{
    public class ApplicationContextDb : DbContext
    {
        public ApplicationContextDb(DbContextOptions<ApplicationContextDb> options): base(options) { }

        #region DbSet
        public DbSet<Product> Products { get; set; }

        #endregion
    }
}
