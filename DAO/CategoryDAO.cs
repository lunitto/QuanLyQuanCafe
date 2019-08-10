using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null) instance = new CategoryDAO(); return CategoryDAO. instance;
            }

            private set
            {
               CategoryDAO.instance = value;
            }
        }

        private CategoryDAO() { }

        public bool InsertCategory(string name)
        {
            string query = string.Format("INSERT dbo.FoodCategory(name) VALUES(N'{0}')", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateCategory(string name, int id)
        {
            string query = string.Format("Update dbo.FoodCategory set name = N'{0}' whereid = {1}", name, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteCategory( int idCategory)
        {
            int result = 0;
            FoodDAO.Instance.DeleteFoodByCategoryID(idCategory);
            
            string query = string.Format("Delete dbo.FoodCategory where id = {0}", idCategory);
            result = DataProvider.Instance.ExecuteNonQuery(query);
            
            return result > 0;
        }

        public List<Category> GetListByCategory()
        {
            List<Category> listCategory = new List<Category>();
            string query = "select * from FoodCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow item in data.Rows)
            {
                Category category = new Category(item);
                listCategory.Add(category);

            }

            return listCategory;
        }
        

        public Category GetCategoryByID(int id)
        {

            Category category = null;

            string query = "select * from FoodCategory where id = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                 category = new Category(item);

                 return category;  ;

            }

            return category;

        }

    }
}
