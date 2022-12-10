using Microsoft.EntityFrameworkCore;
using myLive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myLive.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<InstrutoresModel> Instrutores { get; set; }
    }
}
