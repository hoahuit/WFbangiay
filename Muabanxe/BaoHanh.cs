using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Muabanxe
{
    public partial class BaoHanh : Form
    {
        public BaoHanh()
        {
            InitializeComponent();
            conn = kn.conn;
            LoadData();
            SetDefaultButtonStates();
        }

        connect kn = new connect();
        SqlConnection conn;
        SqlCommand cmd;

        // Load dữ liệu từ database vào DataGridView
        private void LoadData()
        {
            try
            {
                string query = "SELECT * FROM BAOHANH";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                SetDefaultButtonStates(); // Reset trạng thái nút sau khi load dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // Reset trạng thái nút mặc định
        private void SetDefaultButtonStates()
        {
            btnthem.Enabled = true;  // Chỉ bật nút "Thêm"
            btnsua.Enabled = false;  // Tắt nút "Sửa"
            btn_xoa.Enabled = false; // Tắt nút "Xóa"
            btnluu.Enabled = false;  // Tắt nút "Lưu"
            txt1.Enabled = true;     // Cho phép nhập mã phiếu mới khi thêm
        }

        // Xóa nội dung các ô nhập liệu
        private void ClearInputs()
        {
            txt1.Clear();
            txt2.Clear();
            txt3.Clear();
            txt4.Clear();
            txt5.Clear();
            txt6.Clear();
        }
        private string currentAction = ""; // Lưu trạng thái hành động: "Add" hoặc "Edit"

        // Xử lý nút "Thêm"
        private void btnthem_Click(object sender, EventArgs e)
        {
            currentAction = "Add"; // Đặt trạng thái là Thêm
            btnluu.Enabled = true;
            btnthem.Enabled = false;
            btnsua.Enabled = false;
            btn_xoa.Enabled = false;
        }

        // Xử lý nút "Lưu"
        private void btnluu_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentAction == "Add") // Thêm mới
                {
                    // Logic thêm mới ở đây
                    string query = "INSERT INTO BAOHANH (MAPHIEUBH, MAXE, MANV, MAKH, THOIGIANBH, NGAYLAP) VALUES (@MAPHIEUBH, @MAXE, @MANV, @MAKH, @THOIGIANBH, @NGAYLAP)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MAPHIEUBH", txt1.Text);
                    cmd.Parameters.AddWithValue("@MAXE", txt2.Text);
                    cmd.Parameters.AddWithValue("@MANV", txt3.Text);
                    cmd.Parameters.AddWithValue("@MAKH", txt4.Text);
                    cmd.Parameters.AddWithValue("@THOIGIANBH", txt5.Text);
                    cmd.Parameters.AddWithValue("@NGAYLAP", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Thêm thành công!");
                }
                else if (currentAction == "Edit") // Sửa
                {
                    // Logic sửa ở đây
                    string query = "UPDATE BAOHANH SET MAXE = @MAXE, MANV = @MANV, MAKH = @MAKH, THOIGIANBH = @THOIGIANBH, NGAYLAP = @NGAYLAP WHERE MAPHIEUBH = @MAPHIEUBH";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MAPHIEUBH", txt1.Text);
                    cmd.Parameters.AddWithValue("@MAXE", txt2.Text);
                    cmd.Parameters.AddWithValue("@MANV", txt3.Text);
                    cmd.Parameters.AddWithValue("@MAKH", txt4.Text);
                    cmd.Parameters.AddWithValue("@THOIGIANBH", txt5.Text);
                    cmd.Parameters.AddWithValue("@NGAYLAP", txt6.Text);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã phiếu bảo hành để cập nhật.");
                    }
                }

                // Tải lại dữ liệu sau khi lưu
                LoadData();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                SetDefaultButtonStates(); // Reset trạng thái nút
                currentAction = ""; // Reset trạng thái sau khi lưu
            }
        }


        // Xử lý nút "Xóa"
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt1.Text))
                {
                    MessageBox.Show("Vui lòng chọn hoặc nhập mã phiếu bảo hành cần xóa!");
                    return;
                }

                string query = "DELETE FROM BAOHANH WHERE MAPHIEUBH = @MAPHIEUBH";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MAPHIEUBH", txt1.Text);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                if (result > 0)
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã phiếu bảo hành cần xóa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                SetDefaultButtonStates();
            }
        }

        // Xử lý nút "Sửa"
        private void btnsua_Click(object sender, EventArgs e)
        {
            currentAction = "Edit"; // Đặt trạng thái là Sửa
            txt1.Enabled = false;  // Không cho phép chỉnh sửa mã phiếu
            btnluu.Enabled = true;
            btnthem.Enabled = false;
            btn_xoa.Enabled = false;
        }

        // Xử lý khi chọn dòng trong DataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txt1.Text = row.Cells["MAPHIEUBH"].Value.ToString();
                txt2.Text = row.Cells["MAXE"].Value.ToString();
                txt3.Text = row.Cells["MANV"].Value.ToString();
                txt4.Text = row.Cells["MAKH"].Value.ToString();
                txt5.Text = row.Cells["THOIGIANBH"].Value.ToString();
                txt6.Text = row.Cells["NGAYLAP"].Value.ToString();

                btnsua.Enabled = true; // Bật nút "Sửa"
                btn_xoa.Enabled = true; // Bật nút "Xóa"
                btnthem.Enabled = true; // Bật nút "Thêm"
                btnluu.Enabled = false; // Tắt nút "Lưu"
            }
        }

        // Xử lý nút "Chi tiết"
        private void btnchitiet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt1.Text))
            {
                MessageBox.Show("Vui lòng chọn một mã phiếu bảo hành để xem chi tiết!");
                return;
            }

            ChiTietBaoHanh formChiTiet = new ChiTietBaoHanh();
            formChiTiet.MaPhieuBH = txt1.Text; // Truyền mã phiếu bảo hành
            formChiTiet.ShowDialog(); // Mở form chi tiết
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt1.Text))
                {
                    MessageBox.Show("Vui lòng nhập hoặc chọn mã phiếu bảo hành cần cập nhật!");
                    return;
                }

                // Chuẩn bị câu lệnh cập nhật
                string query = "UPDATE BAOHANH SET MAXE = @MAXE, MANV = @MANV, MAKH = @MAKH, THOIGIANBH = @THOIGIANBH, NGAYLAP = @NGAYLAP WHERE MAPHIEUBH = @MAPHIEUBH";
                cmd = new SqlCommand(query, conn);

                // Gán giá trị tham số từ các ô nhập liệu
                cmd.Parameters.AddWithValue("@MAPHIEUBH", txt1.Text);
                cmd.Parameters.AddWithValue("@MAXE", txt2.Text);
                cmd.Parameters.AddWithValue("@MANV", txt3.Text);
                cmd.Parameters.AddWithValue("@MAKH", txt4.Text);
                cmd.Parameters.AddWithValue("@THOIGIANBH", txt5.Text);
                cmd.Parameters.AddWithValue("@NGAYLAP", txt6.Text);

                // Thực thi câu lệnh
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                if (result > 0)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData(); // Tải lại dữ liệu sau khi cập nhật
                    ClearInputs(); // Xóa nội dung các ô nhập liệu
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã phiếu bảo hành để cập nhật.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                SetDefaultButtonStates(); // Reset trạng thái nút sau khi xử lý
            }
        }

    }
}
