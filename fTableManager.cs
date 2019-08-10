using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fTableManager : Form
    {
        private Account loginAccount;
  

        public Account LoginAccount
        {
            get
            {
                return loginAccount;
            }

            set
            {
                loginAccount = value; ChangeAccount(loginAccount.Type);
            }
        }

        public fTableManager(Account acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;

            LoadTable();
            LoadCategory();
            LoadComboboxTable(cbSwitchTable);
            LoadComboboxTableCross(cbGross);
        }
        #region Method

        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += "( " + loginAccount.DisplayName + " )";
        }

        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListByCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }

        void LoadFoodListByCategoryID(int id )
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }

        public void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            //for(int i=1; i <=20; i++)
            //{
            //    int idTable = i;
            //    int idBillInfo;
            //    int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(i);
            //    if(idBill > 0)
            //        idBillInfo = BillInfoDAO.Instance.GetUncheckBilLInfoByBillID(idBill);
            //    if (idBill > 0 && BillInfoDAO.Instance.GetUncheckBilLInfoByBillID(idBill) > 0)
            //        TableDAO.Instance.CheckStatusTableBeginDeleteFood(idBill, BillInfoDAO.Instance.GetUncheckBilLInfoByBillID(idBill), idTable);

            //}
               

                foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };

                btn.Text = item.Name + Environment.NewLine + Environment.NewLine + item.Status;

                btn.Click += btn_Click;

                btn.Tag = item;

                    switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.HotPink;
                        break;
                }

                flpTable.Controls.Add(btn);
            }
             
        }

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<QuanLyQuanCafe.DTO.Menu> listBilInfo =MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach(QuanLyQuanCafe.DTO.Menu item in listBilInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }

            //CultureInfo culture = new CultureInfo("vn-VN");

            //Thread.CurrentThread.CurrentCulture = culture;

            txbTotalPrice.Text = totalPrice.ToString("c");
        }

        void LoadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }
        void LoadComboboxTableCross(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableListCross();
            cb.DisplayMember = "Name";
        }

        #endregion

        #region Event

        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.UpdateAccount += F_UpdateAccount;
            f.ShowDialog();
        }

        private void F_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.loginAccount = LoginAccount;
            f.InsertFood += F_InsertFood;
            f.UpdateFood += F_UpdateFood;
            f.DeleteFood += F_DeleteFood;
            f.InsertFoodCategory += F_InsertFoodCategory;
            f.UpdateFoodCategory += F_UpdateFoodCategory;
            f.DeleteFoodCategory += F_DeleteFoodCategory;
            f.InsertTableFood += F_InsertTableFood;
            f.UpdateTableFood += F_UpdateTableFood;
            f.DeleteTableFood += F_DeleteTableFood;

            f.ShowDialog();
        }

        private void F_DeleteTableFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        private void F_UpdateTableFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        private void F_InsertTableFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        private void F_DeleteFoodCategory(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        private void F_UpdateFoodCategory(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        private void F_InsertFoodCategory(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        private void F_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        private void F_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        private void F_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;
            LoadFoodListByCategoryID(id);

        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
         
            try
            {
                int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
                int foodID = (cbFood.SelectedItem as Food).ID;
                int count = (int)nmFoodCount.Value;

                if (idBill == -1)
                {
                    BillDAO.Instance.InsertBill(table.ID);
                    BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count);
                }
                else
                {
                    BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);
                }

                ShowBill(table.ID);
                LoadTable();
            }
            catch
            {
                MessageBox.Show("Hãy chọn bàn!","Thông báo");
            }

        }


        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);

            int discount = (int)nmDiscount.Value;

            double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split(',')[0]);
            double finalPrice = totalPrice - (totalPrice/100)*discount;

            if(idBill != -1)
            {
                if (MessageBox.Show(string.Format("\t Bạn có chắc muốn thanh toán hóa đơn cho  {0}\n\n \t Tổng tiền \t: \t {1} \n \t Giảm giá \t\t: \t {2} % \n \t Thanh toán \t: \t {3} ", table.Name, totalPrice.ToString("c"), discount, finalPrice.ToString("c")), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    
                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalPrice);
                    ShowBill(table.ID);
                    LoadTable();
                }
            }
            
        }

        private void btnGross_Click(object sender, EventArgs e)
        {

            int id1 = (lsvBill.Tag as Table).ID;
            int id2 = (cbGross.SelectedItem as Table).ID;

            int idBill1 = BillDAO.Instance.GetUncheckBillIDByTableID(id1);
            int idBill2 = BillDAO.Instance.GetUncheckBillIDByTableID(id2);

            if (MessageBox.Show(string.Format("Chỉ gộp được bàn đã có người!\nBạn muốn gộp {0} vào {1} ?", (lsvBill.Tag as Table).Name, (cbGross.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (idBill1 > 0 && idBill2 > 0 && (id1 != id2)) TableDAO.Instance.GrossTable(idBill1, idBill2, id1);
                if (id1 == id2) MessageBox.Show("Không gộp được vì bàn trùng nhau, vui lòng xem lại!", "Thông báo");
            }

            LoadTable();

        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as Table).ID;
            int id2 = (cbSwitchTable.SelectedItem as Table).ID;

            int idBill1 = BillDAO.Instance.GetUncheckBillIDByTableID(id1);
            int idBill2 = BillDAO.Instance.GetUncheckBillIDByTableID(id2);

            int idBill = (idBill1 > 0) ? idBill1 : idBill2; 

            if (MessageBox.Show(string.Format("Bạn muốn đổi {0} với {1} ?", (lsvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (idBill1 > 0 && idBill2 > 0) TableDAO.Instance.SwitchTable(idBill1, idBill2);
                if ((idBill1 > 0 && idBill2 > 0) == false && (idBill1 > 0 || idBill2 > 0)) TableDAO.Instance.SwitchBillOfTable( id1 , id2 , idBill );                
            } 

            LoadTable();
            
        }

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCheckOut_Click(this, new EventArgs());
        }

        #endregion

    }

}
