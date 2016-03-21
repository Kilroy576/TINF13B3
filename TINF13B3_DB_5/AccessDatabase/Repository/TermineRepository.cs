using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AccessDatabase.Entity;
using Dapper;

namespace AccessDatabase.Repository
{
    public class TermineRepository : BaseRepository
    {
        public List<Termine> Get()
        {
            var sql = "SELECT * FROM tblTermine";
            return Query<Termine>(sql);
        }
        
        public Termine Get(int id)
        {
            var sql         = "SELECT * FROM tblTermine WHERE IDTermine = @ID";
            var parameter   = new { ID = id };
            return Query<Termine>(sql,parameter).FirstOrDefault();
        }

        public Termine Insert(Termine termin)
        {
            if (termin == null)
                throw new ArgumentNullException(nameof(termin));
            if (termin.IDTermine != 0)
                throw new ArgumentException(nameof(termin.IDTermine));

            Termine result = null;
            var saveDate    = DateTime.Now.ToOADate();
            var sql = " INSERT INTO tblTermine " 
                    + " (strOrt, intIDPersonen, dtmDatumvon,dtmDatumbis,strTitel,strBeschreibung,strTerminUrl,intIDDojoAusrichter,strVerband,blnLehrgang,blnMeisterschaft,blnFreigegeben,intDDLinkStatus,dtmLastChanged,intIDLastChanged,dtmErstAm,intIDErstVon,intDDQuelleTermine,intDDStatusTermine) " 
                    + " VALUES (@Ort, @IDPersonen, @Von, @Bis, @Titel, @Beschreibung, @TerminUrl, @IDDojoAusrichter, @Verband, @Lehrgang, @Meisterschaft, @Freigegeben, @DDLinkStatus, @LastChanged, @IDLastChanged, @ErstAm, @IDErstVon, @DDQuelleTermine, @DDStatusTermine)";
            var parameter = new
            {
                Ort                 = termin.strOrt,
                IDPersonen          = termin.intIDPersonen,
                Von                 = termin.dtmDatumvon,
                Bis                 = termin.dtmDatumbis,
                Titel               = termin.strTitel,
                Beschreibung        = termin.strBeschreibung,
                TerminUrl           = termin.strTerminUrl,
                IDDojoAusrichter    = termin.intIDDojoAusrichter,
                Verband             = termin.strVerband,
                Lehrgang            = termin.blnLehrgang,
                Meisterschaft       = termin.blnMeisterschaft,
                Freigegeben         = termin.blnFreigegeben,
                DDLinkStatus        = termin.intDDLinkStatus,
                LastChanged         = saveDate,
                IDLastChanged       = 1,
                ErstAm              = saveDate,
                IDErstVon           = 1,
                DDQuelleTermine     = termin.intDDQuelleTermine,
                DDStatusTermine     = termin.intDDStatusTermine
            };

            int affectedRows = Execute<Termine>(sql, parameter);
            if (affectedRows > 0)
            {
                var sql2    = " SELECT TOP 1 * " 
                            + " FROM tblTermine " 
                            + " WHERE dtmErstAm = @Saved AND dtmLastChanged = @Saved " 
                            + " ORDER BY IDTermine DESC";
                var parameter2  = new { Saved = saveDate };
                result          = Query<Termine>(sql2, parameter2).ToList().FirstOrDefault();
            }
            
            return result;
        }
        
        public Termine Update(Termine termin)
        {
            Termine result = null;
            
            string sql  = " UPDATE tblTermine " 
                        + " SET "
                        + " strOrt              = @Ort , "
                        + " dtmDatumvon         = @Von, "
                        + " dtmDatumbis         = @Bis, "
                        + " intIDPersonen       = @IDPersonen, "
                        + " strTitel            = @Titel, "
                        + " strBeschreibung     = @Beschreibung,"
                        + " strTerminUrl        = @TerminUrl,"
                        + " intIDDojoAusrichter = @IDDojoAusrichter,"
                        + " strVerband          = @Verband,"
                        + " blnLehrgang         = @Lehrgang,"
                        + " blnMeisterschaft    = @Meisterschaft,"
                        + " blnFreigegeben      = @Freigegeben,"
                        + " intDDLinkStatus     = @DDLinkStatus,"
                        + " dtmLastChanged      = @LastChanged,"
                        + " intIDLastChanged    = @IDLastChanged,"
                        + " intDDQuelleTermine  = @DDQuelleTermine,"
                        + " intDDStatusTermine  = @DDStatusTermine"
                        + " WHERE (IDTermine    = @ID)";

            var parameters      = GetUpdateParameters(termin);
            int affectedRows    = Execute<Termine>(sql, parameters);

            if (affectedRows > 0)
            {
                result = Get(termin.IDTermine);
            }

            return result;
        }

        private DynamicParameters GetUpdateParameters(Termine termin)
        {
            // Error in Dapper with Access: 
            // http://stackoverflow.com/questions/12322023/dapper-ms-access-read-works-write-doesnt

            var parameters = new DynamicParameters();
            parameters.Add("Ort", termin.strOrt);
            parameters.Add("Von", termin.dtmDatumvon);
            parameters.Add("Bis", termin.dtmDatumbis);
            parameters.Add("intIDPersonen", termin.intIDPersonen);
            parameters.Add("Titel", termin.strTitel);
            parameters.Add("Beschreibung", termin.strBeschreibung);
            parameters.Add("TerminUrl", termin.strTerminUrl);
            parameters.Add("IDDojoAusrichter", termin.intIDDojoAusrichter);
            parameters.Add("Verband", termin.strVerband);
            parameters.Add("Lehrgang", termin.blnLehrgang);
            parameters.Add("Meisterschaft", termin.blnMeisterschaft);
            parameters.Add("Freigegeben", termin.blnFreigegeben);
            parameters.Add("DDLinkStatus", termin.intDDLinkStatus);
            parameters.Add("LastChanged", termin.dtmLastChanged, DbType.Date);
            parameters.Add("IDLastChanged", termin.intIDLastChanged);
            parameters.Add("DDQuelleTermine", termin.intDDQuelleTermine);
            parameters.Add("DDStatusTermine", termin.intDDStatusTermine);
            parameters.Add("ID", termin.IDTermine);
            return parameters;
        }

        public int Delete(int id)
        {
            var sql         = "DELETE FROM tblTermine WHERE IDTermine = @ID";
            var parameter   = new { ID = id };
            return Execute<Termine>(sql, parameter);
        }
    }
}