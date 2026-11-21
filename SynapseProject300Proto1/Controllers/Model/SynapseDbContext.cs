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
        public DbSet<Vault> Vaults { get; set; }
        public DbSet<Folder> Folders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Notes");
                entity.HasKey(n => n.NoteId);
            });
            modelBuilder.Entity<Vault>(entity =>
            {
                entity.ToTable("Vaults");
                entity.HasKey(v => v.VaultId);
            });
            modelBuilder.Entity<Folder>(entity =>
            {
                entity.ToTable("Folder");
                entity.HasKey(f => f.FolderId);
            });
        }
        //Use x table usaing xID (e.g Note Table with NoteID)
    }
}
