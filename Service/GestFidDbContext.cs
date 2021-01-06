
using GestFidApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticoliWebService.Services
{
    public class GestFidDbContext : DbContext
    {
        public GestFidDbContext(DbContextOptions<GestFidDbContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Clienti> Clienti { get; set; }
        public virtual DbSet<Transazioni> Transazioni {get; set; }
    
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            

            //Relazione one to many (uno a molti) fra articoli e barcode
            modelBuilder.Entity<Transazioni>()
                .HasOne<Clienti>(s => s.cliente) //ad un cliente...
                .WithMany(g => g.transaz) //corrispondono molte transazioni
                .HasForeignKey(s => s.CodFid); //la chiave esterna dell'entity transazioni
        }

        
    }
}