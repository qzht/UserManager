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
    public partial class AuditFee : Temp
    {
        public AuditFee()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");

        private void button1_Click(object sender, EventArgs e)
        {
            //打印票据
            //因为直接发送数据给小票机会出现丢失数据问题，所以我一般先把文件保存在本地，然后打印
            //string printername = "中崎 AB-58GK";//这个要看打印机配置成什么名字，例如我是中崎 AB-58MK
            //string filename = @"e:\demo.txt";
            //string content = "hello world";
            //  File.WriteAllText(filename, content, Encoding.GetEncoding("gb2312"));
            //RawPrinterHelper.SendStringToPrinter(printername, "test");
            string m = nudCount.Value.ToString();
            string endtime = DateTime.Now.AddMonths(-int.Parse(m)).ToString("yyyy-MM-dd HH:mm:ss");
            string type = cbbtype.Text;
            string where = "";
            if (type == "水费")
            {
                where = " and tradetype='1'";
            }
            if (type == "电费")
            {
                where = " and tradetype='2'";
            }
            if (type == "全部")
            {
                where = " and tradetype='1' or tradetype='2'";
            }
            //string sql = "select zjrxm 姓名,phoneNum 电话,dbo.GetMerName(MerNum) 商户,Opentime 开户时间,canceltime 销户时间 from MerAllInfo_View where mernum not in (select mernum from  dbo.S_Water "
            //           + " where isnull(mernum,'')!='' " + where + " and retcode='00'and createdate>= '" + endtime + "')";

            string sql = " select d.zjrxm 姓名,d.phoneNum 电话,cast(cast(c.transmoney as decimal(10,2))/100  as  decimal(10,2)) 缴费金额,c.类型,c.createdate 缴费日期,dbo.GetMerName(d.MerNum) 商户,d.Opentime 开户时间 from MerAllInfo_View d,( select B.mernum,B.transmoney,A.createdate,case when B.tradetype=1 then '水费' else '电费' end 类型  from S_Water AS B right join ("
  + " select  mernum ,max(createdate) as createdate from S_Water where isnull(mernum,'')!='' " + where + " and retcode='00' "
  + " group by mernum having max(createdate)< '" + endtime + "' ) AS A On A.mernum = B.Mernum and A.createdate=b.createdate) c where d.mernum=c.mernum ";


            DataTable dt = db.ExecuteDataTable(sql);
            int width = 0;
            if (dt != null && dt.Rows.Count != 0)
            {
                dataGridView1.DataSource = dt;
                for (int i = 0; i < this.dataGridView1.Columns.Count; i++)//对于DataGridView的每一个列都调整
                {
                    this.dataGridView1.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);//将每一列都调整为自动适应模式
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
                MessageBox.Show("无信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void AuditFee_Load(object sender, EventArgs e)
        {
            cbbtype.Items.Insert(0, "全部");
            cbbtype.Items.Insert(1, "水费");
            cbbtype.Items.Insert(2, "电费");
            cbbtype.SelectedIndex = 0;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string m = nudCount.Value.ToString();
            string endtime = DateTime.Now.AddMonths(-int.Parse(m)).ToString("yyyy-MM-dd HH:mm:ss");
            string type = cbbtype.Text;
            string where = "";
            if (type == "水费")
            {
                where = " and tradetype='1'";
            }
            if (type == "电费")
            {
                where = " and tradetype='2'";
            }
            if (type == "全部")
            {
                where = " and tradetype='1' or tradetype='2'";
            }
            string sql = " select d.zjrxm 姓名,d.phoneNum 电话,cast(cast(c.transmoney as decimal(10,2))/100  as  decimal(10,2)) 缴费金额,c.类型,c.createdate 缴费日期,dbo.GetMerName(d.MerNum) 商户,d.Opentime 开户时间 from MerAllInfo_View d,( select B.mernum,B.transmoney,A.createdate,case when B.tradetype=1 then '水费' else '电费' end 类型  from S_Water AS B right join ("
  + " select  mernum ,max(createdate) as createdate from S_Water where isnull(mernum,'')!='' " + where + " and retcode='00' "
  + " group by mernum having max(createdate)< '" + endtime + "' ) AS A On A.mernum = B.Mernum and A.createdate=b.createdate) c where d.mernum=c.mernum ";

            DataTable dt = db.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "Excel文件|*.xls";
                saveDialog.FileName = Common.realName + "水电稽查" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                saveDialog.ShowDialog();
                string saveFileName = saveDialog.FileName;
                if (saveFileName.IndexOf(":") < 0)
                {
                    return; //被点了取消  
                }
                Excel.FromDataTableToExcelForWin(dt, Common.realName + "水电稽查", saveFileName);
                MessageBox.Show("导出成功");
            }
            else
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void nudCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
                e.Handled = true;
        }
    }
}
