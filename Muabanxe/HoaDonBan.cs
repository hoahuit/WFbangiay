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
using System.Data.SqlClient;
using System.Data;

namespace Muabanxe
{
    public partial class HoaDonBan : Form
    {
        connect kn = new connect();
        SqlConnection conn;
        SqlCommand cmd;
        public HoaDonBan()
        {
            conn=kn.conn;
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        bool Kiemtra_KH()
        {
            string maKH=txt_makh.Text;
            bool result = false;
            string sql = "SELECT COUNT(*) FROM KHACHHANG WHERE MAKH = @maKH";
            SqlCommand commandCheck = new SqlCommand(sql, conn);
            commandCheck.Parameters.AddWithValue("@maKH", maKH);
            try
            {
                conn.Open();
                int count = (int)commandCheck.ExecuteScalar();
                result = (count > 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string maKH = txt_makh.Text;
            string tenKH = txt_name.Text;
            string gioiTinh = txt_gt.Text;
            string diaChi = txt_dc.Text;
            string sdt = txt_sdt.Text;

            string maNV = txt_manv.Text;
            string maHD = txt_mahd.Text;
            string date = txt_date.Text;

            

            if (!System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^\d+$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ.");
                return;
            }

            if (!Kiemtra_KH())
            {
                // Kiểm tra textbox trống
                if (string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(gioiTinh) ||
                string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(maHD) || string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(date))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng.");
                    return;
                }
                string sql1 = "insert into KHACHHANG(MAKH, TENKH, GIOITINH, DIACHI, SDT) VALUES(@maKH, @tenKH, @gioiTinh, @diaChi, @sdt)";
                SqlCommand command1 = new SqlCommand(sql1, conn);
                command1.Parameters.AddWithValue("@maKH", maKH);
                command1.Parameters.AddWithValue("@tenKH", tenKH);
                command1.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                command1.Parameters.AddWithValue("@diaChi", diaChi);
                command1.Parameters.AddWithValue("@sdt", sdt);

                try
                {
                    conn.Open();
                    int result1 = command1.ExecuteNonQuery();

                    // Kiểm tra xem thêm thành công hay không
                    if (result1 > 0)
                    {
                        MessageBox.Show("Thêm khách hàng thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Thêm khách hàng không thành công.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: Khách hàng đã tồn tại");
                }
                finally
                {
                    conn.Close();
                }
            }
            // Thêm vào bảng KHACHHANG
            

            // Thêm vào bảng HDXUAT
            string sql2 = "insert into HDXUAT(MAHDX, MANV, MAKH, NGAYXUAT) VALUES(@maHD, @maNV, @maKH, @date)";
            SqlCommand command2 = new SqlCommand(sql2, conn);
            command2.Parameters.AddWithValue("@maHD", maHD);
            command2.Parameters.AddWithValue("@maNV", maNV);
            command2.Parameters.AddWithValue("@maKH", maKH);  // Sử dụng chung maKH
            command2.Parameters.AddWithValue("@date", date);

            try
            {
                conn.Open();
                int result2 = command2.ExecuteNonQuery();

                // Kiểm tra xem thêm thành công hay không
                if (result2 > 0)
                {
                    MessageBox.Show("Thêm hóa đơn thành công!");
                }
                else
                {
                    MessageBox.Show("Thêm hóa đơn không thành công.");
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

        private void HoaDonBan_Load(object sender, EventArgs e)
        {

        }

        private void btn_chitiet_Click(object sender, EventArgs e)
        {
            ChitietHoadon chitietHoadon = new ChitietHoadon();
            chitietHoadon.Show();
            this.Hide();
        }
    }
}
