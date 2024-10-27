using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace quản_lý_nhà_trọ
{
    public partial class quen_mat_khau : Form
    {

        private string saveOtp;
        private int islogin;
        private string connectionString = "Data Source=XoF-PC;Initial Catalog=quanlynhatro;Integrated Security=True;";

        public quen_mat_khau(int islogin)
        {
            this.islogin = islogin;
            InitializeComponent();
        }
        // tao otp
        private string createOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }


        private void SendOtpEmail(string userEmail, string otp)
        {
            try
            {
                // Tạo đối tượng đại diện cho địa chỉ email gửi đi và tên người gửi
                var fromAddress = new MailAddress("tinhluc2@gmail.com");

                // Tạo đối tượng đại diện cho địa chỉ email người nhận và tên người nhận
                var toAddress = new MailAddress(txt_gmail.ToString());

                // Mật khẩu ứng dụng của địa chỉ email gửi đi (thay thế bằng mật khẩu ứng dụng thực tế)
                const string fromPassword = "axqnsafslczyhcuy";

                // Tiêu đề của email
                const string subject = "Mã OTP tạo mới mật khẩu";

                // Nội dung của email, bao gồm mã OTP
                string body = $"Mã OTP của bạn là: {otp}";

                // Tạo và cấu hình đối tượng SmtpClient để gửi email qua máy chủ SMTP của Gmail
                var smtp = new SmtpClient
                {
                    // Địa chỉ máy chủ SMTP của Gmail
                    Host = "smtp.gmail.com",

                    // Cổng SMTP để gửi email
                    Port = 587,

                    // Bật SSL/TLS để mã hóa kết nối với máy chủ SMTP
                    EnableSsl = true,

                    // Phương thức gửi email qua mạng
                    DeliveryMethod = SmtpDeliveryMethod.Network,

                    // Không sử dụng thông tin đăng nhập mặc định
                    UseDefaultCredentials = false,

                    // Cung cấp thông tin đăng nhập (địa chỉ email và mật khẩu ứng dụng) để xác thực với máy chủ SMTP
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                // Tạo đối tượng MailMessage đại diện cho email gửi đi
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    // Đặt tiêu đề của email
                    Subject = subject,

                    // Đặt nội dung của email
                    Body = body
                })
                {
                    // Gửi email bằng phương thức Send của đối tượng SmtpClient
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi xảy ra trong khối try, hiển thị thông báo lỗi
                MessageBox.Show($"Không thể gửi email: {ex.Message}");
            }
        }
        //
        private void load()
        {
            if (islogin == 1)
            {
                grp_quenmkchu.Visible = true;
            }
        }



        private bool isvalidGmail(string gmail)
        {
            return Regex.IsMatch(gmail, @"^[^@\s]+@gmail\.com$");
        }

        private bool isvalidSDT(string sdt)
        {
            return Regex.IsMatch(sdt, @"^0[1-9][0-9]{8}$");
        }

        private void btn_GETOPTCHU_Click(object sender, EventArgs e)
        {
            erp_quenmk.Clear();
            string sdt = txt_sdt.Text;
            string newPass = txt_mkmoi.Text;
            string renewPass = txt_nhaplai.Text;
            string gmail = txt_gmail.Text;
            checkInformation(sdt, gmail, newPass, renewPass);
            if (isvalidGmail(gmail))
            {
                saveOtp = createOtp();
                SendOtpEmail(gmail, saveOtp);
                MessageBox.Show("Da gui otp toi gmail cua ban");

            }
            else { MessageBox.Show("Nhap sai gmail"); }

        }

        private void checkInformation(string sdt, string gmail, string newPass, string renewPass)
        {
            if (!isvalidSDT(sdt))
            {
                erp_quenmk.SetError(txt_sdt, "Số điện thoại không đúng định dạng.");
                return;
            }

            else if (!checksdt(sdt))
            {
                erp_quenmk.SetError(txt_sdt, "Không tìm thấy số điện thoại.");
                return;
            }
            else if (!isvalidGmail(gmail))
            {
                erp_quenmk.SetError(txt_gmail, "Không dung dinh dang.");
                return;
            }
            else if (!checkgmail(gmail))
            {
                erp_quenmk.SetError(txt_gmail, "Không tim thay gmail.");
                return;
            }
            else if (!checkPassword(newPass))
            {
                erp_quenmk.SetError(txt_mkmoi, "Mật khẩu không đúng định dạng.");
                return;
            }
            else if (renewPass != newPass)
            {
                erp_quenmk.SetError(txt_nhaplai, "Mật khẩu nhập lại không đúng.");
                return;
            }

        }
     
        private bool checksdt(string sdt)
        {
            string truyvan = "SELECT * FROM chu WHERE sdt = @sdt";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sql_chu = new SqlCommand(truyvan, connection);
                sql_chu.Parameters.AddWithValue("@sdt", sdt);
                connection.Open();
                Object result = sql_chu.ExecuteScalar();
                return result != null;
            }
        }
        private bool checkgmail(string gmail)
        {
            string truyvan = "SELECT * FROM chu WHERE gmail = @gmail";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sql_chu = new SqlCommand(truyvan, connection);
                sql_chu.Parameters.AddWithValue("@gmail", gmail);
                connection.Open();
                Object result = sql_chu.ExecuteScalar();
                return result != null;
            }
        }

        private bool checkPassword(string password)
        {
            return password.Length >= 8 && Regex.IsMatch(password, @"[a-z]") && Regex.IsMatch(password, @"[0-9]") && Regex.IsMatch(password, @"[\W_]");
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (islogin == 1)
            {
                erp_quenmk.Clear();
                string sdt = txt_sdt.Text;
                string newPass = txt_mkmoi.Text;
                string renewPass = txt_nhaplai.Text;
                string gmail = txt_gmail.Text;
                string otp = txt_OTP.Text;
                checkInformation(sdt, gmail, newPass, renewPass);
                if (otp != saveOtp)
                {
                    MessageBox.Show("OTP sai");
                    return;
                }
                else
                {
                    string truyvan = "update chu set Password = @renewPass where sdt = @sdt and gmail = @gmail";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand quenmk = new SqlCommand(truyvan, conn);
                        quenmk.Parameters.AddWithValue("@renewPass",renewPass);
                        quenmk.Parameters.AddWithValue("@sdt", sdt);
                        quenmk.Parameters.AddWithValue("@gmail", gmail);
                        conn.Open();
                        int banghidung = quenmk.ExecuteNonQuery();
                        if (banghidung > 0)
                        {
                            MessageBox.Show("Cap nhat mat khau thanh cong");
                            this.Close();
                        }
                        else { MessageBox.Show("Khong tim thay nguoi dung"); }
                        
                    }
                }
            }
            
        }
    }
}