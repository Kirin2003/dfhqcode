using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TopConferenceProj.Models;

namespace TopConferenceProj.Data
{
    public class TopConferenceProjContext : DbContext
    {
        public TopConferenceProjContext (DbContextOptions<TopConferenceProjContext> options)
            : base(options)
        {
        }

        public DbSet<TopConferenceProj.Models.Paper>? Paper { get; set; }
    }
}
