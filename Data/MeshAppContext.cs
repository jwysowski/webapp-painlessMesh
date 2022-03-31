#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Listener.Models;

    public class MeshAppContext : DbContext
    {
        public MeshAppContext (DbContextOptions<MeshAppContext> options)
            : base(options)
        {
        }

        public DbSet<Listener.Models.Log> Log { get; set; }

        public DbSet<Listener.Models.Node> Node { get; set; }
    }
