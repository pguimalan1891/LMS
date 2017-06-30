using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Interface;
using DataObjects;


namespace ServiceLayer
{

    

    public class CreditInvestigationSvc : ICreditInvestigation
    {
        static readonly IDAOFactory factory = DAOFactories.GetFactory();
        static readonly ICreditInvestigationDAO lnCtrl = factory.CreditInvestigationDAO;
        static readonly ICustomerDAO customer = factory.CustomerDAO;
        public IEnumerable<BusinessObjects.CRIncome> getIncome(string BorrowerID)
        {
           return lnCtrl.getIncome(BorrowerID);
        }

        public BusinessObjects.CreditInvestigation getCRForm(string code)
        {
            return lnCtrl.getCRForm(code).First();
        }

        public IEnumerable<BusinessObjects.LoanList> getLoanApplicationListing()
        {
            return lnCtrl.getLoanApplicationListing("");
        }

        public string updateCI(string income, string deduction, string net_income, string spouse_income, string spouse_deduction, string spouse_net_income, string business_income, string other_income, string total_income, string living_expenses, string rentals, string utility, string education, string amortization, string transportation, string other_expenses, string total_expenses, string gross_disposable_income, string class_amount, string net_disposable_income, string mi_result, string excess_amount, string document_status_code, string notes, string loan_code, string recommended_mlv, string prepared_by)
        {

            return lnCtrl.updateCI( income,  deduction,  net_income,  spouse_income,  spouse_deduction,  spouse_net_income,  business_income,  other_income,  total_income,  living_expenses,  rentals,  utility,  education,  amortization,  transportation,  other_expenses,  total_expenses,  gross_disposable_income,  class_amount,  net_disposable_income,  mi_result,  excess_amount,  document_status_code,  notes,  loan_code,  recommended_mlv,  prepared_by);
        }
    }
}
