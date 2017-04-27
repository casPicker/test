using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EvaluationSystem.Service;
using EvaluationSystem.Service.ServiceImpl;
using System.Security.Cryptography;
using EvaluationSystem.Commons;

namespace EvaluationSystem
{
    public partial class LoginForm : Form
    {
        private LoginService loginService;

        public LoginForm()
        {
            InitializeComponent();
            this.loginService = new LoginServiceImpl();
        }
        double step = 0.05;
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += step;
            if(this.Opacity == 1)
            {
                timer1.Stop();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(60, Color.White);
            this.CancelButton = btnCancel;
            this.AcceptButton = btnLogin;
            this.Opacity = 0;
            timer1.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ValidateResult result = this.loginService.ValidateUser(txtUserName.Text.Trim(), txtPassword.Text);
            if (!result.IsOk)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(result.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return;
            }
            else
            {
                timer1.Stop();//防止提前点击登录按钮
                this.Opacity = 0;//隐藏登录界面
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
        }

        private void llblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

    }
}
