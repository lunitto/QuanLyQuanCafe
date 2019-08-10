using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get
            {
                if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance;
            }

           private set
            {
               BillInfoDAO.instance = value;
            }
        }

        private BillInfoDAO() { }

        public int GetUncheckBilLInfoByBillID(int idBill)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.BillInfo WHERE id = " + idBill);

            if (data.Rows.Count > 0)
            {
                Bill billInfo = new Bill(data.Rows[0]);
                return billInfo.ID;
            }

            return -1;
        }
       
        public void DeleteBillInfoByFoodID(int id)
        {
            DataProvider.Instance.ExecuteQuery("delete dbo.BillInfo WHERE idFood =" + id);
        }

        
        public void DeleteBillInfoByTableID(int id)
        {
            string query = string.Format("SELECT b.id FROM dbo.Bill AS b, dbo.TableFood AS t WHERE b.idTable = {0}}", id);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            int result = 0;
            foreach (DataRow item in data.Rows)
            {
                dynamic idBill = item;
                string queryFoodID = string.Format("DELETE dbo.BillInfo WHERE idBill = {0}", idBill);
                DataProvider.Instance.ExecuteQuery(queryFoodID);
            }
            
        } 
        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.BillInfo WHERE idBill ="+id);

            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }

            return listBillInfo;
        }

        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("EXEC USP_InsertBillInfo @idBill , @idFood , @count ", new object[] { idBill, idFood, count});
        }
    }
}
