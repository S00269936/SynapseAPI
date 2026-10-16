using Microsoft.EntityFrameworkCore;
using SynapseProject300Proto1.Models;

namespace SynapseProject300Proto1.Controllers.Model
{
    public class SynapseDbContext : DbContext
    {
        public SynapseDbContext(DbContextOptions<SynapseDbContext> options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }
        //Folders, tags, users etc
    }
}
