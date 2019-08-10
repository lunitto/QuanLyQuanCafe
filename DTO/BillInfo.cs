using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
   public class BillInfo
    {
        public BillInfo(int id, int iDBill, int foodID, int count)
        {
            this.ID = id;
            this.IDBill = iDBill;
            this.FoodID = foodID;
            this.Count = count;
        }

        public BillInfo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.IDBill = (int)row["iDBill"];
            this.FoodID = (int)row["IDfood"];
            this.Count = (int)row["count"];
        }

        private int count;
        private int foodID;

        private int iDBill;

        private int iD;

        public int ID
        {
            get
            {
                return iD;
            }

            set
            {
                iD = value;
            }
        }

        public int IDBill
        {
            get
            {
                return iDBill;
            }

            set
            {
                iDBill = value;
            }
        }

        public int FoodID
        {
            get
            {
                return foodID;
            }

            set
            {
                foodID = value;
            }
        }

        public int Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
            }
        }
    }
}
