using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Muabanxe
{
    public partial class Menuchucnang : Form
    {
        public Menuchucnang()
        {
            InitializeComponent();
        }

        public void Tat_CN()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button7.Enabled = false;

        }
        private void Menuchucnang_Load(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CapNhatKH capNhatKH = new CapNhatKH();
            capNhatKH.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            HoaDonBan hoaDonBan = new HoaDonBan();
            hoaDonBan.Show();
            this.Hide();
        }
    }
}
