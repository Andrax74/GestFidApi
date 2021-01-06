using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestFidApi.Models
{
    public class Clienti
    {
        [Key]
        [MinLength(8, ErrorMessage = "Il numero minimo di caratteri è 8")]
        public string CodFid { get; set; }
        [Required]
        public string Nominativo { get; set; }
        public string Comune { get; set; }
        public Int16? Stato { get; set; }
        public virtual ICollection<Transazioni> transaz { get; set; }
    }
}