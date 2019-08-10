using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{

    public partial class fAdmin : Form
    {

        BindingSource foodList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource tableFoodList = new BindingSource();
        BindingSource accountList = new BindingSource();
        public Account loginAccount;

        public fAdmin()
        {
            InitializeComponent();
            Load();
        }


        #region Methods

        void Load()
        {
            dtgvFood.DataSource = foodList;
            dtgvCategory.DataSource = categoryList;
            dtgvTable.DataSource = tableFoodList;
            dtgvAccount.DataSource = accountList;

            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            //LoadCategoryIntoCombobox(cbFoodCategory);

            AddFoodBinding();
            AddCategoryBinding();
            AddTableFoodBinding();
            AddAccountBinding();

            LoadListCategory();
            LoadListTable();
            LoadListFood();
            LoadListAccount();

        }

        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID",true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
            cbFoodCategory.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name1", true, DataSourceUpdateMode.Never));
        }
        void AddCategoryBinding()
        {
            txbFoodCategory.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
        }

        void AddTableFoodBinding()
        {
            txbTableID.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            cbTableStatus.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Status", true, DataSourceUpdateMode.Never));
        }

        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            TxbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmAccountType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }

        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListByCategory();
            cb.DisplayMember = "Name";
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);

        }
        void LoadListFood()
        {
           // foodList.DataSource = FoodDAO.Instance.GetFoodList();
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
       
        void LoadListCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListByCategory();
                
        }

        void LoadListTable()
        {
            tableFoodList.DataSource = TableDAO.Instance.LoadTableList();
        }

        void LoadListAccount()
        {
            accountList.DataSource = AccountDAO.Instace.GetListAccount();
        }

        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);
            return listFood;
        }
        #endregion

        #region Event

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);

        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txtFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["IDCategory"].Value;
                    Category category = CategoryDAO.Instance.GetCategoryByID(id);

                    cbFoodCategory.SelectedItem = category;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbFoodCategory.SelectedIndex = index;
                }
            }
            catch
            { }
           
         }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn thêm món này ?"), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)    
            {
                string name = txbFoodName.Text;
                int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
                float price = (float)nmFoodPrice.Value;
                try
                {
                    if (FoodDAO.Instance.InsertFood(name, categoryID, price))
                    {
                        MessageBox.Show("Thêm món thành công !", "Thông báo");
                        LoadListFood();
                        if (insertFood != null)
                            insertFood(this, new EventArgs());
                    }

                }
                catch
                {
                    MessageBox.Show("Có lỗi khi thêm món !", "Thông báo");
                }
            }
               
           

        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(string.Format("Bạn có chắc muốn sửa món này ?"), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)  
            {
                try
                {
                    string name = txbFoodName.Text;
                    int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
                    float price = (float)nmFoodPrice.Value;
                    int idFood = Convert.ToInt32(txbFoodID.Text);

                    if (FoodDAO.Instance.UpdateFood(idFood, name, categoryID, price))
                    {
                        MessageBox.Show("Sửa món thành công !", "Thông báo");
                        LoadListFood();
                        if (updateFood != null)
                            updateFood(this, new EventArgs());
                    }

                }
                catch
                {
                    MessageBox.Show("Có lỗi khi sửa món !", "Thông báo");
                }
            }
             
                
        }


        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn xóa món này ?"),"Thông báo",MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK) 
                {
                int idFood = Convert.ToInt32(txbFoodID.Text);
                try
                {
                    if (FoodDAO.Instance.DeleteFood(idFood))
                    {
                        MessageBox.Show("Xóa món thành công!", "Thông báo");
                        LoadListFood();

                        if (deleteFood != null)
                            deleteFood(this, new EventArgs());
                    }

                }
                catch
                {
                    MessageBox.Show("Có lỗi khi xóa món!", "Thông báo");
                }

            }
               
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }


        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn thêm loại món này ?"), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                string name = txbFoodName.Text;
                try
                {
                    if (CategoryDAO.Instance.InsertCategory(name))
                    {
                        MessageBox.Show("Thêm loại món thành công !", "Thông báo");
                        LoadListCategory();
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi khi thêm loại món !", "Thông báo");
                }
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn sửa loại món ?"), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                string name = txbFoodName.Text;
                int id = Convert.ToInt32(txbCategoryID.Text);
                try
                {
                    if (CategoryDAO.Instance.UpdateCategory(name,id))
                    {
                        MessageBox.Show("Sửa loại món thành công !", "Thông báo");
                        LoadListCategory();
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi khi sửa loại món !", "Thông báo");
                }
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn xóa món này ?"), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                int id = Convert.ToInt32(txbCategoryID.Text);
                try
                {
                    if (CategoryDAO.Instance.DeleteCategory(id))
                    {
                        MessageBox.Show("Xóa loại món thành công !", "Thông báo");
                        LoadListCategory();
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi khi xóa loại món !", "Thông báo");
                }
            }
        }

        private event EventHandler insertFoodCategory;
        public event EventHandler InsertFoodCategory
        {
            add { insertFoodCategory += value; }
            remove { insertFoodCategory -= value; }
        }

        private event EventHandler updateFoodCategory;
        public event EventHandler UpdateFoodCategory
        {
            add { updateFoodCategory += value; }
            remove { updateFoodCategory -= value; }
        }

        private event EventHandler deleteFoodCategory;
        public event EventHandler DeleteFoodCategory
        {
            add { deleteFoodCategory += value; }
            remove { deleteFoodCategory -= value; }
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn thêm bàn chứ ?"), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                string name = txbFoodName.Text;
                string status = cbTableStatus.Text;
                try
                {
                    if (TableDAO.Instance.InsertTable(name, status))
                    {
                        MessageBox.Show("Thêm bàn thành công !", "Thông báo");
                        LoadListTable();
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi khi thêm bàn !", "Thông báo");
                }
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(string.Format("Bạn có chắc muốn sửa bàn chứ ?"), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                string name = txbFoodName.Text;
                string status = cbTableStatus.Text;
                int idTable = Convert.ToInt32(txbTableID.Text);
                try
                {
                    if (TableDAO.Instance.DeleteTable(idTable))
                    {
                        MessageBox.Show("Xóa bàn thành công !", "Thông báo");
                        LoadListTable();
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi khi xóa bàn !", "Thông báo");
                }
            }
        }

        private event EventHandler insertTableFood;
        public event EventHandler InsertTableFood
        {
            add { insertTableFood += value; }
            remove { insertTableFood -= value; }
        }

        private event EventHandler updateTableFood;
        public event EventHandler UpdateTableFood
        {
            add { updateTableFood += value; }
            remove { updateTableFood -= value; }
        }

        private event EventHandler deleteTableFood;
        public event EventHandler DeleteTableFood
        {
            add { deleteTableFood += value; }
            remove { deleteTableFood -= value; }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
           foodList.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadListAccount();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(string.Format("Bạn muốn thêm tài khoản này ?"), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                string userName = txbUserName.Text;
                string displayName = TxbDisplayName.Text;
                int type = (int)nmAccountType.Value;
                try
                {
                    if (AccountDAO.Instace.InsertAccount(userName, displayName, type))
                    {
                        MessageBox.Show("Thêm tài khoản thành công !", "Thông báo");
                        LoadListAccount();
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi khi thêm loại món !", "Thông báo");
                }
            }
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn muốn cập nhật tài khoản này ?"), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                string userName = txbUserName.Text;
                string displayName = TxbDisplayName.Text;
                int type = (int)nmAccountType.Value;
                try
                {
                    if (AccountDAO.Instace.UpdateAccount(userName, displayName, type))
                    {
                        MessageBox.Show("Cập nhật tài khoản thành công !", "Thông báo");
                        LoadListAccount();
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi khi cập nhật tài khoản !", "Thông báo");
                }
            }
        }
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            if (loginAccount.UserName.Equals(userName) || loginAccount.UserName.Equals("admin"))
            {
                MessageBox.Show("Không được xóa!!!");
                return;
            }
            if (MessageBox.Show(string.Format("Bạn muốn xóa khoản này ?"), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {     
                try
                {
                    if (AccountDAO.Instace.DeleteAccount(userName))
                    {
                        MessageBox.Show("Xóa tài khoản thành công !", "Thông báo");
                        LoadListAccount();
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi khi xóa tài khoản !", "Thông báo");
                }
            }
        }

        private void btnResetPassWord_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            try
            {
                if (AccountDAO.Instace.ResetPassAccount(userName))
                {
                    MessageBox.Show("Đặt lại mật khẩu thành công !", "Thông báo");
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi, hãy xem lại !", "Thông báo");
            }
        }

        #endregion


    }
}
