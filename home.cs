using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quản_lý_nhà_trọ
{
    public partial class home : Form
    {
        private int isloginHome;
        public home(int islogin)
        {
            
            InitializeComponent();
            this.isloginHome = islogin;
            homeload(); 
        }

        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            dang_nhap dang_Nhap = new dang_nhap();
            dang_Nhap.Show();
        }
        private void homeload()
        {
            if ( isloginHome == 1)
            {
                btn_themquanly.Visible = true;
                btn_themquanly.Text = "THEM QUAN LY";
                
            }
        }

        private void btn_themquanly_Click(object sender, EventArgs e)
        {
            themquanly themquanly = new themquanly(isloginHome);
            themquanly.Show();
                
        }

    }
}
