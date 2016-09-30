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
    public partial class WaterPrice : Form
    {
        public WaterPrice()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string price = txtWaterPrice.Text.Trim();
            try
            {
                float prices = float.Parse(price);
            }
            catch
            {
                MessageBox.Show("输入有误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int flag = db.NonQuery(" update Price set ShuiPrice='" + price + "'");
            if (flag == 1)
            {
                MessageBox.Show("设置成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("设置失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void WaterPrice_Load(object sender, EventArgs e)
        {
            DataTable dt = db.ExecuteDataTable(" select * from Price");
            if (dt != null && dt.Rows.Count >= 2)
            {
                MessageBox.Show("数据读取失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dt != null && dt.Rows.Count == 0)
            {
                db.NonQuery(" insert into Price (ShuiPrice) values('0')");
                return;
            }
            if (dt != null && dt.Rows.Count == 1)
            {
                txtWaterPrice.Text = dt.Rows[0]["ShuiPrice"].ToString();
            }
        }
    }
}
