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
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Muabanxe
{
    public partial class CapNhatKH : Form
    {
        connect kn = new connect();
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public CapNhatKH()
        {
            conn=kn.conn;
            InitializeComponent();
        }
        void LoadData()
        {
            // Chuẩn bị câu lệnh SQL để lấy dữ liệu
            string sql = "select MAKH, TENKH, GIOITINH, DIACHI, SDT from KHACHHANG";

            // Tạo đối tượng SqlCommand và SqlDataAdapter
            using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-FOASKO16\\SQLEXPRESS;Initial Catalog=QL_MBXE;Integrated Security=True"))  // Khởi tạo kết nối
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dataTable = new DataTable();  // Tạo DataTable để chứa dữ liệu

                try
                {
                    conn.Open();
                    adapter.Fill(dataTable);

                    // Kiểm tra nếu bảng không có dữ liệu
                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu khách hàng.");
                    }
                    else
                    {
                        // Gán DataTable làm nguồn dữ liệu cho DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }





        private void CapNhatKH_Load(object sender, EventArgs e)
        {
            txt_dc.Enabled = false;
            txt_gt.Enabled = false;
            txt_makh.Enabled = false;   
            txt_name.Enabled = false;
            txt_sdt.Enabled = false;
            LoadData();
            dataGridView1.Columns["MAKH"].Width = 150;   
            dataGridView1.Columns["TENKH"].Width = 250;  
            dataGridView1.Columns["GIOITINH"].Width = 100; 
            dataGridView1.Columns["DIACHI"].Width = 300;  
            dataGridView1.Columns["SDT"].Width = 145;
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            txt_dc.Enabled = true;
            txt_gt.Enabled = true;
            txt_makh.Enabled = true;
            txt_name.Enabled = true;
            txt_sdt.Enabled = true;
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string maKH = txt_makh.Text;
            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng cần xóa.");
                return;
            }
            string sql = "DELETE FROM KHACHHANG WHERE MAKH = @maKH";

            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@maKH", maKH);

            try
            {
                conn.Open();
                int result = command.ExecuteNonQuery();

                // Kiểm tra xem có xóa thành công không
                if (result > 0)
                {
                    MessageBox.Show("Xóa khách hàng thành công!");
                    LoadData(); // Tải lại dữ liệu sau khi xóa
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng để xóa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy dữ liệu từ dòng được chọn
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Đổ dữ liệu vào các TextBox
                txt_makh.Text = row.Cells["MAKH"].Value.ToString();
                txt_name.Text = row.Cells["TENKH"].Value.ToString();
                txt_gt.Text = row.Cells["GIOITINH"].Value.ToString();
                txt_dc.Text = row.Cells["DIACHI"].Value.ToString();
                txt_sdt.Text = row.Cells["SDT"].Value.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Xóa (clear) nội dung các TextBox
            txt_makh.Clear();
            txt_name.Clear();
            txt_gt.Clear();
            txt_dc.Clear();
            txt_sdt.Clear();

        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string maKH = txt_makh.Text;
            string tenKH = txt_name.Text;
            string gioiTinh = txt_gt.Text;
            string diaChi = txt_dc.Text;
            string sdt = txt_sdt.Text;

            // Kiểm tra xem các trường dữ liệu có rỗng không
            if (string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(gioiTinh) ||
                string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng.");
                return;
            }

            // Kiểm tra số điện thoại hợp lệ (chỉ chứa số)
            if (!System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^\d+$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ.");
                return;
            }

            string sql = "UPDATE KHACHHANG SET TENKH = @tenKH, GIOITINH = @gioiTinh, DIACHI = @diaChi, SDT = @sdt WHERE MAKH = @maKH";
            SqlCommand cmd = new SqlCommand(sql, conn);

            // Thêm tham số vào câu lệnh SQL
            cmd.Parameters.AddWithValue("@maKH", maKH);
            cmd.Parameters.AddWithValue("@tenKH", tenKH);
            cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
            cmd.Parameters.AddWithValue("@diaChi", diaChi);
            cmd.Parameters.AddWithValue("@sdt", sdt);

            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();

                // Kiểm tra kết quả
                if (result > 0)
                {
                    MessageBox.Show("Cập nhật khách hàng thành công!");
                    LoadData();  // Tải lại dữ liệu vào DataGridView
                }
                else
                {
                    MessageBox.Show("Cập nhật không thành công. Mã khách hàng không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Menuchucnang menuchucnang = new Menuchucnang();
            menuchucnang.Show();
            this.Hide();
        }
    }
}
