using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XK.NBear.DB;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace UserManager
{
    public partial class QueryRecordWork : Temp
    {
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");

        public QueryRecordWork()
        {
            InitializeComponent();
        }

        private void QueryRecordWork_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string sql = "select company from dbo.GuanLiUser group by company";
            dt = db.ExecuteDataTable(sql);
            DataRow dr = dt.NewRow();
            dr["company"] = "全部";
            if (dt != null && dt.Rows.Count != 0)
                dt.Rows.InsertAt(dr, 0);
            cbbone.ValueMember = "company";
            cbbone.DisplayMember = "company";
            cbbone.DataSource = dt;

            dtpstart.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00"); ;
            dtpend.Text = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            string one = (cbbone.SelectedValue ?? "").ToString();
            if (string.IsNullOrWhiteSpace(one))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string start = dtpstart.Text;
            string end = dtpend.Text;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = " select RealName 姓名,Company 收费单位,LoginTime 开始时间,UnLoginTime 结束时间,"
                       +" CZCount 充值笔数,CZSum 充值总额, XKCount 销卡笔数, XKSum 销卡总额,AllSum 本次余额,"
                       +" XFCount 交易笔数,XFSum 收费金额 ,XFXJ 现金,XFCZK 充值卡 from dbo.RecordWork where 1=1 ";
            sql += " and '" + start + "'<=LoginTime and UnLoginTime<='"+end+"' ";

            if (cbbone.Text != "全部")
            {
                sql += " and Company='"+cbbone.Text+"'";
            }
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
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            string one = (cbbone.SelectedValue ?? "").ToString();
            if (string.IsNullOrWhiteSpace(one))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string start = dtpstart.Text;
            string end = dtpend.Text;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = " select RealName 姓名,Company 收费单位,LoginTime 开始时间,UnLoginTime 结束时间,"
                       + " CZCount 充值笔数,CZSum 充值总额, XKCount 销卡笔数, XKSum 销卡总额,AllSum 本次余额,"
                       + " XFCount 交易笔数,XFSum 收费金额 ,XFXJ 现金,XFCZK 充值卡 from dbo.RecordWork where 1=1 ";
            sql += " and '" + start + "'<=LoginTime and UnLoginTime<='" + end + "' ";

            if (cbbone.Text != "全部")
            {
                sql += " and Company='" + cbbone.Text + "'";
            }
            DataTable dt = db.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "Excel文件|*.xls";
                saveDialog.FileName = Common.realName + "交接班记录查询" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                saveDialog.ShowDialog();
                string saveFileName = saveDialog.FileName;
                if (saveFileName.IndexOf(":") < 0)
                {
                    return; //被点了取消  
                }
                Excel.FromDataTableToExcelForWin(dt, Common.realName + "交接班记录查询", saveFileName);
                MessageBox.Show("导出成功");
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
