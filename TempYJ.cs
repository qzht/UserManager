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
    public partial class TempYJ : Temp
    {
        public TempYJ()
        {
            InitializeComponent();
            this.printDocument1.OriginAtMargins = false;//启用页边距
            this.pageSetupDialog1.EnableMetric = true; //以毫米为单位
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
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

        public string OrderId = "";
        public string Company = "";
        public string sfxm = "";
        public string zjhm = "";
        public string MerName = "";
        public string fzr = "";
        public string jyxm = "";
        public string Money = "";
        public int Type = 0;//0传值1查库
        public string MerNum = "";
        public string skr = "";

        private void TempYJ_Load(object sender, EventArgs e)
        {
            if (Type == 0)
            {
                txtCompany.Text = Common.Company;
                txtyear.Text = DateTime.Now.ToString("yyyy");
                txtmonth.Text = DateTime.Now.ToString("MM");
                txtday.Text = DateTime.Now.ToString("dd");
                txtOrder.Text = OrderId;
                txtsfxm.Text = sfxm;
                txtzjhm.Text = zjhm;
                txtMerName.Text = MerName;
                txtfzr.Text = fzr;
                txtjyxm.Text = jyxm;
                txtMoney.Text = Money;
                txtMoneyUp.Text = RMB.CmycurD(Money);
                txtjkr.Text = fzr;
                txtskr.Text = Common.realName;
            }
            else
            {
                txtCompany.Text = Company;
                txtyear.Text = DateTime.Now.ToString("yyyy");
                txtmonth.Text = DateTime.Now.ToString("MM");
                txtday.Text = DateTime.Now.ToString("dd");
                txtOrder.Text = OrderId;
                txtsfxm.Text = sfxm;

                txtMerName.Text = MerName;
                txtfzr.Text = fzr;
                txtjyxm.Text = jyxm;
                txtMoney.Text = Money;
                txtMoneyUp.Text = RMB.CmycurD(Money);
                txtjkr.Text = fzr;
                txtskr.Text = skr;
                DataTable dt = db.ExecuteDataTable(" select * from MerAllInfo_View where MerNum='" + MerNum + "'");
                if (dt != null && dt.Rows.Count != 0)
                {
                    txtzjhm.Text = dt.Rows[0]["ZJHM"].ToString();
                }
            }
        }
    }
}
