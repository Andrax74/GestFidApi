using System;

namespace ArticoliWebService.Models
{
    public class InfoMsg
    {
        public DateTime data { get; set; }
        public string messaggio { get; set; }

        public InfoMsg(DateTime data, string messaggio)
        {
            this.data = data;
            this.messaggio = messaggio;
        }
    }
}