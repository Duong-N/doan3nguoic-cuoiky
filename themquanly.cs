using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twilio.Rest.Api.V2010.Account.Call;

namespace quản_lý_nhà_trọ
{
    public partial class themquanly : Form
    {
        private string connectionString = "Data Source=XoF-PC;Initial Catalog=quanlynhatro;Integrated Security=True;";
        private int islogin;
        public themquanly(int isloginHome)
        {
            InitializeComponent();
            this.islogin = isloginHome;
            loadCombobox();
            loadForm();
        }
        //load cac form
        private void loadForm()
        {
            if (islogin == 1)
            {
                grb_themquanly.Visible = true;
            }
        }
        private void loadCombobox()
        {
            string truyvan = "select diachi from nhatro ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(truyvan, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cb_nhatro.Items.Add(reader["diachi"].ToString());
                }
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            if (islogin == 1)
            {
                addquanly();
            }
        }
        private void addquanly()
        {
            error.Clear();
            string name =  txt_tenql.Text;
            string gmail = txt_gmailql.Text;
            string sdt = txt_sdtql.Text;
            string matkhau = txt_matkhauql.Text;
            string diachi = cb_nhatro.Text;
            if (!isvalidname(name))
            {
                error.SetError(txt_tenql, "Ten khong hop le");
                return;
            }
            if (!isvalidGmail(gmail))
            {
                error.SetError(txt_gmailql, "Gmail khong hop le");
                return;
            }
            if (!isvalidSDT(sdt))
            {
                error.SetError(txt_sdtql, "SDT khong hop le");
                return;
            }
            if (!isvalidPass(matkhau))
            {
                error.SetError(txt_matkhauql, "Mat khau khong hop le");
                return;
            }
            else
            {
                string truyvan = "INSERT INTO quanly (ID_chu,tenquanly,sdt,matkhau,diachinhatro,gmail) values ('1',@tenquanly,@sdt,@matkhau,@diachinhatro,@gmail)";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    SqlCommand cmd = new SqlCommand(truyvan, connection);
                    cmd.Parameters.AddWithValue("@tenquanly", name);
                    cmd.Parameters.AddWithValue("@gmail", gmail);
                    cmd.Parameters.AddWithValue("@sdt", sdt);
                    cmd.Parameters.AddWithValue("@matkhau", matkhau);
                    cmd.Parameters.AddWithValue("@diachinhatro", diachi);
                    try
                    {
                        connection.Open();
                        int banghidung = cmd.ExecuteNonQuery(); 
                        if (banghidung > 0)
                        {
                            MessageBox.Show("Them quan ly thanh cong!");
                            this.Close();   
                        }
                        else
                        {
                            MessageBox.Show("Khong the them quan ly");
                        }          
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("ERROR:" + ex);
                    }
                    

                }
                    

                
                
            }
        }
        // check du lieu dau vao
        private bool isvalidname(string name)
        {
            return Regex.IsMatch(name, @"^[A-Za-zĐĐàáảãạâầấẩẫậêềếểễệôồốổỗộưừứửữự]+(\s[A-Za-zĐĐàáảãạâầấẩẫậêềếểễệôồốổỗộưừứửữ]+)*$");
        }
        private bool isvalidSDT(string sdt)
        {
            return Regex.IsMatch(sdt, @"^0[1-9][0-9]{8}$" );
        }
        private bool isvalidGmail(string gmail)
        {
            return Regex.IsMatch(gmail, @"^[^@\s]+@gmail\.com$");
        }
        private bool isvalidPass(string password)
        {
            return password.Length >= 8 && Regex.IsMatch(password, @"[a-z]") && Regex.IsMatch(password, @"[0-9]") && Regex.IsMatch(password, @"[\W_]");
        }
        //
    }
}
