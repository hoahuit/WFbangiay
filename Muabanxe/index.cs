using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace Muabanxe
{
    public partial class index : Form
    {
        connect kn=new connect();
        SqlConnection conn;
        SqlCommand cmd;
        public index()
        {
            InitializeComponent();
            conn = kn.conn;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bt_thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit ?","Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        bool Login(string username, string password)
        {
            bool result = false;
            string cmd = "SELECT COUNT(1) FROM TAIKHOAN WHERE USENAME=@username AND PASSWORD=@password";

            SqlCommand command = new SqlCommand(cmd, conn);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            try
            {
                conn.Open();
                int count = (int)command.ExecuteScalar();

                // Kiểm tra nếu tài khoản tồn tại
                result = (count == 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
            finally
            { 
                conn.Close();
            }
            return result;
        }

        string roleConform(string username, string password)
        {
            string role = string.Empty;
            string cmd = "SELECT ROLE FROM TAIKHOAN WHERE USENAME=@username and PASSWORD=@password";
            SqlCommand command = new SqlCommand(cmd, conn);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);


                conn.Open();
                object result = command.ExecuteScalar();

                // Kiểm tra nếu tài khoản tồn tại và lấy vai trò
                if (result != null)
                {
                    role = result.ToString();
                }
                conn.Close();
           
            return role;
        }
        private void bt_dangnhap_Click(object sender, EventArgs e)
        {
            string username = txt_name.Text;
            string password = txt_pass.Text;
            if(nv.Checked==true )
            {
                if (Login(username, password) && roleConform(username, password) == "user")
                {
                    MessageBox.Show("Login USER Success");
                    
                    // Mở form chính hoặc thực hiện các hành động khác
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.");
                }
                Menuchucnang menuchucnang = new Menuchucnang();
                menuchucnang.Tat_CN();
                menuchucnang.Hide();
                menuchucnang.Show();
                this.Hide();
            }
            if(ql.Checked==true )
            {
                if (Login(username, password) && roleConform(username, password) == "admin")
                {
                    MessageBox.Show("Login ADMIN Success");
                    Menuchucnang menuchucnang = new Menuchucnang();
                    menuchucnang.Hide();
                    menuchucnang.Show();
                    this.Hide();
                    // Mở form chính hoặc thực hiện các hành động khác
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.");
                }
            }
        }
    }
}
