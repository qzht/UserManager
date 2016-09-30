using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XK.NBear.DB;

namespace UserManager
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");

        private void button2_Click(object sender, EventArgs e)
        {
            string oldpassword = txtoldpassword.Text;
            string newpassword = txtnewpassword.Text;
            string newpassword2 = txtnewpassword2.Text;

            if (string.IsNullOrWhiteSpace(oldpassword))
            {
                MessageBox.Show("请输入原密码");
                return;
            }
            if (string.IsNullOrWhiteSpace(newpassword))
            {
                MessageBox.Show("请输入新密码");
                return;
            }
            if (string.IsNullOrWhiteSpace(newpassword2))
            {
                MessageBox.Show("请重新输入新密码");
                return;
            }

            oldpassword = XK.NBear.Common.XinDES.Encrypt(oldpassword);
            string sql = " select count(*) from dbo.GuanLiUser where isLeave=0 and username='" + Common.userName + "' and password='" + oldpassword + "' ";
            string result = db.Scalar(sql).ToString();
            if (result != "1")
            {
                MessageBox.Show("输入密码不正确");
                return;
            }
            if (newpassword != newpassword2)
            {
                MessageBox.Show("输入两次新密码不一致");
                return;
            }
            newpassword = XK.NBear.Common.XinDES.Encrypt(newpassword);
            sql = " update GuanLiUser set password='" + newpassword + "' where username='" + Common.userName + "' ";
            int flag = db.NonQuery(sql);
            if (flag == 1)
            {
                MessageBox.Show("修改成功");
            }
            else
            {
                MessageBox.Show("修改失败");
            }
        }
    }
}
