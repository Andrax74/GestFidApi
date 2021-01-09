using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestFidApi.Models
{
    public class Clienti
    {
        [Key]
        [MinLength(8, ErrorMessage = "Il numero minimo di caratteri è 8")]
        [MaxLength(10, ErrorMessage = "Il numero massimo di caratteri è 10")]
        public string CodFid { get; set; }

        [StringLength(50, MinimumLength=5, ErrorMessage="Il Nominativo deve aver almeno 5 e massimo 50 caratteri")]
        public string Nominativo { get; set; }
        public string Comune { get; set; }
        public Int16? Stato { get; set; } = 1;
        public virtual ICollection<Transazioni> transaz { get; set; }
    }
}