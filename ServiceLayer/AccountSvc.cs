using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using ServiceLayer.Interface;
using System.Security.Cryptography;

namespace ServiceLayer
{
    public class AccountSvc: IAccountSvc 
    {
        static readonly IDAOFactory factory = DAOFactories.GetFactory();
        static readonly IAccountDAO acctCtrl = factory.AccountDAO;

        public List<Dictionary<string, object>> Login(string username, string password)
        {
            string encPass = String.Empty;
            EncryptData(username, password, ref encPass);
            return acctCtrl.Login(username, encPass);
        }

        void EncryptData(string key, string data, ref string outData)
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
