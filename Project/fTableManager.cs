using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caffee_Cub_Management
{
    public partial class fTableManager : Form
    {
        public fTableManager()
        {
            InitializeComponent();
            LoadTable();
            LoadDanhMuc();
        }

        List<Table> LoadTableList()
        {
            string query = "SELECT * FROM Ban";

            List<Table> tableList = new List<Table>();
            DataProvider provider = new DataProvider();
            DataTable data = provider.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }

        void LoadTable()
        {
            List<Table> tableList = LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = 100, Height = 100 };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Có người": btn.BackColor = Color.Orange; break;
                    default: btn.BackColor = Color.Blue; break;
                }
                flpTable.Controls.Add(btn);
            }
        }

        void LoadDanhMuc()
        {
            string query = "SELECT TenDanhMuc FROM DanhMuc";
            DataProvider provider = new DataProvider();
            DataTable data = provider.ExecuteQuery(query);

            cbbDanhmuc.DisplayMember = "TenDanhMuc";//Word là tên trường bạn muốn hiển thị trong combobox
            cbbDanhmuc.ValueMember = "TenDanhMuc";
            cbbDanhmuc.DataSource = data;
        }

        void btn_Click(object sender, EventArgs e)
        {
            string tableID = ((sender as Button).Tag as Table).ID;
            string query = "SELECT Mon.TenMon, CTHD.SoLuong, Mon.DonGia, Mon.DonGia* CTHD.SoLuong FROM Ban " +
                        "INNER JOIN HoaDon ON Ban.IDBan = HoaDon.IDBan " +
                        "INNER JOIN ChiTietHoaDon CTHD ON HoaDon.IDHoaDon = CTHD.IDHoaDon " + 
                        "INNER JOIN Mon ON CTHD.IDMon = Mon.IDMon WHERE Ban.IDBan = '" + tableID + "' ";
            DataProvider provider = new DataProvider();
            SqlDataReader data = provider.ExecuteReader(query);


            listviewCTHD.Items.Clear();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    ListViewItem tenmon = new ListViewItem(data.GetString(0));
                    ListViewItem.ListViewSubItem soluong = new ListViewItem.ListViewSubItem(tenmon, data[1].ToString());
                    ListViewItem.ListViewSubItem dongia = new ListViewItem.ListViewSubItem(tenmon, data[2].ToString());
                    ListViewItem.ListViewSubItem thanhtien = new ListViewItem.ListViewSubItem(tenmon, data[3].ToString());
                    tenmon.SubItems.Add(soluong);
                    tenmon.SubItems.Add(dongia);
                    tenmon.SubItems.Add(thanhtien);
                    listviewCTHD.Items.Add(tenmon);
                }
            }

            string totalPrice_query = "SELECT SUM(Mon.DonGia* CTHD.SoLuong) FROM Ban " +
                        "INNER JOIN HoaDon ON Ban.IDBan = HoaDon.IDBan " +
                        "INNER JOIN ChiTietHoaDon CTHD ON HoaDon.IDHoaDon = CTHD.IDHoaDon " +
                        "INNER JOIN Mon ON CTHD.IDMon = Mon.IDMon WHERE Ban.IDBan = '" + tableID + "' ";
            DataProvider totalPrice_provider = new DataProvider();
            object totalPrice = totalPrice_provider.ExecuteScalar(totalPrice_query);

            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;

            txbTongtien.Text = totalPrice.ToString();
        }

        private void CbbDanhmuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idDanhmuc = "DM" + (cbbDanhmuc.SelectedIndex + 1);
            ComboBox cbb = sender as ComboBox;

            string query = "SELECT TenMon FROM Mon INNER JOIN DanhMuc ON Mon.IDDanhMuc = DanhMuc.IDDanhMuc " +
                "WHERE DanhMuc.IDDanhMuc = '" + idDanhmuc + "' ";
            DataProvider provider = new DataProvider();
            SqlDataReader data = provider.ExecuteReader(query);

            cbbMon.Items.Clear();
            while (data.Read())
            {
                cbbMon.Items.Add(data.GetString(0));
            }
        }
    }
}
