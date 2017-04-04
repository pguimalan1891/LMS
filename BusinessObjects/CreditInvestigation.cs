using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class CreditInvestigation
    {
        public string CINumber;
        public string ID;
        public string Date;

        public string Neighborhood;

        public string LivingConditions;

        public string RecommendedMLV;
        public string MonthlyInstallment;
        public string Income;
        public string Deduction;
        public string NetIncome;
        public string spouseIncome;
        public string spouseDeduction;
        public string spouseNetIncome;
        public string BusinessIncome;
        public string OtherIncome;
        public string TotalIncome;

        public string LivingExpenses;
        public string Rentals;
        public string LightWater;
        public string Education;
        public string Amortization;
        public string Transporatation;
        public string OtherExpense;
        public string TotalExpense;

        public string GDI;
        public string SummaryClass;
        public string NDI;
        public string SummaryMonthlyInstallment;
        public string Excess;


        public string Notes;

        public IEnumerable<InterviewedPersons> interviewedPersons;
        public IEnumerable<CreditStatus> creditStatus;
        public IEnumerable<RelativesNLApplicant> relativeNLApplicant;

    }

    public class InterviewedPersons
    {
        public string FullName;
        public string Relationship;
        public string FullAddress;
        public string ReactionNotes;

    }
    public class CreditStatus
    {
        public string Creditor;
        public string CreditType;
        public string PromissoryNote;
        public string Balance;
        public string Installments;
        public string Arrears;
        public string Remarks;
    }
    public class RelativesNLApplicant
    {
        public string FullName;
        public string Relationship;
        public string FullAddress;
    }
}
