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

namespace Muabanxe
{
    public partial class ChitietHoadon : Form
    {
        connect kn = new connect();
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public ChitietHoadon()
        {
            conn=kn.conn;
            InitializeComponent();
        }

        void LoadDsHoaDon()
        {
            string sql1 = "SELECT MAHDX, MANV, MAKH, NGAYXUAT FROM HDXUAT";
            DataTable dataTable1 = new DataTable();  // Tạo DataTable để chứa dữ liệu

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql1, conn))
                {
                    conn.Open();
                    adapter.Fill(dataTable1);

                    // Kiểm tra nếu bảng không có dữ liệu
                    if (dataTable1.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu.");
                    }
                    else
                    {
                        // Gán DataTable làm nguồn dữ liệu cho DataGridView
                        dataGridView1.DataSource = dataTable1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close(); // Đảm bảo kết nối được đóng
                }
            }
        }

        void LoadChiTiet()
        {
            string maHDX=txt_mahd.Text;
            DataTable dataTable2 = new DataTable();
            string sql2 = "select MAHDX, MAXE, SOLUONG, DONGIA, TONGTIEN from CHITIETHDX where MAHDX = @maHD";

            using (SqlCommand command = new SqlCommand(sql2, conn))
            {
                command.Parameters.AddWithValue("@maHD", maHDX);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    try
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        adapter.Fill(dataTable2);

                        if (dataTable2.Rows.Count == 0)
                        {
                            MessageBox.Show("Không có dữ liệu.");
                            txt_maxe.Enabled = true;
                            txt_sl.Enabled = true;
                            txt_dg.Enabled = true;
                        }
                        else
                        {
                            dataGridView2.DataSource = dataTable2;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }
        private void ChitietHoadon_Load(object sender, EventArgs e)
        {
            txt_mahd.Enabled = false;
            txt_maxe.Enabled = false;
            txt_dg.Enabled = false;
            txt_sl.Enabled = false;
            txt_tongtien.Enabled = false;
            LoadDsHoaDon();
            dataGridView1.Columns["NGAYXUAT"].Width = 200;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy dữ liệu từ dòng được chọn
                DataGridViewRow row1 = dataGridView1.Rows[e.RowIndex];
                txt_mahd.Text = row1.Cells["MAHDX"].Value.ToString();

            }
        }

        private void btn_xem_Click(object sender, EventArgs e)
        {

            LoadChiTiet();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            HoaDonBan hoaDonBan = new HoaDonBan();
            hoaDonBan.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string maHD=txt_mahd.Text;
            string maXe=txt_maxe.Text;
            int sl=int.Parse(txt_sl.Text);
            int dg= int.Parse(txt_dg.Text);
            int tongtien= sl*dg;
            string sql2 = "insert into CHITIETHDX(MAHDX, MAXE, SOLUONG, DONGIA, TONGTIEN) VALUES(@maHD, @maXE, @sl, @dg, @tongTien)";
            SqlCommand command2 = new SqlCommand(sql2, conn);
            command2.Parameters.AddWithValue("@maHD", maHD);
            command2.Parameters.AddWithValue("@maXE", maXe);
            command2.Parameters.AddWithValue("@sl", sl);  // Sử dụng chung maKH
            command2.Parameters.AddWithValue("@dg", dg);
            command2.Parameters.AddWithValue("@tongTien", tongtien);


            try
            {
                conn.Open();
                int result2 = command2.ExecuteNonQuery();

                // Kiểm tra xem thêm thành công hay không
                if (result2 > 0)
                {
                    MessageBox.Show("Thêm thành công!");
                    LoadChiTiet();
                }
                else
                {
                    MessageBox.Show("Thêm không thành công.");
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

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string maHD = txt_mahd.Text;
            string maXe=txt_maxe.Text;
            string sql = "delete from CHITIETHDX where MAHDX=@maHD and MAXE=@maXe";

            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@maHD", maHD);
            command.Parameters.AddWithValue("@maXe", maXe);

            try
            {
                conn.Open();
                int result = command.ExecuteNonQuery();

                // Kiểm tra xem có xóa thành công không
                if (result > 0)
                {
                    MessageBox.Show("Xóa thành công!");
                }
                else
                {
                    MessageBox.Show("Không tìm để xóa.");
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy dữ liệu từ dòng được chọn
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                // Đổ dữ liệu vào các TextBox
                txt_maxe.Text = row.Cells["MAXE"].Value.ToString();
                txt_sl.Text = row.Cells["SOLUONG"].Value.ToString();
                txt_dg.Text = row.Cells["DONGIA"].Value.ToString();
                txt_tongtien.Text = row.Cells["TONGTIEN"].Value.ToString();
            }
        }

        private void btn_xoahd_Click(object sender, EventArgs e)
        {
            string maHD = txt_mahd.Text;
            string maXe = txt_maxe.Text;

            // Xóa từ bảng CHITIETHDX trước
            string sql1 = "DELETE FROM CHITIETHDX WHERE MAHDX=@maHD AND MAXE=@maXe";
            using (SqlCommand command1 = new SqlCommand(sql1, conn))
            {
                command1.Parameters.AddWithValue("@maHD", maHD);
                command1.Parameters.AddWithValue("@maXe", maXe);

                try
                {
                    conn.Open();
                    int result1 = command1.ExecuteNonQuery();

                    // Kiểm tra xem có xóa thành công không
                    if (result1 > 0)
                    {
                        MessageBox.Show("Xóa chi tiết hóa đơn thành công!");
                        LoadChiTiet(); // Cập nhật lại dữ liệu sau khi xóa
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy chi tiết hóa đơn để xóa.");
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

            // Xóa từ bảng HDXUAT nếu không còn chi tiết nào trong bảng CHITIETHDX với mã hóa đơn này
            string sql2 = "DELETE FROM HDXUAT WHERE MAHDX=@maHD";
            using (SqlCommand command2 = new SqlCommand(sql2, conn))
            {
                command2.Parameters.AddWithValue("@maHD", maHD);

                try
                {
                    conn.Open();
                    int result2 = command2.ExecuteNonQuery();

                    // Kiểm tra xem có xóa thành công không
                    if (result2 > 0)
                    {
                        MessageBox.Show("Xóa hóa đơn thành công!");
                        LoadDsHoaDon();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy hóa đơn để xóa.");
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
        }
    }
}
