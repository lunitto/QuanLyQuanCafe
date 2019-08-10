using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get
            {
                if (instance==null) instance = new FoodDAO(); return FoodDAO.instance;
            }

           private set
            {
               FoodDAO.instance = value;
            }
        }
        private FoodDAO() { }

        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> listFood = new List<Food>();
            string query = "select * from Food where idCategory =" + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);

            }

            return listFood;
        }
        public List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = new List<Food>();
            string query = string.Format("SELECT* FROM dbo.Food WHERE [dbo].[fuConvertToUnsign1](name) LIKE N'%' + [dbo].[fuConvertToUnsign1](N'{0}') + N'%'", name);
          
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);

            }

            return listFood;
        }
        public List<Food> GetFoodList()

        {
            List<Food> foodList = new List<Food>();

            DataTable data = DataProvider.Instance.ExecuteQuery("select * from dbo.food");

            foreach (DataRow item in data.Rows)
            {
                Food table = new Food(item);
                foodList.Add(table);
            }

            return foodList;
        }
        public DataTable GetListFood()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT f.name ,price, c.name,f.id FROM dbo.Food AS f, dbo.FoodCategory AS c WHERE c.id = f.idCategory");
        }

        public bool InsertFood(string name, int idCategory, float price)
        {
            string query = string.Format("INSERT dbo.Food(name, idCategory, price) VALUES(N'{0}', {1}, {2})",name, idCategory, price);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateFood(int idFood, string name, int idCategory, float price)
        {
            string query = string.Format("UPDATE dbo.Food SET name = N'{0}', idCategory = {1} , price = {2} WHERE id = {3}", name, idCategory, price,idFood);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteFood(int idFood)
        {
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFood);
            string query = string.Format("DELETE dbo.Food WHERE id = {0}", idFood);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public void DeleteFoodByCategoryID(int idCategory)
        {
            string query = string.Format("SELECT f.id FROM dbo.FoodCategory AS fc, dbo.Food AS f WHERE fc.id = f.idCategory  AND fc.id ={0}", idCategory);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            int result = 0;
            foreach(DataRow item in data.Rows)
            {
                dynamic idFood = item; 
                string queryFoodID = string.Format("DELETE dbo.Food WHERE id = {0}", idFood);
                DataProvider.Instance.ExecuteQuery(queryFoodID);
            }
          
        }

    }
}
