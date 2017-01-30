﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNET
{
    public class DAOFactory : IDAOFactory
    {
        public ILibraryDAO LibraryDAO
        {
            get { return new LibraryDAO(); }
        }
    }
}