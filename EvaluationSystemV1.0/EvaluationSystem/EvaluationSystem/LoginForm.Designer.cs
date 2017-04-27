namespace EvaluationSystem
{
    partial class LoginForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lablRegisterTip = new System.Windows.Forms.Label();
            this.llblRegister = new System.Windows.Forms.LinkLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Font = new System.Drawing.Font("华文中宋", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserName.ForeColor = System.Drawing.SystemColors.Window;
            this.lblUserName.Location = new System.Drawing.Point(24, 16);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(86, 21);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "用户名：";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(105, 14);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(176, 21);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.Text = "zxx";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("华文中宋", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPassword.ForeColor = System.Drawing.SystemColors.Window;
            this.lblPassword.Location = new System.Drawing.Point(23, 55);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(79, 21);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "密  码：";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(105, 53);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(176, 21);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "zxx";
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(105, 89);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(206, 89);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lablRegisterTip
            // 
            this.lablRegisterTip.AutoSize = true;
            this.lablRegisterTip.BackColor = System.Drawing.Color.Transparent;
            this.lablRegisterTip.Font = new System.Drawing.Font("华文中宋", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lablRegisterTip.ForeColor = System.Drawing.SystemColors.Window;
            this.lablRegisterTip.Location = new System.Drawing.Point(25, 127);
            this.lablRegisterTip.Name = "lablRegisterTip";
            this.lablRegisterTip.Size = new System.Drawing.Size(100, 16);
            this.lablRegisterTip.TabIndex = 6;
            this.lablRegisterTip.Text = "还没有用户名?";
            // 
            // llblRegister
            // 
            this.llblRegister.AutoSize = true;
            this.llblRegister.BackColor = System.Drawing.Color.Transparent;
            this.llblRegister.Font = new System.Drawing.Font("华文中宋", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.llblRegister.Location = new System.Drawing.Point(131, 127);
            this.llblRegister.Name = "llblRegister";
            this.llblRegister.Size = new System.Drawing.Size(36, 16);
            this.llblRegister.TabIndex = 7;
            this.llblRegister.TabStop = true;
            this.llblRegister.Text = "注册";
            this.llblRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblRegister_LinkClicked);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.llblRegister);
            this.panel1.Controls.Add(this.lblUserName);
            this.panel1.Controls.Add(this.lablRegisterTip);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.lblPassword);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Location = new System.Drawing.Point(401, 281);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(316, 158);
            this.panel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("华文中宋", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(59, 397);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(295, 42);
            this.label1.TabIndex = 9;
            this.label1.Text = "中国科学院地理科学与资源研究所\r\n              Version1.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("华文中宋", 32.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(123, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(594, 98);
            this.label2.TabIndex = 10;
            this.label2.Text = "丝绸之路经济带边境口岸城市\r\n地缘战略优势度综合评估系统";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("华文中宋", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(364, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(353, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = " 中国科学院重点部署项目（ZDRW-ZS-2016-6-2）";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 479);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lablRegisterTip;
        private System.Windows.Forms.LinkLabel llblRegister;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

