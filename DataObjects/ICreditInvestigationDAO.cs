using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface ICreditInvestigationDAO
    {
        IEnumerable<BusinessObjects.CRIncome> getIncome(string BorrowerID);
        IEnumerable<BusinessObjects.CreditInvestigation> getCRForm(string code);
        IEnumerable<BusinessObjects.LoanList> getLoanApplicationListing(string searchkey);
        string updateCI(string income, string deduction, string net_income, string spouse_income, string spouse_deduction, string spouse_net_income, string business_income, string other_income, string total_income, string living_expenses, string rentals, string utility, string education, string amortization, string transportation, string other_expenses, string total_expenses, string gross_disposable_income, string class_amount, string net_disposable_income, string mi_result, string excess_amount, string document_status_code, string notes, string loan_code, string recommended_mlv, string prepared_by);

    }
}
