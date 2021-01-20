using System;
using System.Collections.Generic;


namespace GestFidApi.Dtos
{
    public class ClientiDto
    {
        public string CodFid { get; set; }
        public string Nominativo { get; set; }
        public string Comune { get; set; }
        public Int16? Stato { get; set; }
        public string IdAvatar { get; set; }
        
        public virtual ICollection<TransazDto> Transazioni { get; set; }
    }
}