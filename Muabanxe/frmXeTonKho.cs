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

namespace Muabanxe
{
    public partial class frmXeTonKho : Form
    {
        public frmXeTonKho()
        {
            InitializeComponent();
            conn = kn.conn;

        }

        connect kn = new connect();
        SqlConnection conn;
        SqlCommand cmd;
        private void btnthem_Click(object sender, EventArgs e)
        {

            try
            {
                string query = "SELECT MAXE, TENXE, HANGXE, MAUXE, DUNGTICH, GIANHAP, GIABAN FROM XE WHERE TINHTRANG = N'Còn'";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);

            }
        }
    }
}