using System.Collections.Generic;
using System.Threading.Tasks;
using GestFidApi.Models;

namespace GestFidApi.Service
{
    public interface IClientiRepository
    {
        Task<ICollection<Clienti>> SelAll();
        Task<ICollection<Clienti>> SelCliByName(string Nominativo);
        Task<Clienti> SelCliByCode(string Code);
        Clienti SelCliByCode2(string Code);
        bool InsCliente(Clienti cliente);
        bool UpdCliente(Clienti cliente);
        bool DelCliente(Clienti cliente);
        bool ClienteExists(string Code);
        int GetNumTransaz(string Code);
    }
}