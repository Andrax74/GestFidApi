using System;
using System.ComponentModel.DataAnnotations;

namespace GestFidApi.Models
{
    public class Transazioni
    {
        [Key]
        public Int64 Id { get; set; }
        public DateTime Data { get; set; }
        public string CodFid { get; set; }
        public string PuntoVendita { get; set; }
        public int? Bollini { get; set; }
        public virtual Clienti cliente { get; set; }

    }
}