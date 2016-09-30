using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XK.NBear.DB;
using System.Text.RegularExpressions;

namespace UserManager
{
    public partial class ZiDianXiangMu : Form
    {
        public ZiDianXiangMu()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        public void BindData()
        {
            string sql = "select Name,id from dbo.DictType ";
            cbbtype.ValueMember = "id";
            cbbtype.DisplayMember = "Name";
            cbbtype.DataSource = db.ExecuteDataTable(sql);
        }
        public void BindgwData()
        {
            string cbbid = (cbbtype.SelectedValue ?? "").ToString();

            if (!string.IsNullOrWhiteSpace(cbbid))
            {
                string sql = " select id,Name,Value,Seq from DictData where DicTypeID='" + cbbid + "'";
                dataGridView1.DataSource = db.ExecuteDataTable(sql);
            }

        }

        private void YongHuGuanLi_Load(object sender, EventArgs e)
        {
            BindData();
            BindgwData();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string cbbid = (cbbtype.SelectedValue ?? "").ToString();
            if (string.IsNullOrWhiteSpace(cbbid))
            {
                MessageBox.Show("请首先添加项目类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string username = txtname.Text;
            string bm = txtvalue.Text;
            string seq = nudseq.Value.ToString();
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("名称不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(bm))
            {
                MessageBox.Show("编码不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(seq))
            {
                MessageBox.Show("排序不能为空");
                return;
            }

            string s = "^[0-9]*$";//正则表达式
            Regex reg = new Regex(s);
            if (!reg.IsMatch(bm))
            {
                MessageBox.Show("请输入数字");
                return;
            }

            string sql = " select count(*) from DictData where DicTypeID='" + cbbid + "' and Name='" + username + "'";
            string result = (db.Scalar(sql) ?? "0").ToString();
            if (result != "0")
            {
                MessageBox.Show("名称不能重复");
                return;
            }
            sql = " select count(*) from DictData where DicTypeID='" + cbbid + "' and Value='" + bm + "'";
            result = (db.Scalar(sql) ?? "0").ToString();
            if (result != "0")
            {
                MessageBox.Show("编码不能重复");
                return;
            }
            sql = " insert into DictData (DicTypeID,Name,Value,Seq) values(@DicTypeID,@Name,@Value,@Seq) ";
            ParameterCollection p = new ParameterCollection();
            p.Add("@DicTypeID", cbbid);
            p.Add("@Name", username);
            p.Add("@Value", bm.PadLeft(2, '0'));
            p.Add("@Seq", seq);
            int flag = db.NonQuery(sql, p);
            if (flag == 1)
            {
                MessageBox.Show("添加成功");
                BindgwData();
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
                string id = dataGridView1.Rows[index].Cells["id"].Value.ToString();


                if (DialogResult.Yes == MessageBox.Show("确定删除此信息吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string sql = " delete DictData where id='" + id + "'";
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

        /// <summary>
        /// 项目类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindgwData();
        }
    }
}
