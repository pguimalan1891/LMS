using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class DAOFactories
    {
        public static IDAOFactory GetFactory() {
            return new AdoNET.DAOFactory();
        }                   
    }
}
