using Microsoft.EntityFrameworkCore;
using usermanagment.Model;

namespace usermanagment.dbcontext
{
    public class usercontext: DbContext
    {
       
            public usercontext(DbContextOptions<usercontext> options) : base(options)
            {

            }

         public DbSet<user> users { get; set; }
        public DbSet<Resourceallocation> Resourceallocations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<project> projects { get; set; }



    }
}
