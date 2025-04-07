using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace E_Project_.Models
{
    public class Connection : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=.;User Id=sa;Password=aptech;Database=project1;TrustServerCertificate=true;");

        }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<login> admins { get; set; }
        public DbSet<ProductCheckout> ProductCheckouts { get; set; }

        public DbSet<Payment_method> Payment_methods { get; set; }
    }
}
