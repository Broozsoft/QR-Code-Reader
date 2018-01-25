using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qr_reader.Models
{
    public class DbEntity : DbContext
    {
        public DbEntity(DbContextOptions<DbEntity> options) : base(options) { }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Article_type> Article_types { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbEntity()
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Test>().ToTable("test");
        //    modelBuilder.Entity<Article_type>().ToTable("article_type");
        //    modelBuilder.Entity<Article>().ToTable("article");
        //    modelBuilder.Entity<Unit>().ToTable("unit");
        //    modelBuilder.Entity<Result>().ToTable("result");
        //    modelBuilder.Entity<Order>().ToTable("order");
        //    modelBuilder.Entity<ResultUnit>()
        //    .HasKey(bc => new { bc.result_Id, bc.unit_Id });

        //    modelBuilder.Entity<ResultUnit>()
        //        .HasOne(bc => bc.result)
        //        .WithMany(b => b.resultunits)
        //    .HasForeignKey(bc => bc.result_Id);

        //    modelBuilder.Entity<ResultUnit>()
        //        .HasOne(bc => bc.unit)
        //        .WithMany(c => c.resultunits)
        //        .HasForeignKey(bc => bc.unit_Id);


        //    modelBuilder.Entity<ResultUnit>().

        //}
    }
}
