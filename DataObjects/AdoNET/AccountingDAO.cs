using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNET
{
    public class AccountingDAO: IAccountingDAO
    {
        static DB db = new DB();

        public List<Dictionary<string, object>> getRequestForPayment(int status)
        {
            string sql = "usp_getRequestForPayment";

            object[] parms = { "@statuscode", status };
            return db.ReadDictionary(sql, 1, parms);
        }
    }
}
