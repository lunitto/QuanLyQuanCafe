using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get
            {
                if (instance == null) instance = new TableDAO(); return TableDAO.instance;
            }

            private set
            {
               TableDAO.instance = value;
            }
        }

        public static int TableWidth = 85;
        public static int TableHeight = 85;

        private TableDAO() { }

        public bool DeleteTable(int idTable)
        {
            int result = 0;
            BillInfoDAO.Instance.DeleteBillInfoByTableID(idTable);
            string query = string.Format("Delete dbo.Table whereid = {0}", idTable);
            result = DataProvider.Instance.ExecuteNonQuery(query);
            
            return result > 0;
        }
        public bool InsertTable(string name, string status)
        {
            string query = string.Format("INSERT dbo.TableFood ( name, status ) VALUES  ( N'{0}', N'{1}' )", name, status);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateTable(string name, string status, int id)
        {
            string query = string.Format("UPDATE dbo.TableFood SET name = N'{0}', status = {1} where id = {2}", name, status, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public void GrossTable(int idBill1, int idBill2, int idTable)
        {
            DataProvider.Instance.ExecuteQuery(" EXEC USP_GrossTable @idBillGoss1 , @idBillGoss2 , @idTableGoss1", new object[] { idBill1, idBill2, idTable });
        }

        public void SwitchBillOfTable(int id1, int id2, int idBill)
        {
            DataProvider.Instance.ExecuteQuery(" EXEC USP_SwitchBillOffTable @idTable1 , @idTable2 , @idBill", new object[] { id1, id2, idBill });
        }

        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery(" EXEC USP_SwitchTable @idFirstTable , @idSecondTable", new object[] { id1, id2 });
        }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery(" dbo.USP_GetTableList");

            foreach(DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
               
            }

            return tableList; 
        }

        public List<Table> LoadTableListCross()
        {
            List<Table> tableList = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery(" dbo.USP_GetTableList");

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                if (table.Status == "Có người")
                tableList.Add(table);
            }

            return tableList;
        }
        


    }
}
