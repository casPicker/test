using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EvaluationSystem.Entity;
using EvaluationSystem.Service;
using EvaluationSystem.Service.ServiceImpl;

namespace EvaluationSystem.ViewForm
{
    public partial class PwdChangeForm : Form
    {
        private LoginService loginService;
        public PwdChangeForm()
        {
            InitializeComponent();
            this.loginService = new LoginServiceImpl();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtPwd.Text.Equals(""))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("新密码不能为空！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                this.txtPwd.Focus();
                return;
            }
            if (this.txtPwdAgain.Text.Equals(""))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请再次确认密码！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                this.txtPwdAgain.Focus();
                return;
            }
            if(!this.txtPwd.Text.Equals(this.txtPwdAgain.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("两次输入的密码不一致！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                this.txtPwdAgain.Text = "";
                this.txtPwdAgain.Focus();
                return;
            }
            User curUser = EvaluationSystem.Util.SystemUtil.curUser;
            if (curUser == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请先登录系统！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            if (this.loginService.ChangePassword(curUser.UserName, this.txtPwd.Text))
            {
                this.Close();
                DevExpress.XtraEditors.XtraMessageBox.Show("密码修改成功！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else 
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("密码修改失败，请重新登录后尝试！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }
    }
}
