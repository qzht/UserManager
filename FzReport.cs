using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using XK.NBear.DB;

namespace UserManager
{
    public partial class FzReport : Temp
    {
        public FzReport()
        {
            InitializeComponent();
            this.printDocument1.OriginAtMargins = false;//启用页边距
            this.pageSetupDialog1.EnableMetric = true; //以毫米为单位
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        public string MerUserName = "";
        public string MerName = "";
        public string ShiYong = "";
        public string WuYe = "";
        public string ZhiAn = "";
        public string ZhanDi = "";
        public string ShiChang = "";
        public string PayTime = "";
        public string FangZu = "";
        public string OrderId = "";
        public string Remark = "";
        public int Type = 0;//0传值1查库
        public string MerNum = "";
        public string skr = "";
        public string Company = "";
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FzReport_Load(object sender, EventArgs e)
        {
            if (Type == 0)
            {
                txtCompany.Text = Common.Company;
                txtOrder.Text = OrderId;
                txtYear.Text = DateTime.Now.Year.ToString();
                txtMonth.Text = DateTime.Now.Month.ToString();
                txtDay.Text = DateTime.Now.Day.ToString();
                txtXm.Text = MerUserName;
                txtAdress.Text = MerName;
                txtshiyong.Text = ShiYong;
                txtwuye.Text = WuYe;
                txtzhian.Text = ZhiAn;
                txtzhandi.Text = ZhanDi;
                txtshichang.Text = ShiChang;
                txtTime.Text = PayTime;
                txtjkr.Text = MerUserName;
                txtskr.Text = Common.realName;
                txtmoney.Text = FangZu;
                txtRemark.Text = Remark;
                string rmb = RMB.CmycurD(FangZu);
                txtmoneyUP.Text = rmb;
            }
            else
            {

                txtCompany.Text = Company;
                txtOrder.Text = OrderId;
                txtYear.Text = DateTime.Now.Year.ToString();
                txtMonth.Text = DateTime.Now.Month.ToString();
                txtDay.Text = DateTime.Now.Day.ToString();
                txtXm.Text = MerUserName;
                txtAdress.Text = MerName;
                DataTable dt = db.ExecuteDataTable(" select * from MerInfo where MerNum='" + MerNum + "'");
                if (dt != null && dt.Rows.Count != 0)
                {
                    txtshiyong.Text = dt.Rows[0]["Fangwushiyong"].ToString();
                    txtwuye.Text = dt.Rows[0]["Wuyefuwu"].ToString();
                    txtzhian.Text = dt.Rows[0]["Zhianguanli"].ToString();
                    txtzhandi.Text = dt.Rows[0]["Zhandibuchang"].ToString(); ;
                    txtshichang.Text = dt.Rows[0]["Shichangguanli"].ToString();
                    string start = dt.Rows[0]["FangZuStart"].ToString();
                    string end = dt.Rows[0]["FangZuEnd"].ToString();
                    if (!string.IsNullOrWhiteSpace(start))
                    {
                        start = DateTime.Parse(start).ToString("yyyy.MM.dd");
                    }
                    if (!string.IsNullOrWhiteSpace(end))
                    {
                        end = DateTime.Parse(end).ToString("yyyy.MM.dd");
                    }
                    txtTime.Text = start + "-" + end;
                }

                txtjkr.Text = MerUserName;
                txtskr.Text = skr;
                txtmoney.Text = FangZu;
                txtRemark.Text = Remark;
                string rmb = RMB.CmycurD(FangZu);
                txtmoneyUP.Text = rmb;
            }
        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            PrintDialog MySettings = new PrintDialog();
            MySettings.Document = this.printDocument1;

            PaperSize p = null;
            foreach (PaperSize ps in MySettings.Document.PrinterSettings.PaperSizes)
            {
                if (ps.PaperName.Equals("A4"))//这里设置纸张大小,但必须是定义好的   
                    p = ps;
            }
            MySettings.Document.DefaultPageSettings.PaperSize = p;
            if (MySettings.ShowDialog() == DialogResult.OK)
            {
                PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.Show();
            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;   //先建立画布 
            int x = 80;
            int y = 60;
            Font font = new Font("宋体", 12);

            g.DrawImage(this.BackgroundImage, 50, 50);
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    Control tx = (item as Control);
                    g.DrawString(tx.Text, font, Brushes.Black, tx.Left + x, tx.Top + y);
                }

            }
        }
    }
}
