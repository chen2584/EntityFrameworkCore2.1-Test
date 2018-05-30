using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace testAPI.Models
{
    public class ChenDbContext : DbContext
    {
        public static string connectionString { private get; set; }

        public ChenDbContext() { }

        public ChenDbContext(bool isLazy)
        {

        }

        private readonly IConfiguration configuration;
        public ChenDbContext(IConfiguration configuration) =>
            this.configuration = configuration;

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Customer> Customer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString).ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength=10)]
        public string userName { get; set; }
        [Required]
        [MaxLength(30)]
        public string lastName { get; set; }
        [MaxLength(100)]
        public string email { get; set; }
        public virtual List<Order> Order { get; set; }

    }

    public class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int value { get; set; }


        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserInfo UserInfo { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string productName { get; set; }
        public int remain { get; set; }
        public int price { get; set; }
    }

    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}