using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace UserManager
{
    public partial class WaterTemp : Temp
    {
        public WaterTemp()
        {
            InitializeComponent();
            this.printDocument1.OriginAtMargins = false;//启用页边距
            this.pageSetupDialog1.EnableMetric = true; //以毫米为单位
        }

        public string Month = "";
        public string OrderId = "";
        public string xm = "";
        public string MerName = "";
        public string cs = "";
        public string gsl = "";
        public string zb = "";
        public string dj = "";
        public string je = "";

        private void WaterTemp_Load(object sender, EventArgs e)
        {
            txtMonth1.Text = Month;
            txtMonth2.Text = Month;
            txtOrder1.Text = OrderId;
            txtOrder2.Text = OrderId;
            txtxm1.Text = xm;
            txtxm2.Text = xm;
            txtMerName1.Text = MerName;
            txtMerName2.Text = MerName;
            txtcs1.Text = cs;
            txtcs2.Text = cs;
            txtgsl1.Text = gsl;
            txtgsl2.Text = gsl;
            txtzb1.Text = zb;
            txtzb2.Text = zb;
            txtdj1.Text = dj;
            txtdj2.Text = dj;
            txtmoney1.Text = je;
            txtmoney2.Text = je;
            txtje1.Text =RMB.CmycurD(je);
            txtje2.Text = RMB.CmycurD(je);
           
            txtPayTime1.Text = DateTime.Now.ToString("yyyy.MM.dd");
            txtPayTime2.Text = DateTime.Now.ToString("yyyy.MM.dd");
            txtsfy1.Text = Common.realName;
            txtsfy2.Text = Common.realName;
        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = true;
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
                printDocument1.DefaultPageSettings.Landscape = true;
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
    }
}
