using System;

namespace AccessDatabase.Interface
{
    public interface IEntity
    {
        DateTime? dtmLastChanged { get; set; }
        int intIDLastChanged { get; set; }
        DateTime? dtmErstAm { get; set; }
        int intIDErstVon { get; set; }
    }
}