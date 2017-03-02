using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataObjects.AdoNET
{
    public class BookingDAO: IBookingDAO
    {
        static DB db = new DB();

        public List<Dictionary<string,object>> getBookingRecords(int status)
        {
            string sql = "RetrieveBookingRecords";

            object[] parms = { "@statuscode", status };
            return db.ReadDictionary(sql, 1, parms);
        }

        public List<Dictionary<string, object>> getCheckVoucher(int status)
        {
            string sql = "usp_getCheckVoucher";

            object[] parms = { "@statuscode", status };
            return db.ReadDictionary(sql, 1, parms);
        }

        public List<Dictionary<string, object>> getCIRForm(int status)
        {
            string sql = "usp_getCIRForm";

            object[] parms = { "@statuscode", status };
            return db.ReadDictionary(sql, 1, parms);
        }
    }
}
