using System;
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
        
        public ICustomerDAO CustomerDAO
        {
            get { return new CustomerDAO(); }
        }

        public IAccountDAO AccountDAO
        {
            get { return new AccountDAO(); }
        }

        public ILoanApplicationDAO LoanApplicationDAO
        {
            get { return new LoanApplicationDAO(); }
        }

        public IBookingDAO BookingDAO
        {
            get { return new BookingDAO(); }
        }

        public IAccountingDAO AccountingDAO
        {
            get { return new AccountingDAO(); }


        }

        public IMaintenanceAgentProfileDAO MaintenanceAgentProfileDAO
        {
            get { return new MaintenanceAgentProfileDAO(); }
        }

        public IDTSecurityManagerDAO DTSecurityManagerDAO
        {
            get { return new DTSecurityManagerDAO(); }
        }

        public IHomeDAO HomeDAO
        {
            get { return new HomeDAO(); }
        }

        public IOfficialReceiptDAO OfficialReceiptDAO
        {
            get { return new OfficialReceiptDAO(); }
        }

        public ICreditInvestigationDAO CreditInvestigationDAO
        {
            get { return new CreditInvestigationDAO(); }
        }
    }
}
