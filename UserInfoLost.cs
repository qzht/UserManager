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
    public partial class UserInfoLost : Form
    {
        public UserInfoLost()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // string MerNum = txtMerNum.Text.Trim();

            string one = (cbbone.SelectedValue ?? "").ToString();
            string two = (cbbtwo.SelectedValue ?? "").ToString();
            string three = (cbbthree.SelectedValue ?? "").ToString();
            string four = (cbbfour.Text ?? "").ToString();
            if (string.IsNullOrWhiteSpace(one))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(two))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(three))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (string.IsNullOrWhiteSpace(four))
            //{
            //    MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            string phone = txtqphone.Text;

            string sfz = txtqsfz.Text;

            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            string where = "";
           
            if (!string.IsNullOrWhiteSpace(four))
            {
                where += " and  MerNum='" + MerNum + "' ";
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                where += " and  PhoneNum='" + phone + "' ";
            }
            if (!string.IsNullOrWhiteSpace(sfz))
            {
                where += " and  ZJHM='" + sfz + "' ";
            }
            if (string.IsNullOrWhiteSpace(where))
            {
                MessageBox.Show("请填写最少一种查询条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dt = db.ExecuteDataTable("select * from MerAllInfo_View where 1=1 "+where+" ");
            if (dt != null && dt.Rows.Count >= 2)
            {
                MessageBox.Show("读取数据异常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dt != null && dt.Rows.Count == 0)
            {
                MessageBox.Show("无此商户信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dt != null && dt.Rows.Count == 1)
            {
                txtUserName.Text = dt.Rows[0]["ZJRXM"].ToString();
                txtsfz.Text = dt.Rows[0]["ZJHM"].ToString();
                txtphone.Text = dt.Rows[0]["PhoneNum"].ToString();
                txtAccountBalance.Text = dt.Rows[0]["Yue"].ToString();
                button2.Enabled = true;
            }
        }

        /// <summary>
        /// 挂失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //string MerNum = txtMerNum.Text.Trim();
            //if (string.IsNullOrWhiteSpace(MerNum))
            //{
            //    MessageBox.Show("请输入商户编号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            string one = (cbbone.SelectedValue ?? "").ToString();
            string two = (cbbtwo.SelectedValue ?? "").ToString();
            string three = (cbbthree.SelectedValue ?? "").ToString();
            string four = (cbbfour.Text ?? "").ToString();
            if (string.IsNullOrWhiteSpace(one))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(two))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(three))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (string.IsNullOrWhiteSpace(four))
            //{
            //    MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            string where = "";
            string phone = txtqphone.Text;
            string sfz = txtqsfz.Text;
            if (string.IsNullOrWhiteSpace(four))
            {
                MerNum = "";
            }
            if (!string.IsNullOrWhiteSpace(four))
            {
                where += " and  MerNum='" + MerNum + "' ";
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                where += " and  PhoneNum='" + phone + "' ";
            }
            if (!string.IsNullOrWhiteSpace(sfz))
            {
                where += " and  ZJHM='" + sfz + "' ";
            }
            DataTable dt = db.ExecuteDataTable("select isShuiCardLost,ID from MerAllInfo_View where 1=1 " + where + "");
            if (string.IsNullOrWhiteSpace(where))
            {
                MessageBox.Show("请填写最少一种查询条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("此商户还没开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dt.Rows.Count == 1)
            {
                if (!bool.Parse(dt.Rows[0]["isShuiCardLost"].ToString()))
                {
                    MessageBox.Show("已挂失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                db.NonQuery(" update MerInfo set isShuiCardLost=0 where ID= " + dt.Rows[0]["ID"].ToString() + "");
                db.NonQuery("insert into U_AuditLog(OpeType,UserID,MerNum,CarType) values('挂失','" + Common.UserId + "','" + MerNum + "',0)");
                Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtUserName.Text + "挂失");
                MessageBox.Show("挂失成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("挂失失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserInfoLost_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = "select Name,Value from dbo.Dict_View where TypeName='One' order by cast(seq as int) ";
                cbbone.ValueMember = "Value";
                cbbone.DisplayMember = "Name";
                cbbone.DataSource = db.ExecuteDataTable(sql);
                sql = "select Name,Value from dbo.Dict_View where TypeName='Two' order by cast(seq as int) ";
                cbbtwo.ValueMember = "Value";
                cbbtwo.DisplayMember = "Name";
                cbbtwo.DataSource = db.ExecuteDataTable(sql);
                sql = "select Name,Value from dbo.Dict_View where TypeName='Three' order by cast(seq as int) ";
                cbbthree.ValueMember = "Value";
                cbbthree.DisplayMember = "Name";
                cbbthree.DataSource = db.ExecuteDataTable(sql);
                //sql = "select Name,Value from dbo.Dict_View where TypeName='Four' order by seq ";
                //cbbfour.ValueMember = "Value";
                //cbbfour.DisplayMember = "Name";
                //cbbfour.DataSource = db.ExecuteDataTable(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
