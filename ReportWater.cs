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
    public partial class ReportWater : Temp
    {
        public ReportWater()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string start = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            string end = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            string where = " and TermID='U_" + Common.UserId + "'";
            if (Common.userName == "admin")
            {
                where = "";
            }
            string sql = "select  case when tradetype=1 then '水费' when tradetype=2 then '电费' when tradetype=3 then '车辆年费'when tradetype=4 then '房租'when tradetype=5 then '余额'  else '其他' end   类型,count(*) 数量,sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))) 总额 from  dbo.S_Water where '" + start + "'<=CreateDate and IsCash=1 and CreateDate<='" + end + "' " + where + " and RetCode='00' group by tradetype";
            DataTable dt = db.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "Excel文件|*.xls";
                saveDialog.FileName = Common.realName + "当日流水统计" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                saveDialog.ShowDialog();
                string saveFileName = saveDialog.FileName;
                if (saveFileName.IndexOf(":") < 0)
                {
                    return; //被点了取消  
                }
                Excel.FromDataTableToExcelForWin(dt, Common.realName + "当日流水统计", saveFileName);
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
        /// 窗体加载事件 水费1 ，电费2 ，车辆年费3， 房租4 ，钱包5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportWater_Load(object sender, EventArgs e)
        {
            string start = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            string end = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            string where = " and TermID='U_" + Common.UserId + "'";
            if (Common.userName == "admin")
            {
                where = "";
            }
            string sql = "select  case when tradetype=1 then '水费' when tradetype=2 then '电费' when tradetype=3 then '车辆年费'when tradetype=4 then '房租'when tradetype=5 then '余额'  else '其他' end   类型,count(*) 数量,sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))) 总额 from  dbo.S_Water where '" + start + "'<=CreateDate and IsCash=1 and CreateDate<='" + end + "' " + where + " and RetCode='00' group by tradetype";
            DataTable dt = db.ExecuteDataTable(sql);
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
