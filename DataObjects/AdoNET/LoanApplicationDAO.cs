using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNET
{
    public class LoanApplicationDAO : ILoanApplicationDAO
    {
        public List<Dictionary<string, object>> GetBorrowers(string searchkey)
        {
            string sql = "" +
            "select pis.id,document_status_map.description as [Status], pis.code as [Code], pis.last_name as [Last Name], pis.first_name as [First Name], pis.middle_name as [Middle Name], Convert(varchar,pis.date_of_birth,101) as [Date of Birth], " +
            "gender.description as [Gender], civil_status.description as [Civil Status], pis_address.street_address as [Address], city.description as [City] " +
            "from Final_Testing.dbo.pis inner join Final_Testing.dbo.document_status_map   on (pis.document_status_code = document_status_map.code)  inner join Final_Testing.dbo.user_account prepared_by   on (pis.prepared_by_id = prepared_by.id)  inner join Final_Testing.dbo.gender   on (pis.gender_id = gender.id)  inner join Final_Testing.dbo.civil_status   on (pis.civil_status_id = civil_status.id)  left join Final_Testing.dbo.pis_address   on (pis_address.pis_id = pis.id and pis_address.address_type_id = '0')  inner join Final_Testing.dbo.city on(pis_address.city_id = city.id)  inner join Final_Testing.dbo.province   on(city.province_id = province.id)  inner join Final_Testing.dbo.organization   on(pis.organization_id = organization.id)  where pis.permission > 0 ";
            object[] parms = { };
            return db.ReadDictionary(sql, 0, parms);
        }
    }
}
