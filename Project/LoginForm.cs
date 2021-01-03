
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caffee_Cub_Management
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void BtLogin_Click(object sender, EventArgs e)
        {
            string userName = txbUsername.Text;
            string passWord = txbPassword.Text;
            string query = "SELECT * FROM TaiKhoan WHERE userName = '" + userName + "' AND passWord = '" + passWord + "' ";
            DataProvider provider = new DataProvider();
            DataTable result = provider.ExecuteQuery(query);
       
            if (result.Rows.Count > 0)
            {
                fTableManager f = new fTableManager();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
            }
        }

        private void BtExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
