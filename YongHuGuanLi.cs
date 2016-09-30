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
    public partial class YongHuGuanLi : Form
    {
        public YongHuGuanLi()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        public void BindData()
        {
            string sql = "select userName,realName,Company from dbo.GuanLiUser where roleid !=0 and isLeave =0 ";
            dataGridView1.DataSource = db.ExecuteDataTable(sql);
        }

        private void YongHuGuanLi_Load(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string password = txtpassword.Text;
            string realName = txtrealName.Text;
            string company = txtCompany.Text;
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("账号不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(realName))
            {
                MessageBox.Show("真实姓名不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(company))
            {
                MessageBox.Show("收费单位不能为空");
                return;
            }
            if (username == "admin")
            {
                MessageBox.Show("此账号为系统管理员，请换个账号");
                return;
            }
            string sql = " select count(*) from GuanLiUser where userName='" + username + "'";
            string result = db.Scalar(sql).ToString();
            if (result != "0")
            {
                MessageBox.Show("账号重复，请换个账号");
                return;
            }
            password = XK.NBear.Common.XinDES.Encrypt(password);
            sql = " insert into GuanLiUser (userName,password,realName,RoleId,IsLeave,Company) values(@userName,@password,@realName,1,0,@Company) ";
            ParameterCollection p = new ParameterCollection();
            p.Add("@userName", username);
            p.Add("@password", password);
            p.Add("@realName", realName);
            p.Add("@Company", company);
            int flag = db.NonQuery(sql, p);
            if (flag == 1)
            {
                MessageBox.Show("添加成功");
                BindData();
            }
            else
            {
                MessageBox.Show("添加失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("无数据");
                return;
            }
            int index = dataGridView1.CurrentRow.Index;
            if (index == -1)
            {
                MessageBox.Show("无选择数据");
            }
            else
            {
                string username = dataGridView1.Rows[index].Cells["userName"].Value.ToString();

                if (Common.userName == username)
                {
                    MessageBox.Show("不能删除自己的账号");
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("确定删除该用户吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string sql = " delete GuanLiUser where userName='" + username + "'";
                    int flag = db.NonQuery(sql);
                    if (flag == 1)
                    {
                        MessageBox.Show("删除成功");
                        BindData();
                    }
                    else
                    {
                        MessageBox.Show("删除失败");
                    }
                }
            }
        }
    }
}
