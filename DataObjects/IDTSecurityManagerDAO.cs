using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataObjects
{
    public interface IDTSecurityManagerDAO
    {
        int updateUserRoles(List<Roles> userRoles, string userAccountID);
        IEnumerable<Roles> getGrantedRoles(string ID);
        IEnumerable<Roles> getNotGrantedRoles(string ID);
        int UpdateStatusUserAccount(string ID, string Status);
        int ResetPasswordUserAccount(UserAccount UserAccount);
        int UpdateUserAccounts(string ProcessType, UserAccount UserAccount);
        IEnumerable<UserAccount> getUserAccountbyID(string ID);
        IEnumerable<UserAccount> getUserAccountbyCode(string Code);
        List<Dictionary<string, object>> getUserAccounts();
        IEnumerable<UserAccountStatus> getUserAccountStatus();
        IEnumerable<Company> getListCompany();
        IEnumerable<Position> getPosition();

    }
}
