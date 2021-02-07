using TemplateCore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateCore.DAL.Contexts
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> opt) : base(opt)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().HasKey(x => x.name);
            //modelBuilder.Entity<Product>().HasOne(o => o.Brand).WithMany(w => w.Products).OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<ProductCategory>().HasKey(hk => new { hk.ProductID, hk.CategoryID });

            ////SEED DATA
            //modelBuilder.Entity<Admin>().HasData(new Admin { EmailAddress = "slmngoktas@gmail.com", ID = 1, Name = "Selman", Surname = "GÖKTAŞ", Password = "123456" });
        }
        public DbSet<Test> Test { get; set; }

       
       
    }
}
