using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using dmsk;
using XK.NBear.DB;

namespace UserManager
{
    public partial class QueryCard : Temp
    {
        public QueryCard()
        {
            InitializeComponent();
        }
        common com = new common();
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");

        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string msg = "";
            string carduid = com.get_UID(out msg);
            if (carduid == "-1")
            {
                txtcarid.Text = "";
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string result = (db.Scalar("select count(*) from U_AuditLog where CardNum='" + carduid + "'") ?? "0").ToString();
                if (result == "0")
                {
                    MessageBox.Show("无此卡信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DataTable dt = db.ExecuteDataTable(" select a.zjrxm, b.OpeType,b.OpeTime,b.MerName from  dbo.MerUser a, dbo.U_AuditLog b where a.id=b.userid and b.CardNum='" + carduid + "' order by b.OpeTime desc ");
                    txtcarid.Text = carduid;
                    dataGridView1.DataSource = dt;
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
