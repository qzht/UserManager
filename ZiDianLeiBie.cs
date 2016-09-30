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
    public partial class ZiDianLeiBie : Form
    {
        public ZiDianLeiBie()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        public void BindData()
        {
            string sql = "select Name,Remark from dbo.DictType ";
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
            string realName = txtrealName.Text;
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("名称不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(realName))
            {
                MessageBox.Show("备注不能为空");
                return;
            }
            string sql = " select count(*) from DictType where Name='" + username + "'";
            string result = db.Scalar(sql).ToString();
            if (result != "0")
            {
                MessageBox.Show("名称重复");
                return;
            }
            sql = " insert into DictType (Name,Remark) values(@userName,@realName) ";
            ParameterCollection p = new ParameterCollection();
            p.Add("@userName", username);
            p.Add("@realName", realName);
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
                string username = dataGridView1.Rows[index].Cells["Name"].Value.ToString();

                if (DialogResult.Yes == MessageBox.Show("确定删除该名称吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string sql = " delete DictType where Name='" + username + "'";
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
