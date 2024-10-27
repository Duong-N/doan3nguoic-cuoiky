using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quản_lý_nhà_trọ
{

    public partial class dang_nhap : Form
    {
        private string connectionString = "Data Source=XoF-PC;Initial Catalog=quanlynhatro;Integrated Security=True;";
        private int islogin = 1;
        private string pattern = @"^0[1-9][0-9]{8}$";
        public dang_nhap()
        {
            InitializeComponent();
            loadForm();
        }
        private void loadForm()
        {
            if (islogin == 1)
            {
                grb_chu.Visible = true;
                grb_quanly.Visible = false;
            }

            if (islogin == 2)
            {
                grb_quanly.Visible = true;
                grb_chu.Visible = false;
            }

}
        //Dang nhap chu
        private void btn_chu_Click(object sender, EventArgs e)
        {
            loadForm();
            islogin = 1;
        }
        private void btn_quanly_Click(object sender, EventArgs e)
        {
            islogin = 2;
            loadForm();
        }
        private void dangnhap_chu()
        {
           
            string sdt = txt_sdtchu.Text;
            string password = txt_passwordchu.Text;
            string gmail = txt_gmail.Text;
            if (!isvalidSDT(sdt))
            {
                ep_checkloi.SetError(txt_sdtchu, "khong dung dinh dang");
                return;
            }
            else if (!isvalidGmail(gmail))
            {
                ep_checkloi.SetError(txt_gmail, "khong dung dinh dang");
                return;
            }
            else if (!checkLoginChu(sdt,password,gmail))
            {
                ep_checkloi.SetError(txt_sdtchu, "tai khoan hoac mat khau khong dung");
                ep_checkloi.SetError(txt_passwordchu, "tai khoan hoac mat khau khong dung");
                ep_checkloi.SetError(txt_gmail, "tai khoan hoac mat khau khong dung");
                return;
            }
            else
            {
               
                home home = new home(islogin);
                home.Show();

            }

        }
        private bool isvalidSDT(string sdt)
        {
            return Regex.IsMatch(sdt, pattern);
        }
        private bool isvalidGmail(string gmail)
        {
            return Regex.IsMatch(gmail, @"^[^@\s]+@gmail\.com$");
        }
        private bool checkLoginChu(string sdt, string password,string gmail)
        {
            string truyvan = "select * from chu where sdt = @sdt and Password = @password and gmail = @gmail" ;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sql_chu = new SqlCommand(truyvan, connection);
                sql_chu.Parameters.AddWithValue("@sdt", sdt);
                sql_chu.Parameters.AddWithValue("@password", password);
                sql_chu.Parameters.AddWithValue("@gmail", gmail);
                connection.Open();
                Object result = sql_chu.ExecuteScalar();
                if (result != null)
                {
                    return true;
                }

            }
            return false;
        }

        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            ep_checkloi.Clear();
            if (islogin==1)
            {
                dangnhap_chu();
             
            }
        }

        private void btn_quenmk_Click(object sender, EventArgs e)
        {
            quen_mat_khau quenmk = new quen_mat_khau(islogin);
            quenmk.Show();
        }

       
    }
}
