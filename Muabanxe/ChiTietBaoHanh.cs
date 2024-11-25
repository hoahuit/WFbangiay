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
    public partial class ChiTietBaoHanh : Form
    {
        public string MaPhieuBH { get; set; } // Biến để nhận mã phiếu bảo hành từ Form khác

        public ChiTietBaoHanh()
        {
            InitializeComponent();
            conn = kn.conn;

        }

        connect kn = new connect();
        SqlConnection conn;
        SqlCommand cmd;
        private void ChiTietBaoHanh_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MaPhieuBH))
            {
                txt1.Text = MaPhieuBH; // Hiển thị mã phiếu bảo hành
                LoadChiTietBaoHanh(MaPhieuBH);
            }
        }
        private void LoadChiTietBaoHanh(string maPhieuBH)
        {
            try
            {
                string query = "SELECT * FROM CHITIETBH WHERE MAPHIEUBH = @MAPHIEUBH";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MAPHIEUBH", maPhieuBH);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Hiển thị dữ liệu trong DataGridView
                dataGridView1.DataSource = dt;

                // Nếu muốn hiển thị trong TextBox
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txt2.Text = row["NGAYBHLAN1"].ToString();
                    txt3.Text = row["NGAYBHLAN2"].ToString();
                    txt4.Text = row["NGAYBHLAN3"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chi tiết bảo hành cho mã phiếu này.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
