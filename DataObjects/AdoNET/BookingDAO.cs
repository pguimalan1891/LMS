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

        public List<Dictionary<string,object>> getBookingRecords()
        {
            string sql = "exec RetrieveBookingRecord";

            object[] parms = { };
            return db.ReadDictionary(sql, 0, parms);
        }
    }
}
