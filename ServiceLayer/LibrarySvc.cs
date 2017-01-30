using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using BusinessObjects;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class LibrarySvc : ILibrarySvc
    {
        static readonly IDAOFactory factory = DAOFactories.GetFactory();
        static readonly ILibraryDAO library = factory.LibraryDAO;

        public List<Dictionary<string, object>> getLibraryComponent(string DType)
        {
            var qryResult = library.getLibraryComponent(DType);
            List<Dictionary<string, object>> lstInfo = new List<Dictionary<string, object>>();
            foreach (var rec in qryResult)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                foreach(var x in rec)
                {
                    d.Add(x.Key, x.Value);
                }
                lstInfo.Add(d);
            }

            return lstInfo;
        }

        public List<Dictionary<string, object>> getLIbraryUpdateComponent(string updComponent)
        {
            var qryResult = library.getLibraryUpdateComponent(updComponent);
            List<Dictionary<string, object>> lstInfo = new List<Dictionary<string, object>>();
            foreach (var rec in qryResult)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                foreach (var x in rec)
                {
                    d.Add(x.Key, x.Value);
                }
                lstInfo.Add(d);
            }

            return lstInfo;
        }
    }
}
