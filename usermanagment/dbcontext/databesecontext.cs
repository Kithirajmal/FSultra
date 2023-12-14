using Microsoft.EntityFrameworkCore;
using usermanagment.Model;

namespace usermanagment.dbcontext
{
    public class usercontext : DbContext
    {

        public usercontext(DbContextOptions<usercontext> options) : base(options)
        {

        }

        public DbSet<user> users { get; set; }
        public DbSet<Resourceallocation> Resourceallocations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<project> projects { get; set; }

        //public DbSet<AllocatedEmployee> AllocatedEmployees { get; set; }

        //public DbSet<NonAllocatedEmployee> nonAllocatedEmployees {  get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>(x => x
            .ToTable("Employees")
            .Property(entity => entity.Id));

            builder.Entity<Employee>(x => x
          .HasKey(entity => entity.Id));

            builder.Entity<Resourceallocation>(x => x
                     .ToTable("Resourceallocations")
                     .Property(entity => entity.id));

            builder.Entity<Resourceallocation>(x => x
                      .HasKey(entity => entity.id));
 

             builder.Entity<Employee>(x => x
                     .HasMany(entity => entity.resourceallocations).WithOne(x=>x.employee).HasForeignKey(x  => x.employeeid));
        }


    }
}
