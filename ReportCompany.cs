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
    public partial class ReportCompany : Temp
    {
        public ReportCompany()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        private void ReportCompany_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dtpstart.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00"); ;
            dtpend.Text = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

            string sql = "select company from dbo.GuanLiUser group by company";
            dt = db.ExecuteDataTable(sql);
            DataRow dr = dt.NewRow();
            dr["company"] = "全部";
            if (dt != null && dt.Rows.Count != 0)
                dt.Rows.InsertAt(dr, 0);
            cbbCompany.ValueMember = "company";
            cbbCompany.DisplayMember = "company";
            cbbCompany.DataSource = dt;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string start = dtpstart.Text;
            string end = dtpend.Text;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string where = "";
            if (cbbCompany.Text != "全部")
            {
                where = " and TermID in (select 'U_'+cast(id as nvarchar(10)) from dbo.GuanLiUser where company='" + cbbCompany.Text + "')";
            }
            string sql = "select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
            + " where '" + start + "' <=createdate and createdate<='" + end + "' " + where + " and tradetype=5 and RetCode='00' ";
            string yue = (db.Scalar(sql) ?? "0").ToString();
            sql = " select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' <=createdate and createdate<='" + end + "' " + where + " and tradetype=1  and RetCode='00' ";
            string water = (db.Scalar(sql) ?? "0").ToString();
            sql = "select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' <=createdate and createdate<='" + end + "'  " + where + " and tradetype=2  and RetCode='00' ";
            string dian = (db.Scalar(sql) ?? "0").ToString();
            sql = "select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' <=createdate and createdate<='" + end + "' " + where + " and tradetype in(6,7,8)  and RetCode='00' ";
            string xiaofei = (db.Scalar(sql) ?? "0").ToString();
            sql = " select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' >createdate " + where + " and tradetype=5  and RetCode='00' ";
            string yueIni = (db.Scalar(sql) ?? "0").ToString();
            sql = " select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' >createdate " + where + " and tradetype in(6,7,8)  and RetCode='00' ";
            string xiaofeiIni = (db.Scalar(sql) ?? "0").ToString();
            string yueIni2 = (decimal.Parse(yueIni) - decimal.Parse(xiaofeiIni)).ToString("#0.00");
            string Yue2 = (decimal.Parse(yueIni2) + decimal.Parse(yue) - decimal.Parse(xiaofei)).ToString("#0.00");
            dataGridView1.Rows.Add(new object[] { yueIni2, yue, xiaofei, Yue2, water, dian });
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string start = dtpstart.Text;
            string end = dtpend.Text;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string where = "";
            if (cbbCompany.Text != "全部")
            {
                where = " and TermID in (select 'U_'+cast(id as nvarchar(10)) from dbo.GuanLiUser where company='" + cbbCompany.Text + "')";
            }
            string sql = "select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
            + " where '" + start + "' <=createdate and createdate<='" + end + "' " + where + " and tradetype=5 and RetCode='00' ";
            string yue = (db.Scalar(sql) ?? "0").ToString();
            sql = " select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' <=createdate and createdate<='" + end + "' " + where + " and tradetype=1  and RetCode='00' ";
            string water = (db.Scalar(sql) ?? "0").ToString();
            sql = "select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' <=createdate and createdate<='" + end + "'  " + where + " and tradetype=2  and RetCode='00' ";
            string dian = (db.Scalar(sql) ?? "0").ToString();
            sql = "select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' <=createdate and createdate<='" + end + "' " + where + " and tradetype in(6,7,8)  and RetCode='00' ";
            string xiaofei = (db.Scalar(sql) ?? "0").ToString();
            sql = " select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' >createdate " + where + " and tradetype=5  and RetCode='00' ";
            string yueIni = (db.Scalar(sql) ?? "0").ToString();
            sql = " select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' >createdate " + where + " and tradetype in(6,7,8)  and RetCode='00' ";
            string xiaofeiIni = (db.Scalar(sql) ?? "0").ToString();
            string yueIni2 = (decimal.Parse(yueIni) - decimal.Parse(xiaofeiIni)).ToString("#0.00");
            string Yue2 = (decimal.Parse(yueIni2) + decimal.Parse(yue) - decimal.Parse(xiaofei)).ToString("#0.00");
            dataGridView1.Rows.Add(new object[] { yueIni2, yue, xiaofei, Yue2, water, dian });
            DataTable dt = GetDgvToTable(dataGridView1);
            if (dt != null && dt.Rows.Count != 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "Excel文件|*.xls";
                saveDialog.FileName = Common.realName + "收费单位统计" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                saveDialog.ShowDialog();
                string saveFileName = saveDialog.FileName;
                if (saveFileName.IndexOf(":") < 0)
                {
                    return; //被点了取消  
                }
                Excel.FromDataTableToExcelForWin(dt, Common.realName + "收费单位统计", saveFileName);
                MessageBox.Show("导出成功");
            }
            else
            {
                dataGridView1.Rows.Clear();
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string start = dtpstart.Text;
            string end = dtpend.Text;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string where = "";
            if (cbbCompany.Text != "全部")
            {
                where = " and TermID in (select 'U_'+cast(id as nvarchar(10)) from dbo.GuanLiUser where company='" + cbbCompany.Text + "')";
            }
            string sql = "select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
            + " where '" + start + "' <=createdate and createdate<='" + end + "' " + where + " and tradetype=5 and RetCode='00' ";
            string yue = (db.Scalar(sql) ?? "0").ToString();
            sql = " select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' <=createdate and createdate<='" + end + "' " + where + " and tradetype=1  and RetCode='00' ";
            string water = (db.Scalar(sql) ?? "0").ToString();
            sql = "select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' <=createdate and createdate<='" + end + "'  " + where + " and tradetype=2  and RetCode='00' ";
            string dian = (db.Scalar(sql) ?? "0").ToString();
            sql = "select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' <=createdate and createdate<='" + end + "' " + where + " and tradetype in(6,7,8)  and RetCode='00' ";
            string xiaofei = (db.Scalar(sql) ?? "0").ToString();
            sql = " select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' >createdate " + where + " and tradetype=5  and RetCode='00' ";
            string yueIni = (db.Scalar(sql) ?? "0").ToString();
            sql = " select isnull(sum(cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2))),0) from S_Water "
           + " where '" + start + "' >createdate " + where + " and tradetype in(6,7,8)  and RetCode='00' ";
            string xiaofeiIni = (db.Scalar(sql) ?? "0").ToString();
            string yueIni2 = (decimal.Parse(yueIni) - decimal.Parse(xiaofeiIni)).ToString("#0.00");
            string Yue2 = (decimal.Parse(yueIni2) + decimal.Parse(yue) - decimal.Parse(xiaofei)).ToString("#0.00");
            dataGridView1.Rows.Add(new object[] { yueIni2, yue, xiaofei, Yue2, water, dian });
            DataTable dt = GetDgvToTable(dataGridView1);
            if (dt != null && dt.Rows.Count != 0)
            {
            }
            else
            {
                dataGridView1.Rows.Clear();
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            VBprinter.DGVprint dgVprint1 = new VBprinter.DGVprint();
            dgVprint1.MainTitle = "收费单位统计";
            dgVprint1.AutoWrap = true;
            dgVprint1.ZoomToPaperWidth = true;
            dgVprint1.Print(dataGridView1, false);
        }
    }
}
