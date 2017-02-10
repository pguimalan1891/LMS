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
            return library.getLibraryComponent(DType);
        }

        public List<Dictionary<string, object>> getLIbraryUpdateComponent(string updComponent)
        {            
            return library.getLibraryUpdateComponent(updComponent);
        }

        public int updLibraryComponent(string DType, int OpCode, string Components)
        {
            int x = library.updLibraryComponent(DType, OpCode, Components);
            return x;
        }
    }
}
