using System;
using AccessDatabase.Interface;

namespace AccessDatabase.Entity
{
    public class Termine:IEntity
    {
        public int IDTermine { get; set; }
        public string strOrt { get; set; }
        public int intIDPersonen { get; set; }
        public DateTime? dtmDatumvon { get; set; }
        public DateTime? dtmDatumbis { get; set; }
        public string strTitel { get; set; }
        public string strBeschreibung { get; set; }
        public string strTerminUrl { get; set; }
        public int? intIDDojoAusrichter { get; set; }
        public string strVerband { get; set; }
        public bool blnLehrgang { get; set; }
        public bool blnMeisterschaft { get; set; }
        public bool blnFreigegeben { get; set; }
        public int? intDDLinkStatus { get; set; }
        public int intDDQuelleTermine { get; set; }
        public int intDDStatusTermine { get; set; }


        public DateTime? dtmLastChanged { get; set; }
        public int intIDLastChanged { get; set; }
        public DateTime? dtmErstAm { get; set; }
        public int intIDErstVon { get; set; }
    }
}