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
    public partial class ShouJuReport : Temp
    {
        public ShouJuReport()
        {
            InitializeComponent();
            this.printDocument1.OriginAtMargins = false;//启用页边距
            this.pageSetupDialog1.EnableMetric = true; //以毫米为单位
        }
        public string MerUserName = "";
        public string MerName = "";
        public string Money = "";
        public string OrderId = "";
        public string Remark = "";
        public string Type = "1";//1水费2电费
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

        /// <summary>
        /// 排版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (item is RichTextBox)
                {
                    Control tx = (item as Control);
                    g.DrawString(tx.Text, font, Brushes.Black, tx.Left + x, tx.Top + y);
                }

            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShouJuReport_Load(object sender, EventArgs e)
        {
            txtCompany.Text = Common.Company;
            txtPayTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtOrder.Text = OrderId;
            txtMerUserName.Text = MerUserName;
            txtMerName.Text = MerName;
            txtPayType.Text = "现金";
            txtPayMoeny.Text = Money;
            txtPayMoney2.Text = Money;
            txtPayMoenyUP.Text = RMB.CmycurD(Money);
            txtsfy.Text = Common.realName;
            richTextBox1.Text = Remark;
        }
    }
}
