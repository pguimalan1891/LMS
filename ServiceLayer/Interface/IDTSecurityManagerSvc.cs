using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace ServiceLayer.Interface
{
    public interface IDTSecurityManagerSvc
    {
        int updateUserRoles(List<Roles> userRoles, string userAccountID);
        IEnumerable<Roles> getGrantedRoles(string ID);
        IEnumerable<Roles> getNotGrantedRoles(string ID);
        int UpdateStatusUserAccount(string ID, string Status);
        int ResetPasswordUserAccount(UserAccount UserAccount);
        UserAccount getUserAccountbyID(string ID);
        int UpdateUserAccounts(string ProcessType, UserAccount UserAccount);
        List<Dictionary<string, object>> getUserAccounts();
        IEnumerable<UserAccountStatus> getUserAccountStatus();
        IEnumerable<Company> getListCompany();
        IEnumerable<Position> getPosition();
        void EncryptData(string key, string data, ref string outData);
    }
}
