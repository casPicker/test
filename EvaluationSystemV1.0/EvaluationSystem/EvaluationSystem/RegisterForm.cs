using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EvaluationSystem.Service;
using EvaluationSystem.Service.ServiceImpl;
using EvaluationSystem.Commons;

namespace EvaluationSystem
{
    public partial class RegisterForm : XtraForm
    {
        private LoginService loginService;
        public RegisterForm()
        {
            InitializeComponent();
            this.loginService = new LoginServiceImpl();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnRegister;
            this.CancelButton = btnCancel;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            ValidateResult result = this.loginService.RegisterUser(txtUserName.Text.Trim(), txtPassword.Text, txtConfirmPwd.Text);
            if (result.IsOk)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(result.Message, "信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(result.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }
    }
}
