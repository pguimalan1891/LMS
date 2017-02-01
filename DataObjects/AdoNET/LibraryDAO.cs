using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNET
{
    public class LibraryDAO : ILibraryDAO
    {
        static DB db = new DB();

        public List<Dictionary<string,object>> getLibraryComponent(string DType)
        {
            string sql = "dbo.usp_getDevelopmentToolsLibrary";
            object[] parms = { "@DType", DType };
            return db.ReadDictionary(sql, 1, parms);
        }

        public List<Dictionary<string, object>> getLibraryUpdateComponent(string DType)
        {
            string sql = "dbo.usp_getDevelopmentToolsLibrary";
            object[] parms = { "@DType", DType };
            return db.ReadDictionary(sql, 1, parms);
        }

        public int updLibraryComponent(string DType,int OpCode,string Components)
        {
            string sql = "dbo.usp_updDevelopmentToolsLibrary";
            object[] parms = { "DType", DType, "OpCode",OpCode, "Components",Components };
            int x = db.Scalar(sql, 1, parms).AsInt();
            return x;
        }

    }
}
