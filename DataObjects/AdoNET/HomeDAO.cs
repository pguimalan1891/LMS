using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNET
{
    public class HomeDAO : IHomeDAO
    {
        static DB db = new DB();

        public List<Dictionary<string, object>> GetCustomerList()
        {
            string sql = "Select pisDat.LAST_NAME + ', ' + pisDat.FIRST_NAME + ' ' + pisDat.MIDDLE_NAME as FullName, Code from uvw_PISData pisDat " +
            "inner join uvw_PISAddress pisAdd on pisDat.ID = pisAdd.PIS_ID and pisAdd.ADDRESS_TYPE_ID = 0 and pisDat.PERMISSION > 0 Order by pisDat.DATETIME_CREATED Desc";
            object[] parms = { };
            return db.ReadDictionary(sql, 0, parms);
        }
    }
}
