using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using BusinessObjects;
using ServiceLayer.Interface;
using System.Security.Cryptography;

namespace ServiceLayer
{
    public class DTSecurityManagerSvc: IDTSecurityManagerSvc
    {
        static readonly IDAOFactory factory = DAOFactories.GetFactory();
        static readonly IDTSecurityManagerDAO DTSecurityManager = factory.DTSecurityManagerDAO;

        public int updateUserRoles(List<Roles> userRoles, string userAccountID)
        {
            return DTSecurityManager.updateUserRoles(userRoles, userAccountID);
        }
        public IEnumerable<Roles> getGrantedRoles(string ID)
        {
            return DTSecurityManager.getGrantedRoles(ID);
        }

        public IEnumerable<Roles> getNotGrantedRoles(string ID)
        {
            return DTSecurityManager.getNotGrantedRoles(ID);
        }

        public int UpdateStatusUserAccount(string ID, string Status)
        {
            return DTSecurityManager.UpdateStatusUserAccount(ID, Status);
        }
        public int ResetPasswordUserAccount(UserAccount UserAccount)
        {
            string encPassword = "";
            EncryptData(UserAccount.Code, UserAccount.Password, ref encPassword);
            UserAccount.Password = encPassword;
            UserAccount.PasswordID = Guid.NewGuid().ToString();
            return DTSecurityManager.ResetPasswordUserAccount(UserAccount);
        }

        public int UpdateUserAccounts(string ProcessType, UserAccount UserAccount)
        {
            if(ProcessType == "Add")
            {
                string encPassword = "";
                EncryptData(UserAccount.Code, UserAccount.Password, ref encPassword);
                UserAccount.Password = encPassword;
                UserAccount.PasswordID = Guid.NewGuid().ToString();
            }
            return DTSecurityManager.UpdateUserAccounts(ProcessType, UserAccount);
        }

        public UserAccount getUserAccountbyID(string ID)
        {
            return DTSecurityManager.getUserAccountbyID(ID).First();
        }

        public UserAccount getUserAccountbyCode(string Code)
        {
            return DTSecurityManager.getUserAccountbyCode(Code).First();
        }

        public List<Dictionary<string, object>> getUserAccounts()
        {
            return DTSecurityManager.getUserAccounts();
        }

        public IEnumerable<UserAccountStatus> getUserAccountStatus()
        {
            return DTSecurityManager.getUserAccountStatus();
        }

        public IEnumerable<Company> getListCompany()
        {
            return DTSecurityManager.getListCompany();
        }
        public IEnumerable<Position> getPosition()
        {
            return DTSecurityManager.getPosition();
        }

        public void EncryptData(string key, string data, ref string outData)
        {
            string tmpPassword;

            tmpPassword = key + "." + data;

            UTF8Encoding textConverter = new UTF8Encoding();
            byte[] passBytes = textConverter.GetBytes(tmpPassword);

            SHA384Managed sha = new SHA384Managed();
            //outData = System.Text.Encoding.UTF8.GetString(sha.ComputeHash(passBytes));
            outData = Convert.ToBase64String(sha.ComputeHash(passBytes));

            //outData = data;
        }
    }
}
