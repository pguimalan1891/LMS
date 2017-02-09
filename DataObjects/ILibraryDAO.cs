using System.Collections.Generic;

namespace DataObjects
{
    public interface ILibraryDAO
    {
        List<Dictionary<string, object>> getLibraryComponent(string DType);
        List<Dictionary<string, object>> getLibraryUpdateComponent(string DType);
        int updLibraryComponent(string DType, int OpCode, string Components);
    }
}
