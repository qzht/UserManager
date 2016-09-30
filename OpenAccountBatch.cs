using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XK.NBear.DB;
using dmsk;

namespace UserManager
{
    public partial class OpenAccountBatch : Form
    {
        public OpenAccountBatch()
        {
            InitializeComponent();
        }
        common com = new common();
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string one = (cbbone.SelectedValue ?? "").ToString();
            string two = (cbbtwo.SelectedValue ?? "").ToString();
            string three = (cbbthree.SelectedValue ?? "").ToString();
            string four = (cbbfour.SelectedValue ?? "").ToString();
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
            if (string.IsNullOrWhiteSpace(four))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = "";//txtMerNum.Text.Trim();
            if (one == "-1")
            {
                MerNum += "_";
            }
            else
            {
                MerNum += one;
            }
            if (two == "-1")
            {
                MerNum += "_";
            }
            else
            {
                MerNum += two;
            }
            if (three == "-1")
            {
                MerNum += "_";
            }
            else
            {
                MerNum += three;
            }
            if (four == "-1")
            {
                MerNum += "_";
            }
            else
            {
                MerNum += four;
            }
            string xm = txtxm.Text.Trim();
            string phone = txtphone.Text.Trim();
            string sfz = txtsfz.Text.Trim();
            string sql = " select mernum 商户编号,zjrxm 姓名, phonenum 手机号,zjhm 身份证号,shuicardnum 水卡号,diancardnum 电卡号"
                       + "  from  dbo.MerAllInfo_View where 1=1";
            if (!string.IsNullOrWhiteSpace(MerNum))
            {
                sql += " and mernum like '" + MerNum + "' ";
            }
            if (!string.IsNullOrWhiteSpace(xm))
            {
                sql += " and zjrxm like '%" + xm + "%' ";
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                sql += " and phonenum='" + phone + "' ";
            }
            if (!string.IsNullOrWhiteSpace(sfz))
            {
                sql += " and zjhm='" + sfz + "' ";
            }
            DataTable dt = db.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("无商户信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        /// <summary>
        /// 开户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int index = dataGridView1.CurrentRow.Index;
            if (index == -1)
            {
                MessageBox.Show("无选择数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string msg = "";
                string MerNum = dataGridView1.Rows[index].Cells["商户编号"].Value.ToString();
                string OldCard = dataGridView1.Rows[index].Cells["水卡号"].Value.ToString();
                if (!string.IsNullOrWhiteSpace(OldCard))
                {
                    MessageBox.Show("开户失败-该商户已开户完成，请勿重复开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int flag2 = com.pd_gsk();
                if (flag2 == 0)
                {
                    MessageBox.Show("非公司卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string carduid = com.get_UID(out msg);
                if (carduid == "-1")
                {
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (IsExitWaterCar(carduid))
                {
                    MessageBox.Show("卡号重复，请换张卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int flag = 0;
                try
                {
                    flag = com.kaihu(MerNum, "1", "100", out msg);
                }
                catch
                {
                    MessageBox.Show("操作卡失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (flag == 0)
                {
                    db.NonQuery(" update MerInfo set ShuiCardNum='' where MerNum='" + MerNum + "' ");
                    MessageBox.Show("水卡开户成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 挂失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int index = dataGridView1.CurrentRow.Index;
            if (index == -1)
            {
                MessageBox.Show("无选择数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string MerNum = dataGridView1.Rows[index].Cells["商户编号"].Value.ToString();
                db.NonQuery(" update MerInfo set isShuiCardLost=0 where MerNum='" + MerNum + "'");
                MessageBox.Show("挂失成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 解挂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int index = dataGridView1.CurrentRow.Index;
            if (index == -1)
            {
                MessageBox.Show("无选择数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string MerNum = dataGridView1.Rows[index].Cells["商户编号"].Value.ToString();
                db.NonQuery(" update MerInfo set isShuiCardLost=1 where MerNum='" + MerNum + "'");
                MessageBox.Show("解挂成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 销户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int index = dataGridView1.CurrentRow.Index;
            if (index == -1)
            {
                MessageBox.Show("无选择数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string msg = "";
                int flag = com.zuofei(out msg);
                if (flag != 0)
                {
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string MerNum = dataGridView1.Rows[index].Cells["商户编号"].Value.ToString();
                    db.NonQuery(" delete from MerInfo where MerNum='" + MerNum + "'");
                    MessageBox.Show("销户成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsersInfo_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string sql = "select Name,Value from dbo.Dict_View where TypeName='One' order by cast(seq as int) ";
            dt = db.ExecuteDataTable(sql);
            DataRow dr = dt.NewRow();
            dr["Name"] = "全部";
            dr["Value"] = "-1";
            if (dt != null && dt.Rows.Count != 0)
                dt.Rows.InsertAt(dr, 0);
            cbbone.ValueMember = "Value";
            cbbone.DisplayMember = "Name";
            cbbone.DataSource = dt;
            sql = "select Name,Value from dbo.Dict_View where TypeName='Two' order by cast(seq as int) ";
            dt = db.ExecuteDataTable(sql);
            dr = dt.NewRow();
            dr["Name"] = "全部";
            dr["Value"] = "-1";
            if (dt != null && dt.Rows.Count != 0)
                dt.Rows.InsertAt(dr, 0);
            cbbtwo.ValueMember = "Value";
            cbbtwo.DisplayMember = "Name";
            cbbtwo.DataSource = dt;
            sql = "select Name,Value from dbo.Dict_View where TypeName='Three' order by cast(seq as int) ";
            dt = db.ExecuteDataTable(sql);
            dr = dt.NewRow();
            dr["Name"] = "全部";
            dr["Value"] = "-1";
            if (dt != null && dt.Rows.Count != 0)
                dt.Rows.InsertAt(dr, 0);
            cbbthree.ValueMember = "Value";
            cbbthree.DisplayMember = "Name";
            cbbthree.DataSource = dt;
            sql = "select Name,Value from dbo.Dict_View where TypeName='Four' order by cast(seq as int) ";
            dt = db.ExecuteDataTable(sql);
            dr = dt.NewRow();
            dr["Name"] = "全部";
            dr["Value"] = "-1";
            if (dt != null && dt.Rows.Count != 0)
                dt.Rows.InsertAt(dr, 0);
            cbbfour.ValueMember = "Value";
            cbbfour.DisplayMember = "Name";
            cbbfour.DataSource = dt;
        }
        /// <summary>
        /// 判断卡号是否重复
        /// </summary>
        /// <param name="ShuiCardNum"></param>
        /// <returns></returns>
        public bool IsExitWaterCar(string ShuiCardNum)
        {
            bool state = false;
            string sql = " select count(*) from MerInfo where ShuiCardNum='" + ShuiCardNum + "' ";//首先判断商户编号唯一
            string result = (db.Scalar(sql) ?? "0").ToString();
            if (int.Parse(result) > 0)
                state = true;
            return state;
        }


        /// <summary>
        /// 发卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null ||  ((DataTable)dataGridView1.DataSource).Rows.Count==0)
            {
                MessageBox.Show("请先查询数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
