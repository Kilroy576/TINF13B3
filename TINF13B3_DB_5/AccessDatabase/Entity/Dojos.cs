using System;
using AccessDatabase.Interface;

namespace AccessDatabase.Entity
{
    public class Dojos:IEntity
    {
        public int IDDojos { get; set; }
        public int intIDPersonen { get; set; }
        public string strDojoName { get; set; }
        public string strBeschreibung { get; set; }
        public string strDojoUrl { get; set; }
        public int intDDLinkStatus { get; set; }
        public int intDDStilrichtung { get; set; }
        public string strPLZ { get; set; }
        public string strOrt { get; set; }
        public int intDDBundesland { get; set; }
        public int intDDLand { get; set; }
        public bool blnFreigegeben  { get; set; }
        public string memInternerKommentar { get; set; }


        public DateTime? dtmLastChanged { get; set; }
        public int intIDLastChanged { get; set; }
        public DateTime? dtmErstAm { get; set; }
        public int intIDErstVon { get; set; }
    }
}