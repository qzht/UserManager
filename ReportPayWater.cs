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
    public partial class ReportPayWater : Temp
    {
        public ReportPayWater()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        private void ReportPayWater_Load(object sender, EventArgs e)
        {
            dtpstart.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00"); ;
            dtpend.Text = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
        }
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
            string sql = " select a.dyhbh 用户编号,a.zjrxm 用户姓名,a.JFCS 购水次数,  cast(cast(b.transmoney as decimal(10,2))/100 /dbo.GetWaterPrice() as decimal(10,0))  总购量 ,"
                          + " dbo.GetWaterPrice() 水价,cast(cast(b.transmoney as decimal(10,2))/100 as decimal(10,2)) 水费,b.createdate 购水时间,b.orderid 发票编号,"
                          + " dbo.GetUserName(isnull(b.TermID,'')) 营业人员"
                          + " from  dbo.MerAllInfo_View a right join S_Water b"
                          + " on a.mernum=b.mernum "
                          + " where '" + start + "'<=b.createdate and b.createdate<='" + end + "'  and b.tradetype=1  and b.retcode='00' ";
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
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            string sql = " select a.dyhbh 用户编号,a.zjrxm 用户姓名,a.JFCS 购水次数,a.ZGL 总购量,"
                               + " dbo.GetWaterPrice() 水价,cast(cast(b.transmoney as decimal(10,2))/100 as decimal(10,2)) 水费,b.createdate 购水时间,b.orderid 发票编号,"
                               + " dbo.GetUserName(isnull(b.TermID,'')) 营业人员"
                               + " from  dbo.MerAllInfo_View a right join S_Water b"
                               + " on a.mernum=b.mernum "
                               + " where '" + start + "'<=b.createdate and b.createdate<='" + end + "'  and b.tradetype=1  and b.retcode='00' "; ;
            DataTable dt = db.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "Excel文件|*.xls";
                saveDialog.FileName = Common.realName + "营业售水月报表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                saveDialog.ShowDialog();
                string saveFileName = saveDialog.FileName;
                if (saveFileName.IndexOf(":") < 0)
                {
                    return; //被点了取消  
                }
                Excel.FromDataTableToExcelForWin(dt, Common.realName + "营业售水月报表", saveFileName);
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
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
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
            string sql = " select a.dyhbh 用户编号,a.zjrxm 用户姓名,a.JFCS 购水次数,a.ZGL 总购量,"
                        + " dbo.GetWaterPrice() 水价,cast(cast(b.transmoney as decimal(10,2))/100 as decimal(10,2)) 水费,b.createdate 购水时间,b.orderid 发票编号,"
                        + " dbo.GetUserName(isnull(b.TermID,'')) 营业人员"
                        + " from  dbo.MerAllInfo_View a right join S_Water b"
                        + " on a.mernum=b.mernum "
                        + " where '" + start + "'<=b.createdate and b.createdate<='" + end + "'  and b.tradetype=1  and b.retcode='00' ";
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
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            VBprinter.DGVprint dgVprint1 = new VBprinter.DGVprint();
            dgVprint1.MainTitle = "营业售水月报表";
            dgVprint1.SubTitle = "日期范围：从" + start + "到" + end + "止";
            dgVprint1.AutoWrap = true;
            dgVprint1.ZoomToPaperWidth = true;
            dgVprint1.Print(dataGridView1, false);
        }
    }
}
