using laba5_oop.Storage.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Storage
{
    public class AutoDataContext : DbContext
    {
        public AutoDataContext(DbContextOptions<AutoDataContext> options) : base (options)
        {

        }

        public DbSet<Detail> Details { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ModelCar> ModelCars { get; set; }
        public DbSet<CarForSold> CarForSolds { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BrandOfDetail> BrandOfDetails { get; set; }
    }
}
