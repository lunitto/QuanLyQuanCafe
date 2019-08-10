using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instace
        {
            get
            {
                if (instance == null) instance = new AccountDAO(); return instance;
            }

            private set
            {
                instance = value;
            }
        }

        private AccountDAO() { }

        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord ";

            DataTable result = DataProvider.Instance.ExecuteQuery(query,new object[]{userName, passWord});

            return result.Rows.Count >0;
        }

        public bool UpdateAccountInfo(string userName, string displayName, string pass, string newPass)
        {
            string query = string.Format("EXEC USP_UpdateAccount @userName , @disPlayName , @passWord , @newPassWord", userName, displayName, pass, newPass);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
           
            return result > 0;
        }
        public bool UpdateAccount(string userName, string displayName,int type)
        {
            string query = string.Format("update dbo.Account set displayname = n'{0}, type = {1} where username = n'{2}'", displayName, type, userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteAccount(string userName)
        {
            string query = string.Format("Delete dbo.Account where userName = {0}",userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool InsertAccount(string userName, string displayName, int type)
        {
            string query = string.Format("INSERT dbo.Account(userName, displayName, passWord, type) VALUES(N'{0}', N'{1}', N'0', {2})", userName, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool ResetPassAccount(string userName)
        {
            string query = string.Format("Update dbo.Account set password = 0 where username = N'{0}'", userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable data= DataProvider.Instance.ExecuteQuery("select * from account where userName = '" + userName +"'");
             
            foreach(DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }

        public List<Account> GetAccountList()
        {
            List<Account> listAccount = new List<Account>();
            string query = string.Format("SELECT * FROM dbo.Account AS a");

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            
            foreach(DataRow item in data.Rows)
            {
                Account table = new Account(item);
                listAccount.Add(table);
            }

            return listAccount;
        }

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT a.userName, a.displayName, a.type FROM dbo.Account AS a");
        }
    }
}
