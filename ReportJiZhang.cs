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
    public partial class ReportJiZhang : Temp
    {
        public ReportJiZhang()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportJiZhang_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dtpstart.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00"); ;
            dtpend.Text = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

            cbbtype.Items.Insert(0, "全部");
            cbbtype.Items.Insert(1, "现金");
            cbbtype.Items.Insert(2, "网络");
            cbbtype.SelectedIndex = 0;

            cbbxm.Items.Add(new ListItem("0", "全部"));
            // cbbxm.Items.Add(new ListItem("1", "水费"));
            // cbbxm.Items.Add(new ListItem("2", "电费"));
            cbbxm.Items.Add(new ListItem("3", "车辆年费"));
            cbbxm.Items.Add(new ListItem("4", "房租"));
            //cbbxm.Items.Add(new ListItem("5", "余额"));
            // cbbxm.Items.Add(new ListItem("6", "地泵扣费"));
            //  cbbxm.Items.Add(new ListItem("7", "车辆出入扣费"));
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
            else
            {
                where3 += " and TradeType in(3,4,10,11)";
            }


            string sql = "select  case when tradetype=1 then '水费' when tradetype=2 then '电费' when tradetype=3 then '车辆年费'"
            + "when tradetype=4 then '房租'when tradetype=5 then '余额' when tradetype=6 then '地泵扣费' when tradetype=7 then '车辆出入扣费' "
            + "when tradetype=8 then '销户' when tradetype=10 then '广告租赁费' when tradetype=11 then '押金'  else '其他' end   类型,zjrxm 姓名,case when isnull(MerNum,'')='' then '市场卡' else dbo.GetMerName(MerNum) end 商户,"
            + "createdate 时间,cast(cast(transmoney as decimal(10,2))/100 as decimal(10,2)) 金额,case when iscash =1 then '现金' else '网络' end 缴费方式,MerNum 商户编号 ,OrderId 流水号,dbo.GetUserName(TermID) 操作员,dbo.GetCompany(TermID) 收费单位 "
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

        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            if (RowIndex == -1)
            {
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string type = dataGridView1.Rows[RowIndex].Cells[0].Value.ToString();
                string MerUserName = dataGridView1.Rows[RowIndex].Cells[1].Value.ToString();
                string MerName = dataGridView1.Rows[RowIndex].Cells[2].Value.ToString();
                string MerNum = dataGridView1.Rows[RowIndex].Cells[6].Value.ToString();
                string OrderId = dataGridView1.Rows[RowIndex].Cells[7].Value.ToString();
                string Company = dataGridView1.Rows[RowIndex].Cells[9].Value.ToString();
                string sfy = dataGridView1.Rows[RowIndex].Cells[8].Value.ToString();
                string Money = dataGridView1.Rows[RowIndex].Cells[4].Value.ToString();
                switch (type)
                {
                    case "房租":
                        FzReport f = new FzReport();
                        f.Type = 1;
                        f.MerUserName = MerUserName;
                        f.MerName = MerName;
                        f.MerNum = MerNum;
                        f.skr = sfy;
                        f.Company = Company;
                        f.FangZu = Money;
                        f.OrderId = OrderId;
                        f.Remark = "记账";
                        f.Owner = this;
                        f.ShowDialog();
                        break;
                    case "押金":

                       TempYJ yj = new TempYJ();
                       yj.Type = 1;
                       yj.sfxm = "房租押金";
                       yj.fzr = MerUserName;
                       yj.MerName = MerName;
                       yj.MerNum = MerNum;
                       yj.skr = sfy;
                       yj.Company = Company;
                       yj.Money = Money;
                       yj.OrderId = OrderId;
                       yj.Owner = this;
                       yj.ShowDialog();
                        break;
                    case "车辆年费":

                        TempOther t = new TempOther();
                        t.Type = 1;
                        t.sfxm = "车辆年费";
                        t.jkr = MerUserName;
                        t.MerName = MerName;
                        t.MerNum = MerNum;
                        t.skr = sfy;
                        t.Company = Company;
                        t.Money = Money;
                        t.OrderId = OrderId;
                        t.Owner = this;
                        t.ShowDialog();
                        break;
                    case "广告租赁费":

                        TempOther o = new TempOther();
                        o.Type = 1;
                        o.PayType = 1;
                        o.jkr = MerUserName;
                        o.MerName = MerName;
                        o.MerNum = MerNum;
                        o.skr = sfy;
                        o.Company = Company;
                        o.Money = Money;
                        o.OrderId = OrderId;
                        o.Owner = this;
                        o.ShowDialog();
                        break;
                    default:
                        MessageBox.Show("该缴费类型没有凭证", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }

            }
        }
    }
}
