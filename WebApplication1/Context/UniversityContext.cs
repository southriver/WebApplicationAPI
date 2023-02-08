using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Context
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Student> Students{ get; set; }
    }
}
