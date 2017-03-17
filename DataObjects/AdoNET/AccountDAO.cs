using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNET
{
    public class AccountDAO : IAccountDAO
    {
        static DB db = new DB();

        public List<Dictionary<string, object>> Login(string username, string password)
        {
            string sql = "dbo.usp_Login";
            object[] parms = { "@username", username, "@password", password };
            return db.ReadDictionary(sql, 1, parms);
        }

        public List<Dictionary<string,object>> getMenus()
        {
            string sql = "Select MenuID,ParentID,MenuName,DisplayName,LnkAddress,Ordering from User_menu Order by Ordering Desc";
            object[] parms = { };
            return db.ReadDictionary(sql, 0, parms);
        }

    }
}
