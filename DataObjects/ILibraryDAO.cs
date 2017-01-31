using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataObjects
{
    public interface ILibraryDAO
    {
        List<Dictionary<string, object>> getLibraryComponent(string DType);
        List<Dictionary<string, object>> getLibraryUpdateComponent(string DType);
        int updLibraryComponent(string DType, int OpCode, string Components);
    }
}
