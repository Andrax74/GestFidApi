using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticoliWebService.Services;
using GestFidApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GestFidApi.Service
{
    public class ClientiRepository : IClientiRepository
    {
        GestFidDbContext gestFidDbContext;

        public ClientiRepository(GestFidDbContext gestFidDbContext)
        {
            this.gestFidDbContext =  gestFidDbContext;
        }

        public async Task<ICollection<Clienti>> SelAll()
        {
            return await this.gestFidDbContext.Clienti
                .Include(a => a.transaz)
                .OrderBy(a => a.Nominativo)
                .ToListAsync();
        }

        public async Task<Clienti> SelCliByCode(string Code)
        {
            return await this.gestFidDbContext.Clienti
                .Include(a => a.transaz)
                .Where(a => a.CodFid.Equals(Code))
                .FirstOrDefaultAsync();
        }

        public Clienti SelCliByCode2(string Code)
        {
            return this.gestFidDbContext.Clienti
                .AsNoTracking()
                .Where(a => a.CodFid.Equals(Code))
                .FirstOrDefault();
        }

        public async Task<ICollection<Clienti>> SelCliByName(string Nominativo)
        {
            return await this.gestFidDbContext.Clienti
                .Include(a => a.transaz)
                .Where(a => a.Nominativo.Contains(Nominativo))
                .OrderBy(a => a.Nominativo)
                .ToListAsync();
        }

        public bool InsCliente(Clienti cliente)
        {
            this.gestFidDbContext.Add(cliente);
            return Salva();
        }

        public bool UpdCliente(Clienti cliente)
        {
            this.gestFidDbContext.Update(cliente);
            return Salva();
        }

        public bool ClienteExists(string Code)
        {
            return this.gestFidDbContext.Clienti
                .Any(c => c.CodFid == Code);
        }

        public bool DelCliente(Clienti cliente)
        {
            this.gestFidDbContext.Remove(cliente);
            return Salva();
        }

        private bool Salva()
        {
            try
            {
                var saved = this.gestFidDbContext.SaveChanges();
                return saved >= 0 ? true : false; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
                return false;
            }
        }

        public int GetNumTransaz(string Code)
        {
            return this.gestFidDbContext.Clienti
                .AsNoTracking()
                .Include(a => a.transaz)
                .Where(a => a.CodFid.Equals(Code))
                .FirstOrDefault().transaz.Count;
        }
    }
}