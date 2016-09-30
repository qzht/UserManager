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
    public partial class JiaoJieBan : Temp
    {
        public JiaoJieBan()
        {
            InitializeComponent();
            this.printDocument1.OriginAtMargins = false;//启用页边距
            this.pageSetupDialog1.EnableMetric = true; //以毫米为单位
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        string Type = "1";//1 充值2 消费

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
                DataTable temp = db.ExecuteDataTable("select top 1 *from  dbo.RecordWork where userid='" + Common.UserId + "' order by LoginTime desc");
                db.NonQuery(" update RecordWork set UnLoginTime='" + txtEndTime.Text + "',CZCount='" + txtCzCount.Text + "',CZSum='" + txtCzSum.Text + "',XKCount='" + txtXkCount.Text + "',XKSum='" + txtXkSum.Text + "',AllSum='" + txtAllSum.Text + "' where id='" + temp.Rows[0]["id"].ToString() + "' ");
           
            }
            //   printPreviewDialog1.ShowDialog();
        }

        /// <summary>
        /// 消费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
                DataTable temp = db.ExecuteDataTable("select top 1 *from  dbo.RecordWork where userid='" + Common.UserId + "' order by LoginTime desc");
                db.NonQuery(" update RecordWork set UnLoginTime='" + txtXFEndtime.Text + "',XFCount='"+txtXFCount.Text+"',XFSum='"+txtXFSum.Text+"',XFXJ='"+txtXFXJ.Text+"',XFCZK='"+txtXFCZK.Text+"' where id='" + temp.Rows[0]["id"].ToString() + "' ");
            }
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JiaoJieBan_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font = new Font("宋体", 8);
            Brush bru = Brushes.Black;
            if (Type == "1")
            {
                #region 充值操作员交接单
                e.Graphics.DrawString("衡水东明村集团", font, bru, 45, 20);
                e.Graphics.DrawString("一卡通交接单", font, bru, 48, 40);
                e.Graphics.DrawString("收费单位：" + txtCompany.Text + "", font, bru, 10, 70);
                e.Graphics.DrawString("操作员：" + txtName.Text + "", font, bru, 22, 90);
                e.Graphics.DrawString("时间自：" + txtStarttime.Text + "", font, bru, 22, 110);
                e.Graphics.DrawString("至：" + txtEndTime.Text + "", font, bru, 44, 130);
                e.Graphics.DrawString("充值笔数：" + txtCzCount.Text + "", font, bru, 10, 150);
                e.Graphics.DrawString("充值总额：" + txtCzSum.Text + "", font, bru, 10, 170);
                e.Graphics.DrawString("销卡笔数：" + txtXkCount.Text + "", font, bru, 10, 190);
                e.Graphics.DrawString("销卡总额：" + txtXkSum.Text + "", font, bru, 10, 210);
                e.Graphics.DrawString("本班次余额：" + txtAllSum.Text + "", font, bru, 10, 230);
                e.Graphics.DrawString("      ", font, bru, 10, 250);
                #endregion
            }
            else
            {
                #region 消费操作员交接单
                string where = " and TermID='U_" + Common.UserId + "'";
                if (Common.userName == "admin")
                {
                    where = "";
                }
                txtXFSum.Text = db.Scalar("select isnull(cast(sum(cast(transmoney as decimal(10,2)))/100,0) as decimal(10,2)) from  dbo.S_Water where '" + txtStarttime.Text + "'<=CreateDate and CreateDate>='" + txtEndTime.Text + "' " + where + " AND TradeType in (6,7)").ToString();
                txtXFSum.Text = (decimal.Parse(txtXFSum.Text) + decimal.Parse(txtXFXJ.Text)).ToString();

                e.Graphics.DrawString("衡水东明村集团", font, bru, 45, 20);
                e.Graphics.DrawString("交接单", font, bru, 60, 40);
                e.Graphics.DrawString("收费单位：" + txtXFCompany.Text + "", font, bru, 10, 70);
                e.Graphics.DrawString("收费员：" + txtXFName.Text + "", font, bru, 22, 90);
                e.Graphics.DrawString("时间自：" + txtXFStarttime.Text + "", font, bru, 22, 110);
                e.Graphics.DrawString("至：" + txtXFEndtime.Text + "", font, bru, 44, 130);
                e.Graphics.DrawString("交易笔数：" + txtXFCount.Text + "", font, bru, 10, 150);
                e.Graphics.DrawString("收费金额：" + txtXFSum.Text + "", font, bru, 10, 170);
                e.Graphics.DrawString("现金：" + txtXFXJ.Text + "", font, bru, 32, 190);
                e.Graphics.DrawString("储值卡：" + txtXFCZK.Text + "", font, bru, 22, 210);
                e.Graphics.DrawString("      ", font, bru, 10, 250);
                #endregion
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                Type = "1";
            }
            if (tabControl1.SelectedTab == tabPage2)
            {
                Type = "2";
            }
        }

        /// <summary>
        /// 充值查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            #region 绑定充值

            string where = " and TermID='U_" + Common.UserId + "'";
            if (Common.userName == "admin")
            {
                where = "";
            }
            txtCompany.Text = Common.Company;
            txtName.Text = Common.realName;
            txtStarttime.Text = Common.LoginTime;
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtCzCount.Text = db.Scalar("select count(*) from  dbo.S_Water where '" + txtStarttime.Text + "'<=CreateDate and CreateDate<='" + txtEndTime.Text + "' " + where + " AND TradeType in (1,2,3,4,5,10,11)").ToString();
            txtCzSum.Text = db.Scalar("select isnull(cast(sum(cast(transmoney as decimal(10,2)))/100 as decimal(10,2)),0) from  dbo.S_Water where '" + txtStarttime.Text + "'<=CreateDate and CreateDate<='" + txtEndTime.Text + "' " + where + " AND TradeType in (1,2,3,4,5,10,11)").ToString();
            txtXkCount.Text=db.Scalar("select count(*) from  dbo.S_Water where '" + txtStarttime.Text + "'<=CreateDate and CreateDate<='" + txtEndTime.Text + "' " + where + " AND TradeType in (8)").ToString();
            txtXkSum.Text = db.Scalar("select isnull(cast(sum(cast(transmoney as decimal(10,2)))/100 as decimal(10,2)),0) from  dbo.S_Water where '" + txtStarttime.Text + "'<=CreateDate and CreateDate<='" + txtEndTime.Text + "' " + where + " AND TradeType in (8)").ToString();
            txtAllSum.Text = (decimal.Parse(txtCzSum.Text) - decimal.Parse(txtXkSum.Text)).ToString();
            #endregion
        }

        /// <summary>
        /// 消费查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            #region 绑定消费
            string where = " and TermID='U_" + Common.UserId + "'";
            if (Common.userName == "admin")
            {
                where = "";
            }
            txtXFCompany.Text = Common.Company;
            txtXFName.Text = Common.realName;
            txtXFStarttime.Text = Common.LoginTime;
            txtXFEndtime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtXFCount.Text=db.Scalar("select count(*) from  dbo.S_Water where '" + txtStarttime.Text + "'<=CreateDate and CreateDate>='" + txtEndTime.Text + "' " + where + " AND TradeType in (6,7)").ToString();
            txtXFSum.Text = db.Scalar("select isnull(cast(sum(cast(transmoney as decimal(10,2)))/100 as decimal(10,2)),0) from  dbo.S_Water where '" + txtStarttime.Text + "'<=CreateDate and CreateDate>='" + txtEndTime.Text + "' " + where + " AND TradeType in (6,7)").ToString();
            txtXFCZK.Text = db.Scalar("select isnull(cast(sum(cast(transmoney as decimal(10,2)))/100 as decimal(10,2)),0) from  dbo.S_Water where '" + txtStarttime.Text + "'<=CreateDate and CreateDate>='" + txtEndTime.Text + "' " + where + " AND TradeType in (6,7) and IsCash='0'").ToString();
            txtXFXJ.Text = db.Scalar("select isnull(cast(sum(cast(transmoney as decimal(10,2)))/100 as decimal(10,2)),0) from  dbo.S_Water where '" + txtStarttime.Text + "'<=CreateDate and CreateDate>='" + txtEndTime.Text + "' " + where + " AND TradeType in (6,7)  and IsCash='1'").ToString();
           // txtXFSum.Text = (decimal.Parse(txtXFSum.Text) + decimal.Parse(txtXFXJ.Text)).ToString();

            #endregion
        }
    }
}
