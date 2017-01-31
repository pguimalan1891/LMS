using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace ServiceLayer.Interface
{
    public interface ILibrarySvc
    {
        List<Dictionary<string, object>> getLibraryComponent(string DType);

        List<Dictionary<string, object>> getLIbraryUpdateComponent(string updComponent);
        int updLibraryComponent(string DType, int OpCode, string Components);

    }
}
