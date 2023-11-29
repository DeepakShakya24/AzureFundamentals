using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TangyWebFunc.Model;

namespace TangyWebFunc.Data
{
    public class TangyFuncDbContext: DbContext
    {
        public TangyFuncDbContext(DbContextOptions<TangyFuncDbContext> options):base(options)
        {

        }

        public DbSet<SalesModel> SalesModels { get; set; }
        public DbSet<BlobDetails> BlobDetails { get; set; }

        //DesignTimeDbContext
        public class BloggingContextFactory : IDesignTimeDbContextFactory<TangyFuncDbContext>
        {
            public TangyFuncDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<TangyFuncDbContext>();
                optionsBuilder.UseSqlServer("Server=tcp:azfundamentalserver.database.windows.net,1433;Initial Catalog=azFundamemtaLDB;Persist Security Info=False;User ID=adminsql;Password=deepak@123@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

                return new TangyFuncDbContext(optionsBuilder.Options);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SalesModel>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<BlobDetails>(entity =>
            {
                entity.HasKey(c => c.Id);
            });
        }
    }
}
