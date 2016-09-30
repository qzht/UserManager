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
    public partial class QueryWater : Temp
    {
        public QueryWater()
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
            string where = "";
            if (cbbCompany.Text != "全部")
            {
                where = " and TermID in (select 'U_'+cast(id as nvarchar(10)) from dbo.GuanLiUser where company='" + cbbCompany.Text + "')";
            }

            //string where = " and TermID='U_" + Common.UserId + "'";
            //if (Common.userName == "admin")
            //{
            //    where = "";
            //}

            string type = cbbtype.Text;
            string where2 = "";
            if (type == "现金")
                where2 += " and IsCash='" + 1 + "'";
            if (type == "网络")
                where2 += " and IsCash='" + 0 + "'";
            string where3 = "";
            if (cbbxm.Text.ToString() != "全部")
            {
                where3 += " and TradeType='" + ((ListItem)(cbbxm.SelectedItem)).Key + "'";
            }


            string sql = "select  case when tradetype=1 then '水费' when tradetype=2 then '电费' when tradetype=3 then '车辆年费'"
            + "when tradetype=4 then '房租'when tradetype=5 then '余额' when tradetype=6 then '地泵扣费' when tradetype=7 then '车辆出入扣费' "
            + "when tradetype=8 then '销户' when tradetype=10 then '广告租赁费' when tradetype=11 then '押金'  else '其他' end   类型,zjrxm 姓名,case when isnull(MerNum,'')='' then '市场卡' else dbo.GetMerName(MerNum) end 商户,"
            + "createdate 时间,cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2)) 金额,case when iscash =1 then '现金' else '网络' end 缴费方式 "
            + "from  dbo.S_Water where '" + start + "'<=CreateDate and CreateDate<='" + end + "' " + where + " " + where3 + " " + where2 + "  and RetCode='00' order by createdate desc";
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

            string where3 = "";
            if (cbbxm.Text.ToString() != "全部")
            {
                where3 += " and TradeType='" + ((ListItem)(cbbxm.SelectedItem)).Key + "'";
            }


            string sql = "select  case when tradetype=1 then '水费' when tradetype=2 then '电费' when tradetype=3 then '车辆年费'"
            + "when tradetype=4 then '房租'when tradetype=5 then '余额' when tradetype=6 then '地泵扣费' when tradetype=7 then '车辆出入扣费' "
            + "when tradetype=8 then '销户' when tradetype=10 then '广告租赁费' when tradetype=11 then '押金'  else '其他' end   类型,zjrxm 姓名,case when isnull(MerNum,'')='' then '市场卡' else dbo.GetMerName(MerNum) end 商户,"
            + "createdate 时间,cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2)) 金额,case when iscash =1 then '现金' else '网络' end 缴费方式 "
            + "from  dbo.S_Water where '" + start + "'<=CreateDate and CreateDate<='" + end + "' " + where + " " + where3 + " " + where2 + "  and RetCode='00' order by createdate desc";
            DataTable dt = db.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "Excel文件|*.xls";
                saveDialog.FileName = Common.realName + "详细流水查询" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                saveDialog.ShowDialog();
                string saveFileName = saveDialog.FileName;
                if (saveFileName.IndexOf(":") < 0)
                {
                    return; //被点了取消  
                }
                Excel.FromDataTableToExcelForWin(dt, Common.realName + "详细流水查询", saveFileName);
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
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryWater_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dtpstart.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00"); ;
            dtpend.Text = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

            cbbtype.Items.Insert(0, "全部");
            cbbtype.Items.Insert(1, "现金");
            cbbtype.Items.Insert(2, "网络");
            cbbtype.SelectedIndex = 0;

            cbbxm.Items.Add(new ListItem("0", "全部"));
            cbbxm.Items.Add(new ListItem("1", "水费"));
            cbbxm.Items.Add(new ListItem("2", "电费"));
            cbbxm.Items.Add(new ListItem("3", "车辆年费"));
            cbbxm.Items.Add(new ListItem("4", "房租"));
            cbbxm.Items.Add(new ListItem("5", "余额"));
            cbbxm.Items.Add(new ListItem("6", "地泵扣费"));
            cbbxm.Items.Add(new ListItem("7", "车辆出入扣费"));
            cbbxm.Items.Add(new ListItem("10", "广告租赁费"));
            cbbxm.Items.Add(new ListItem("11", "押金"));
            cbbxm.SelectedIndex = 0;
            
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
            string where3 = "";
            if (cbbxm.Text.ToString() != "全部")
            {
                where3 += " and TradeType='" + ((ListItem)(cbbxm.SelectedItem)).Key + "'";
            }


            string sql = "select  case when tradetype=1 then '水费' when tradetype=2 then '电费' when tradetype=3 then '车辆年费'"
            + "when tradetype=4 then '房租'when tradetype=5 then '余额' when tradetype=6 then '地泵扣费' when tradetype=7 then '车辆出入扣费' "
            + "when tradetype=8 then '销户' when tradetype=10 then '广告租赁费' when tradetype=11 then '押金'  else '其他' end   类型,zjrxm 姓名,case when isnull(MerNum,'')='' then '市场卡' else dbo.GetMerName(MerNum) end 商户,"
            + "createdate 时间,cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2)) 金额,case when iscash =1 then '现金' else '网络' end 缴费方式 "
            + "from  dbo.S_Water where '" + start + "'<=CreateDate and CreateDate<='" + end + "' " + where + " " + where3 + " " + where2 + "  and RetCode='00' order by createdate desc";
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
            VBprinter.DGVprint dgVprint1 = new VBprinter.DGVprint();
            dgVprint1.MainTitle = "流水详细查询" + start + "-"+end+"-" + ((ListItem)(cbbxm.SelectedItem)).Value;
            dgVprint1.AutoWrap = true;
            dgVprint1.ZoomToPaperWidth = true;
            dgVprint1.Print(dataGridView1, false);
        }
    }
}
