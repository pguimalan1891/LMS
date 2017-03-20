using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface IDAOFactory
    {
        ILibraryDAO LibraryDAO { get; }
        ICustomerDAO CustomerDAO { get; }
        IBookingDAO BookingDAO { get; }

        IAccountingDAO AccountingDAO { get; }

        ILoanApplicationDAO LoanApplicationDAO { get; }
        IAccountDAO AccountDAO { get; }
        IMaintenanceAgentProfileDAO MaintenanceAgentProfileDAO { get;   }
        IDTSecurityManagerDAO DTSecurityManagerDAO { get; }
        IHomeDAO HomeDAO { get; }
    }
}
