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
    public partial class ReportCard : Temp
    {
        public ReportCard()
        {
            InitializeComponent();
        }

        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
       
        /// <summary>
        /// 导出Excel按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string start = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            string end = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            string where = " and UserID='" + Common.UserId + "'";
            if (Common.userName == "admin")
            {
                where = "";
            }
            string sql = "select opetype 类型,count(*) 数量 from  dbo.U_AuditLog where '" + start + "'<=OpeTime and OpeTime<='" + end + "' "+where+" group by opetype";
            DataTable dt = db.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "Excel文件|*.xls";
                saveDialog.FileName = Common.realName + "当日卡统计" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                saveDialog.ShowDialog();
                string saveFileName = saveDialog.FileName;
                if (saveFileName.IndexOf(":") < 0)
                {
                    return; //被点了取消  
                }
                Excel.FromDataTableToExcelForWin(dt, Common.realName + "当日卡统计", saveFileName);
                MessageBox.Show("导出成功");
            }
            else
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportCard_Load(object sender, EventArgs e)
        {
            string start = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            string end = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

            string where = " and UserID='" + Common.UserId + "'";
            if (Common.userName == "admin")
            {
                where = "";
            }

            string sql = "select opetype 类型,count(*) 数量 from  dbo.U_AuditLog where '" + start + "'<=OpeTime and OpeTime<='" + end + "' " + where + "  group by opetype";
            DataTable dt=db.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
