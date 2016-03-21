using System;
using System.Collections.Generic;
using System.Linq;
using AccessDatabase.Entity;
using Dapper;

namespace AccessDatabase.Repository
{
    public class DojosRepository : BaseRepository
    {
        public List<Dojos> Get()
        {
            var sql = "SELECT * FROM tblDojos";

            return Query<Dojos>(sql);
        }

        public Dojos Get(int id)
        {
            var sql         = "SELECT * FROM tblDojos WHERE IDDOjos = @ID";
            var parameter   = new {ID = id};

            return Query<Dojos>(sql, parameter).FirstOrDefault();
        }

        public Dojos Insert(Dojos dojo)
        {
            if (dojo == null)
                throw new ArgumentNullException(nameof(dojo));
            if (dojo.IDDojos != 0)
                throw new ArgumentException(nameof(dojo.IDDojos));

            Dojos result = null;
            var saveDate = DateTime.Now.ToOADate();
            string sql  = " Insert INTO tblDojos " 
                        + " (intIDPersonen, strDojoName, strBeschreibung, strDojoUrl, intDDLinkStatus, intDDStilrichtung, strPLZ, strOrt, intDDBundesland, intDDLand, blnFreigegeben, memInternerKommentar, dtmLastChanged, intIDLastChanged, dtmErstAm, intIDErstVon) "
                        + " VALUES (@IDPersonen, @DojoName, @Beschreibung, @DojoUrl, @DDLinkStatus, @DDStilrichtung, @PLZ, @Ort, @DDBundesland, @DDLand, @Freigegeben, @InternerKommentar, @LastChanged, @IDLastChanged, @ErstAm, @IDErstVon ) ";
            var parameter = new
            {
                IDPersonen          = dojo.intIDPersonen,
                DojoName            = dojo.strDojoName,
                Beschreibung        = dojo.strBeschreibung,
                DojoUrl             = dojo.strDojoUrl,
                DDLinkStatus        = dojo.intDDLinkStatus,
                DDStilrichtung      = dojo.intDDStilrichtung,
                PLZ                 = dojo.strPLZ,
                Ort                 = dojo.strOrt,
                DDBundesland        = dojo.intDDBundesland,
                DDLand              = dojo.intDDLand,
                Freigegeben         = dojo.blnFreigegeben,
                InternerKommentar   = dojo.memInternerKommentar,
                LastChanged         = saveDate,
                IDLastChanged       = dojo.intIDLastChanged,
                ErstAm              = saveDate,
                IDErstVon           = dojo.intIDErstVon
            };
            var rowsaffected = Execute<Dojos>(sql, parameter);
            if (rowsaffected > 0)
            {
                var select  = " SELECT TOP 1 * "
                            + " FROM tblDojos "
                            + " WHERE dtmErstAm = @Saved AND dtmLastChanged = @Saved "
                            + " ORDER BY IDDojos DESC";
                var parameter2 = new { Saved = saveDate };

                result = Query<Dojos>(select, parameter2).FirstOrDefault();
            }
            return result;
        }

        public Dojos Update(Dojos dojo)
        {
            Dojos result = null;

            string sql = " UPDATE tblDojos "
                        + " SET "
                        + " intIDPersonen           = @IDPersonen, "
                        + " strDojoName             = @DojoName, " 
                        + " strBeschreibung         = @Beschreibung, "
                        + " strDojoUrl              = @DojoUrl, "
                        + " intDDLinkStatus         = @DDLinkStatus, "
                        + " intDDStilrichtung       = @DDStilrichtung, "
                        + " strPLZ                  = @PLZ, "
                        + " strOrt                  = @Ort, "
                        + " intDDBundesland         = @DDBundesland, "
                        + " intDDLand               = @DDLand, "
                        + " blnFreigegeben          = @Freigegeben, "
                        + " memInternerKommentar    = @InternerKommentar, "
                        + " dtmLastChanged          = @LastChanged, "
                        + " intIDLastChanged        = @IDLastChanged "
                        + " WHERE (IDDojos = @ID)";

            var parameters = GetUpdateParameters(dojo);
            int affectedRows = Execute<Termine>(sql, parameters);

            if (affectedRows > 0)
            {
                result = Get(dojo.IDDojos);
            }

            return result;

        }

        private DynamicParameters GetUpdateParameters(Dojos dojo)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("IDPersonen", dojo.intIDPersonen);
            parameters.Add("DojoName", dojo.strDojoName);
            parameters.Add("Beschreibung", dojo.strBeschreibung);
            parameters.Add("DojoUrl", dojo.strDojoUrl);
            parameters.Add("DDLinkStatus", dojo.intDDLinkStatus);
            parameters.Add("DDStilrichtung", dojo.intDDStilrichtung);
            parameters.Add("PLZ", dojo.strPLZ);
            parameters.Add("Ort", dojo.strOrt);
            parameters.Add("DDBundesland", dojo.intDDBundesland);
            parameters.Add("DDLand", dojo.intDDLand);
            parameters.Add("Freigegeben", dojo.blnFreigegeben);
            parameters.Add("InternerKommentar", dojo.memInternerKommentar);
            parameters.Add("LastChanged", DateTime.Now.ToOADate());
            parameters.Add("IDLastChanged", dojo.intIDLastChanged);
            parameters.Add("ID", dojo.IDDojos);

            return parameters;
        }

        public int Delete(int id)
        {
            var sql = "DELETE FROM tblDojos WHERE IDDojos = @ID";
            var parameter = new { ID = id };
            return Execute<Dojos>(sql, parameter);
        }


    }
}