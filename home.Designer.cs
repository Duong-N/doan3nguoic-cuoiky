namespace quản_lý_nhà_trọ
{
    partial class home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_dangnhap = new System.Windows.Forms.Button();
            this.btn_themquanly = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_dangnhap
            // 
            this.btn_dangnhap.Location = new System.Drawing.Point(562, 292);
            this.btn_dangnhap.Name = "btn_dangnhap";
            this.btn_dangnhap.Size = new System.Drawing.Size(107, 36);
            this.btn_dangnhap.TabIndex = 0;
            this.btn_dangnhap.Text = "ĐĂNG NHẬP";
            this.btn_dangnhap.UseVisualStyleBackColor = true;
            this.btn_dangnhap.Click += new System.EventHandler(this.btn_dangnhap_Click);
            // 
            // btn_themquanly
            // 
            this.btn_themquanly.Location = new System.Drawing.Point(379, 292);
            this.btn_themquanly.Name = "btn_themquanly";
            this.btn_themquanly.Size = new System.Drawing.Size(131, 36);
            this.btn_themquanly.TabIndex = 1;
            this.btn_themquanly.UseVisualStyleBackColor = true;
            this.btn_themquanly.Visible = false;
            this.btn_themquanly.Click += new System.EventHandler(this.btn_themquanly_Click);
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 543);
            this.Controls.Add(this.btn_themquanly);
            this.Controls.Add(this.btn_dangnhap);
            this.Name = "home";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_dangnhap;
        private System.Windows.Forms.Button btn_themquanly;
    }
}

