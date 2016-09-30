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
    public partial class TempOther : Temp
    {
        public TempOther()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        public string OrderId = "";
        public string Company = "";
        public string sfxm = "";
        public string MerName = "";
        public string Remark = "";
        public string PayTime = "";
        public string Money = "";
        public string jkr = "";
        public int Type = 0;//0传值1查库
        public int PayType = 0;//0车辆年费1广告租赁费
        public string MerNum = "";
        public string skr = "";
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

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TempOther_Load(object sender, EventArgs e)
        {
            if (Type == 0)
            {
                txtOrderId.Text = OrderId;
                txtCompany.Text = Common.Company;
                txtyear.Text = DateTime.Now.ToString("yyyy");
                txtMonth.Text = DateTime.Now.ToString("MM");
                txtday.Text = DateTime.Now.ToString("dd");
                txtsfxm.Text = sfxm;
                txtMerName.Text = MerName;
                txtRemark.Text = Remark;
                txtPaydate.Text = PayTime;
                txtMoneyUp.Text = RMB.CmycurD(Money);
                txtMoney.Text = Money;
                txtjkr.Text = jkr;
                txtskr.Text = Common.realName;
            }
            else
            {
                txtOrderId.Text = OrderId;
                txtCompany.Text = Company;
                txtyear.Text = DateTime.Now.ToString("yyyy");
                txtMonth.Text = DateTime.Now.ToString("MM");
                txtday.Text = DateTime.Now.ToString("dd");
                txtsfxm.Text = sfxm;
                txtMerName.Text = MerName;
                txtMoneyUp.Text = RMB.CmycurD(Money);
                txtMoney.Text = Money;
                txtjkr.Text = jkr;
                txtskr.Text =skr;
                DataTable dt = db.ExecuteDataTable(" select * from MerAllInfo_View where MerNum='" + MerNum + "'");
                if (dt != null && dt.Rows.Count != 0)
                {
                    string start = dt.Rows[0]["CarStart"].ToString();
                    string end = dt.Rows[0]["CarEnd"].ToString();
                    if (PayType == 0)//车辆年费
                    {
                        if (!string.IsNullOrWhiteSpace(start))
                        {
                            start = DateTime.Parse(start).ToString("yyyy.MM.dd");
                        }
                        if (!string.IsNullOrWhiteSpace(end))
                        {
                            end = DateTime.Parse(end).ToString("yyyy.MM.dd");
                        }
                        txtRemark.Text = dt.Rows[0]["CarNum"].ToString();
                        txtPaydate.Text = start + "-" + end;
                    }
                    else//广告租赁费
                    {
                        start = dt.Rows[0]["AdStart"].ToString();
                        end = dt.Rows[0]["AdEnd"].ToString();
                        if (!string.IsNullOrWhiteSpace(start))
                        {
                            start = DateTime.Parse(start).ToString("yyyy.MM.dd");
                        }
                        if (!string.IsNullOrWhiteSpace(end))
                        {
                            end = DateTime.Parse(end).ToString("yyyy.MM.dd");
                        }
                        txtsfxm.Text = jkr +"-"+dt.Rows[0]["AdPosition"].ToString(); ;
                        txtRemark.Text = dt.Rows[0]["AdRemark"].ToString();
                        txtPaydate.Text = start + "-" + end;
                    }
                }
            }
        }
    }
}
