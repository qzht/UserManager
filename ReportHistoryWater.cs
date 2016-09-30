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
    public partial class ReportHistoryWater : Temp
    {
        public ReportHistoryWater()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string start = dtpstart.Text;
            string end = dtpend.Text;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string where = " and TermID='U_" + Common.UserId + "'";
            if (Common.userName == "admin")
            {
                where = "";
            }
            string type = cbbtype.Text;
            string where2 = "";
            if (type == "现金")
                where2 += " and IsCash='" + 1 + "'";
            if (type == "网络")
                where2 += " and IsCash='" + 0 + "'";
          //  string sql = "select opetype 类型,count(*) 数量 from  dbo.U_AuditLog where '" + start + "'<=OpeTime and OpeTime<='" + end + "' and UserID='" + Common.UserId + "' group by opetype";
            string sql = "select  case when tradetype=1 then '水费' when tradetype=2 then '电费' when tradetype=3 then '车辆年费'when tradetype=4 then '房租'when tradetype=5 then '余额' when tradetype=6 then '地泵扣费' when tradetype=7 then '车辆出入扣费' "
            + "when tradetype=8 then '销户' when tradetype=10 then '广告租赁费' when tradetype=11 then '押金'  else '其他' end   类型,count(*) 数量,sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))) 总额 from  dbo.S_Water where '" + start + "'<=CreateDate and IsCash=1 and CreateDate<='" + end + "' " + where + " " + where2 + " and RetCode='00' group by tradetype";
            DataTable dt = db.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                dataGridView1.DataSource = dt;
                int width = 0;
                for (int i = 0; i < this.dataGridView1.Columns.Count; i++)//对于DataGridView的每一个列都调整
                {
                    width += this.dataGridView1.Columns[i].Width;//记录整个DataGridView的宽度
                }
                if (width > this.dataGridView1.Size.Width)//判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，则将DataGridView的列自动调整模式设置为显示的列即可，如果是小于原来设定的宽度，将模式改为填充。
                {
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                }
                else
                {
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            else
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string start = dtpstart.Text;
            string end = dtpend.Text;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string where = " and TermID='U_" + Common.UserId + "'";
            if (Common.userName == "admin")
            {
                where = "";
            }
            string type = cbbtype.Text;
            string where2 = "";
            if (type == "现金")
                where2 += " and IsCash='" + 1 + "'";
            if (type == "网络")
                where2 += " and IsCash='" + 0 + "'";

            string sql = "select  case when tradetype=1 then '水费' when tradetype=2 then '电费' when tradetype=3 then '车辆年费'when tradetype=4 then '房租'when tradetype=5 then '余额' when tradetype=6 then '地泵扣费' when tradetype=7 then '车辆出入扣费' "
           + "when tradetype=8 then '销户' when tradetype=10 then '广告租赁费' when tradetype=11 then '押金'  else '其他' end   类型,count(*) 数量,sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))) 总额 from  dbo.S_Water where '" + start + "'<=CreateDate and IsCash=1 and CreateDate<='" + end + "' " + where + " " + where2 + " and RetCode='00' group by tradetype";
            DataTable dt = db.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "Excel文件|*.xls";
                saveDialog.FileName = Common.realName + "历次流水统计" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                saveDialog.ShowDialog();
                string saveFileName = saveDialog.FileName;
                if (saveFileName.IndexOf(":") < 0)
                {
                    return; //被点了取消  
                }
                Excel.FromDataTableToExcelForWin(dt, Common.realName+"历次流水统计", saveFileName);
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
        private void ReportHistoryWater_Load(object sender, EventArgs e)
        {
            dtpstart.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00"); ;
            dtpend.Text = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            cbbtype.Items.Insert(0, "全部");
            cbbtype.Items.Insert(1, "现金");
            cbbtype.Items.Insert(2, "网络");
            cbbtype.SelectedIndex = 0;
        }
    }
}
