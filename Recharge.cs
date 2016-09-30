using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XK.NBear.DB;
using dmsk;
using System.Drawing.Printing;
using System.Threading;

namespace UserManager
{
    public partial class Recharge : Form
    {
        public Recharge()
        {
            InitializeComponent();
            this.printDocument1.OriginAtMargins = false;//启用页边距
            this.pageSetupDialog1.EnableMetric = true; //以毫米为单位
        }
        common com = new common();
        int CardType = -1;//-1默认 0 水卡 1电卡
        public string Type = "1";//1水卡2电卡3余额
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");

        #region 按钮事件

        /// <summary>
        /// 读水卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            string msg = "";
            string carduid = com.get_UID(out msg);
            if (carduid == "-1")
            {
                txtcarid.Text = "";
                txtxm.Text = "";
                txtsfz.Text = "";
                txtmerName.Text = "";
                button2.Enabled = true;
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string result = (db.Scalar("select count(*) from MerInfo where shuicardnum='" + carduid + "'") ?? "0").ToString();
                if (result != "1")
                {
                    button2.Enabled = true;
                    MessageBox.Show("无此卡信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    CardType = 0;
                    DataTable dt = db.ExecuteDataTable(" select * from  dbo.MerAllInfo_View where ShuiCardNum='" + carduid + "' ");
                    txtcarid.Text = carduid;
                    txtxm.Text = dt.Rows[0]["ZJRXM"].ToString();
                    txtsfz.Text = dt.Rows[0]["ZJHM"].ToString();
                    if (string.IsNullOrWhiteSpace(dt.Rows[0]["MerNum"].ToString()))
                    {
                        txtmerName.Text = "市场卡";
                    }
                    else
                    {
                        txtmerName.Text = GetMerName(dt.Rows[0]["MerNum"].ToString());
                    }
                    //   txtCPHM.Text = dt.Rows[0]["CarNum"].ToString();
                    //   nudyue.Value = int.Parse((dt.Rows[0]["Yue"]??"0").ToString());
                    //   nudHouse.Value = decimal.Parse((dt.Rows[0]["FangZu"] ?? "0").ToString() == "-1.00" ? "0" : (dt.Rows[0]["FangZu"] ?? "0").ToString());
                    //  nudCar.Value = decimal.Parse((dt.Rows[0]["CarNianFei"] ?? "0").ToString() == "-1.00" ? "0" : (dt.Rows[0]["CarNianFei"] ?? "0").ToString());
                }
            }
            button2.Enabled = true;
        }

        /// <summary>
        /// 读取电卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            button10.Enabled = false;
            string msg = "", carduid = "", buycount = "";
            int flag = com.readusercard(out buycount, out carduid);
            carduid = carduid.PadLeft(6,'0');
            if (flag != 0)
            {
                txtcarid.Text = "";
                txtxm.Text = "";
                txtsfz.Text = "";
                txtmerName.Text = "";
                button10.Enabled = true;
                MessageBox.Show("读卡失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string result = (db.Scalar("select count(*) from MerInfo where DianCardNum='" + carduid + "'") ?? "0").ToString();
                if (result != "1")
                {
                    button10.Enabled = true;
                    MessageBox.Show("读取用户信息失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    CardType = 1;
                    DataTable dt = db.ExecuteDataTable(" select * from  dbo.MerAllInfo_View where DianCardNum='" + carduid + "' ");
                    txtcarid.Text = carduid;
                    txtbuyDiancount.Text = buycount;
                    txtxm.Text = dt.Rows[0]["ZJRXM"].ToString();
                    txtsfz.Text = dt.Rows[0]["ZJHM"].ToString();
                    txtmerName.Text = GetMerName(dt.Rows[0]["MerNum"].ToString());
                    tabControl1.SelectedTab = tabPage2;
                    //txtCPHM.Text = dt.Rows[0]["CarNum"].ToString();
                    ////   nudyue.Value = int.Parse((dt.Rows[0]["Yue"]??"0").ToString());
                    //nudHouse.Value = decimal.Parse((dt.Rows[0]["FangZu"] ?? "0").ToString() == "-1.00" ? "0" : (dt.Rows[0]["FangZu"] ?? "0").ToString());
                    //nudCar.Value = decimal.Parse((dt.Rows[0]["CarNianFei"] ?? "0").ToString() == "-1.00" ? "0" : (dt.Rows[0]["CarNianFei"] ?? "0").ToString());
                }
            }
            button10.Enabled = true;
        }

        /// <summary>
        /// 水卡充值水费1 ，电费2 ，车辆年费3， 房租4 ，钱包5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                txtWaterOrder.Text = "";
                if (CardType != 0)
                {
                    MessageBox.Show("请把卡放到水卡读卡器并点击读水卡按钮", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string msg = "", jfcs = "0", sbbh = "", sql = "";
                string WaterBuyMoney = nudWaterBuyMoney.Value.ToString();
                string Price = txtWaterPrice.Text.Trim();

                if (string.IsNullOrWhiteSpace(Price))
                {
                    MessageBox.Show("请在系统设置中设置水价格", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(WaterBuyMoney) || WaterBuyMoney == "0")
                {
                    MessageBox.Show("请输入充值金额", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string carduid = com.get_UID(out msg);
                if (carduid == "-1")
                {
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string result = (db.Scalar("select count(*) from MerInfo where shuicardnum='" + carduid + "'") ?? "0").ToString();
                if (result != "1")
                {
                    MessageBox.Show("无此卡信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string MerNum = (db.Scalar("select MerNum from MerInfo where shuicardnum='" + carduid + "'") ?? "0").ToString();
                if (string.IsNullOrWhiteSpace(MerNum))
                {
                    MessageBox.Show("市场卡不能充水费", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("确定是否收到现金?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    button4.Enabled = false;
                    txtWaterTotal.Text = (float.Parse(WaterBuyMoney) / float.Parse(Price)).ToString("0.0");
                    int flag = com.jiaofei(float.Parse(txtWaterTotal.Text), out msg, out jfcs, out sbbh);
                    if (flag == 0)
                    {
                        txtbuyWatercount.Text = jfcs;
                        sql = " update MerInfo set sbbh='" + sbbh + "', jfcs='" + (int.Parse(jfcs)) + "',zgl=zgl+'" + float.Parse(txtWaterTotal.Text) + "' where shuicardnum='" + carduid + "'";
                        db.NonQuery(sql);
                        string TradeDate = DateTime.Now.ToString("yyyyMMdd");
                        string TradeTime = DateTime.Now.ToString("HH:mm");
                        string OrderID = Common.GetOrder("SF");
                        txtWaterOrder.Text = OrderID;
                        string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        sql = "insert into S_Water(OrderID,MerNum,RetCode,TermID,ZJRXM,ZJHM,TradeDate,TradeTime,LiquiDate,cardid,TransMoney,TradeType,IsCash,CreateDate) values ('" + OrderID + "','" + MerNum + "','00','U_" + Common.UserId + "','" + txtxm.Text + "','" + txtsfz.Text + "','" + TradeDate + "','" + TradeTime + "','" + TradeDate + "','" + txtcarid.Text + "','" + int.Parse((nudWaterBuyMoney.Value * 100).ToString("0")) + "',1,1,'"+createtime+"')";
                        db.NonQuery(sql);
                        Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtxm.Text + "水卡编号" + txtcarid.Text+ "充值" + nudWaterBuyMoney.Value + "元");

                        MessageBox.Show("充值成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    button4.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 电卡充值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            txtDianOrder.Text = "";
            if (CardType != 1)
            {
                MessageBox.Show("请把卡放到电卡读卡器并点击读电卡按钮", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string DianBuyMoney = nudDianBuyMoney.Value.ToString();
            string Price = txtDianPrice.Text.Trim();
            string sql = "", msg = "";
            if (string.IsNullOrWhiteSpace(Price))
            {
                MessageBox.Show("请在系统设置中设置电价格", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(DianBuyMoney) || DianBuyMoney == "0")
            {
                MessageBox.Show("请输入充值金额", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string result = (db.Scalar("select count(*) from MerInfo where DianCardNum='" + txtcarid.Text + "'") ?? "0").ToString();
            if (result != "1")
            {
                MessageBox.Show("无此卡信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //string MerNum = (db.Scalar("select MerNum from MerInfo where shuicardnum='" + carduid + "'") ?? "0").ToString();
            string MerNum = (db.Scalar("select MerNum from MerInfo where  DianCardNum='" + txtcarid.Text + "'") ?? "0").ToString();
            if (string.IsNullOrWhiteSpace(MerNum))
            {
                MessageBox.Show("市场卡不能充电费", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtDianTotal.Text = (float.Parse(DianBuyMoney) / float.Parse(Price)).ToString("0");
            if (DialogResult.Yes == MessageBox.Show("确定是否收到现金?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                button1.Enabled = false;
                int flag = com.shoudian(uint.Parse(txtDianTotal.Text), out msg);//com.jiaofei(float.Parse(txtWaterTotal.Text), out msg, out jfcs, out sbbh);
                if (flag == 0)
                {
                    sql = " update MerInfo set GouDianNum='" + (int.Parse(txtbuyDiancount.Text) + 1) + "',DGDL='" + txtDianTotal.Text + "' where DianCardNum='" + txtcarid.Text + "'";
                    db.NonQuery(sql);
                    string TradeDate = DateTime.Now.ToString("yyyyMMdd");
                    string TradeTime = DateTime.Now.ToString("HH:mm");
                    string OrderID = Common.GetOrder("DF");
                    txtDianOrder.Text = OrderID;
                    string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sql = "insert into S_Water(OrderID,MerNum,RetCode,TermID,ZJRXM,ZJHM,TradeDate,TradeTime,LiquiDate,cardid,TransMoney,TradeType,IsCash,CreateDate) values ('" + OrderID + "','" + MerNum + "','00','U_" + Common.UserId + "','" + txtxm.Text + "','" + txtsfz.Text + "','" + TradeDate + "','" + TradeTime + "','" + TradeDate + "','" + txtcarid.Text + "','" + int.Parse((nudDianBuyMoney.Value * 100).ToString("0")) + "',2,1,'" + createtime + "')";
                    db.NonQuery(sql);
                    Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtxm.Text + "电卡编号" + txtcarid.Text + "充值" + nudDianBuyMoney.Value + "元");

                    MessageBox.Show("充值成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                button1.Enabled = true;
            }
        }

        /// <summary>
        /// 查看余额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtcarid.Text))
            {
                MessageBox.Show("请先刷卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string result = (db.Scalar("select yue from MerInfo where shuicardnum='" + txtcarid.Text + "'") ?? "0").ToString();
            txtoldyue.Text = result;
        }

        /// <summary>
        /// 余额充值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            txtYueOrder.Text = "";
            if (CardType != 0)
            {
                MessageBox.Show("请把卡放到水卡读卡器并点击读水卡按钮", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtcarid.Text))
            {
                MessageBox.Show("请先刷卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudyue.Value == 0)
            {
                MessageBox.Show("充值金额不能为0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string msg = "";
            string carduid = com.get_UID(out msg);
            if (carduid == "-1")
            {
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string result = (db.Scalar("select count(*) from MerInfo where shuicardnum='" + carduid + "'") ?? "0").ToString();
            if (result != "1")
            {
                MessageBox.Show("无此卡信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = (db.Scalar("select MerNum from MerInfo where shuicardnum='" + carduid + "'") ?? "0").ToString();

            if (DialogResult.Yes == MessageBox.Show("确定是否收到现金?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                button6.Enabled = false;
                result = (db.Scalar("update MerInfo set yue=( case when yue=-1 then '" + nudyue.Value + "' else  '" + nudyue.Value + "' +yue end)  where shuicardnum='" + txtcarid.Text + "'") ?? "0").ToString();

                string TradeDate = DateTime.Now.ToString("yyyyMMdd");
                string TradeTime = DateTime.Now.ToString("HH:mm");
                string OrderID = Common.GetOrder("YE");
                txtYueOrder.Text = OrderID;
                string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "insert into S_Water(OrderID,MerNum,RetCode,TermID,ZJRXM,ZJHM,TradeDate,TradeTime,LiquiDate,cardid,TransMoney,TradeType,IsCash,CreateDate) values ('" + OrderID + "','" + MerNum + "','00','U_" + Common.UserId + "','" + txtxm.Text + "','" + txtsfz.Text + "','" + TradeDate + "','" + TradeTime + "','" + TradeDate + "','" + txtcarid.Text + "','" + int.Parse((nudyue.Value * 100).ToString("0")) + "',5,1,'"+createtime+"')";
                db.NonQuery(sql);
                Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtxm.Text + "卡编号" + txtcarid.Text + "余额充值" + nudyue.Value + "元");
                MessageBox.Show("充值成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button6.Enabled = true;
            }
        }


        #endregion

        #region 窗体事件

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recharge_Load(object sender, EventArgs e)
        {
            txtWaterPrice.Text = GetWaterPrice();
            txtDianPrice.Text = GetDianPrice();
        }


        #endregion

        #region 方法

        public string GetWaterPrice()
        {
            string sql = " select top 1 ShuiPrice from Price";
            return (db.Scalar(sql) ?? "0").ToString();
        }
        public string GetDianPrice()
        {
            string sql = " select top 1 DianPrice from Price";
            return (db.Scalar(sql) ?? "0").ToString();
        }

        public string GetMerName(string MerNum)
        {
            string name = "";
            string one = MerNum.Substring(0, 2);
            string two = MerNum.Substring(2, 2);
            string three = MerNum.Substring(4, 2);
            string four = MerNum.Substring(6, MerNum.Length - 6);
            one = (db.Scalar("select Name from  dbo.Dict_View where TypeName='One' and Value='" + one + "'") ?? "XX").ToString();
            two = (db.Scalar("select Name from  dbo.Dict_View where TypeName='Two' and Value='" + two + "'") ?? "XX").ToString();
            three = (db.Scalar("select Name from  dbo.Dict_View where TypeName='Three' and Value='" + three + "'") ?? "XX").ToString();
            name = one + two + three + four;
            return name;
        }

        #endregion



        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //打印内容 为 自定义文本内容 
            Font font = new Font("宋体", 8);
            Brush bru = Brushes.Black;
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (Type == "3")
            {
                e.Graphics.DrawString("衡水东明村集团（正联）", font, bru, 25, 20);
                e.Graphics.DrawString("储值卡充值单", font, bru, 50, 50);
                e.Graphics.DrawString("金额：" + nudyue.Value + "", font, bru, 10, 80);
                e.Graphics.DrawString("流水号：" + txtYueOrder.Text + "", font, bru, 10, 100);
                e.Graphics.DrawString("时间：" + time + "", font, bru, 10, 120);
                e.Graphics.DrawString("卡号：" + txtcarid.Text + "", font, bru, 10, 140);
                e.Graphics.DrawString("收费员：" + Common.realName + "", font, bru, 10, 160);
                e.Graphics.DrawString("交款人签字：", font, bru, 10, 180);

                e.Graphics.DrawString("衡水东明村集团（副联）", font, bru, 25, 210);
                e.Graphics.DrawString("储值卡充值单", font, bru, 50, 240);
                e.Graphics.DrawString("金额：" + nudyue.Value + "", font, bru, 10, 270);
                e.Graphics.DrawString("流水号：" + txtYueOrder.Text + "", font, bru, 10, 290);
                e.Graphics.DrawString("时间：" + time + "", font, bru, 10, 310);
                e.Graphics.DrawString("卡号：" + txtcarid.Text + "", font, bru, 10, 330);
                e.Graphics.DrawString("收费员：" + Common.realName + "", font, bru, 10, 350);
                e.Graphics.DrawString("交款人签字：", font, bru, 10, 370);
            }
            if (Type == "1")
            {
             
                string str;
                float xPos;       //x点坐标
                float yPos;       //y点的坐标
                float topMargin = 0;
                float leftMargin = 0;

                font = new Font("宋体", 12);
                str = "欢迎光临";
                xPos = leftMargin + 15;
                yPos = topMargin;
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin;
                yPos = yPos + font.GetHeight(e.Graphics) + 5;
                font = new Font("黑体", 12);
                str = "---------------------------------";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin + 25;
                yPos = yPos + font.GetHeight(e.Graphics) + 3;
                font = new Font("黑体", 18);
                str = "客 户 回 执 单";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin;
                yPos = yPos + font.GetHeight(e.Graphics) + 3;
                font = new Font("黑体", 12);
                str = "---------------------------------";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin;
                yPos = yPos + font.GetHeight(e.Graphics) + 5;
                font = new Font("宋体", 11);
                str = "  凭单号";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin;
                yPos = yPos + font.GetHeight(e.Graphics) + 5;
                font = new Font("宋体", 11);
                str = "业务类型";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin;
                yPos = yPos + font.GetHeight(e.Graphics) + 5;
                font = new Font("宋体", 11);
                str = "    卡号";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin;
                yPos = yPos + font.GetHeight(e.Graphics) + 10;
                font = new Font("宋体", 11);
                str = "  原余额";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin;
                yPos = yPos + font.GetHeight(e.Graphics) + 5;
                font = new Font("宋体", 11);
                str = "  现余额";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin;
                yPos = yPos + font.GetHeight(e.Graphics) + 5;
                font = new Font("黑体", 12);
                str = "---------------------------------";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin + 50;
                yPos = yPos + font.GetHeight(e.Graphics) + 5;
                font = new Font("宋体", 14);
                str = "客户签名";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin + 130;
                yPos = yPos + font.GetHeight(e.Graphics) - 5;
                font = new Font("宋体", 12);
                str = "-----------";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin;
                yPos = yPos + font.GetHeight(e.Graphics);
                font = new Font("宋体", 10);
                str = "受理单位名称";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin;
                yPos = yPos + font.GetHeight(e.Graphics) + 5;
                font = new Font("宋体", 10);
                str = "  操作员名称";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin + 10;
                yPos = yPos + font.GetHeight(e.Graphics) + 5;
                font = new Font("宋体", 10);
                str = "　　　日期";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin;
                yPos = yPos + font.GetHeight(e.Graphics) + 5;
                font = new Font("黑体", 12);
                str = "---------------------------------";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = leftMargin;
                yPos = yPos + font.GetHeight(e.Graphics) + 5;
                font = new Font("华文行楷", 12);
                str = "此凭单为购物凭证，请妥善保管！";
                e.Graphics.DrawString(str, font, Brushes.Black, xPos, yPos, new StringFormat());
            }

            #region 一卡通充值单
            //e.Graphics.DrawString("衡水东明村集团（正联）", font, bru, 25, 20);
            //e.Graphics.DrawString("储值卡充值单", font, bru, 50, 50);
            //e.Graphics.DrawString("金额：1000.00", font, bru, 10, 80);
            //e.Graphics.DrawString("流水号：8131164706378281250", font, bru, 10, 100);
            //e.Graphics.DrawString("时间：2016-08-24 08:03:57", font, bru, 10, 120);
            //e.Graphics.DrawString("卡号：1410002938", font, bru, 10, 140);
            //e.Graphics.DrawString("收费员：时玲玲", font, bru, 10, 160);
            //e.Graphics.DrawString("交款人签字：", font, bru, 10, 180);

            //e.Graphics.DrawString("衡水东明村集团（副联）", font, bru, 25, 210);
            //e.Graphics.DrawString("储值卡充值单", font, bru, 50, 240);
            //e.Graphics.DrawString("金额：1000.00", font, bru, 10, 270);
            //e.Graphics.DrawString("流水号：8131164706378281250", font, bru, 10, 290);
            //e.Graphics.DrawString("时间：2016-08-24 08:03:57", font, bru, 10, 310);
            //e.Graphics.DrawString("卡号：1410002938", font, bru, 10, 330);
            //e.Graphics.DrawString("收费员：时玲玲", font, bru, 10, 350);
            //e.Graphics.DrawString("交款人签字：", font, bru, 10, 370);
            #endregion

            #region 一卡通退卡单
            //e.Graphics.DrawString("衡水东明村集团（正联）", font, bru, 25, 20);
            //e.Graphics.DrawString("销卡结算单", font, bru, 50, 50);
            //e.Graphics.DrawString("卡号：1410002938", font, bru, 10, 80);
            //e.Graphics.DrawString("卡持有人：司亮", font, bru, 10, 100);
            //e.Graphics.DrawString("销卡金额：330.00", font, bru, 10, 120);
            //e.Graphics.DrawString("时间：2016-08-24 08:03:57", font, bru, 10, 140);
            //e.Graphics.DrawString("操作员：张如涛", font, bru, 10, 160);
            //e.Graphics.DrawString("领款人签字：", font, bru, 10, 180);

            //e.Graphics.DrawString("衡水东明村集团（副联）", font, bru, 25, 210);
            //e.Graphics.DrawString("销卡结算单", font, bru, 50, 240);
            //e.Graphics.DrawString("卡号：1410002938", font, bru, 10, 270);
            //e.Graphics.DrawString("卡持有人：司亮", font, bru, 10, 290);
            //e.Graphics.DrawString("销卡金额：330.00", font, bru, 10, 310);
            //e.Graphics.DrawString("时间：2016-08-24 08:03:57", font, bru, 10, 330);
            //e.Graphics.DrawString("操作员：张如涛", font, bru, 10, 350);
            //e.Graphics.DrawString("领款人签字：", font, bru, 10, 370);
            #endregion

            #region 充值操作员交接单

            //e.Graphics.DrawString("衡水东明村集团", font, bru, 45, 20);
            //e.Graphics.DrawString("一卡通交接单", font, bru, 48, 40);
            //e.Graphics.DrawString("收费单位：菜市场", font, bru, 10, 70);
            //e.Graphics.DrawString("操作员：张如涛", font, bru, 22, 90);
            //e.Graphics.DrawString("时间自：2016-08-24 08:59:48", font, bru, 22, 110);
            //e.Graphics.DrawString("至：2016-08-24 16:48:12", font, bru, 44, 130);
            //e.Graphics.DrawString("充值笔数：17", font, bru, 10, 150);
            //e.Graphics.DrawString("充值总额：5500.00", font, bru, 10, 170);
            //e.Graphics.DrawString("销卡笔数：0", font, bru, 10, 190);
            //e.Graphics.DrawString("销卡总额：0.00", font, bru, 10, 210);
            //e.Graphics.DrawString("本班次余额：5500.00", font, bru, 10, 230);
            //e.Graphics.DrawString("      ", font, bru, 10, 250);
            #endregion

            #region 消费操作员交接单

            //e.Graphics.DrawString("衡水东明村集团", font, bru, 45, 20);
            //e.Graphics.DrawString("交接单", font, bru, 60, 40);
            //e.Graphics.DrawString("收费单位：股民服务部", font, bru, 10, 70);
            //e.Graphics.DrawString("收费员：张如涛", font, bru, 22, 90);
            //e.Graphics.DrawString("时间自：2016-08-24 08:59:48", font, bru, 22, 110);
            //e.Graphics.DrawString("至：2016-08-24 16:48:12", font, bru, 44, 130);
            //e.Graphics.DrawString("交易笔数：1", font, bru, 10, 150);
            //e.Graphics.DrawString("收费金额：10.00", font, bru, 10, 170);
            //e.Graphics.DrawString("现金：0", font, bru, 32, 190);
            //e.Graphics.DrawString("储值卡：10.00", font, bru, 22, 210);
            //e.Graphics.DrawString("      ", font, bru, 10, 250);

            #endregion

        }

        /// <summary>
        /// 余额打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            Type = "3";
            if (string.IsNullOrWhiteSpace(txtYueOrder.Text))
            {
                MessageBox.Show("请先交费", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                    printDocument1.Print();
            }
        }

        /// <summary>
        /// 电卡充值打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDianOrder.Text))
            {
                MessageBox.Show("请先交费", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            BatteryTemp f = new BatteryTemp();
            f.Month = DateTime.Now.ToString("MM") + " 月";
            f.OrderId = txtDianOrder.Text;
            f.xm = txtxm.Text;
            f.MerName = txtmerName.Text;
            f.cs = txtbuyDiancount.Text + " 次";
            f.gsl = txtDianTotal.Text;
            f.zb = "0";
            f.dj = txtDianPrice.Text;
            f.je = nudDianBuyMoney.Value.ToString();
            f.Owner = this;
            f.ShowDialog();
            //Type = "2";
            //if (string.IsNullOrWhiteSpace(txtDianOrder.Text))
            //{
            //    MessageBox.Show("请先交费", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //ShouJuReport f = new ShouJuReport();
            //f.MerUserName = txtxm.Text;
            //f.MerName = txtmerName.Text;
            //f.Money = nudDianBuyMoney.Value.ToString();
            //f.OrderId = txtDianOrder.Text;
            //f.Remark = "      项目                      说明                        金额 \r\n "
            //         + " 预购卡电费           单价: " + txtDianPrice.Text + "数量: " + txtDianTotal.Text + "                  ￥" + nudDianBuyMoney.Value.ToString() + " ";
            //f.Owner = this;
            //f.ShowDialog();
        }
        /// <summary>
        /// 水卡打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWaterOrder.Text))
            {
                MessageBox.Show("请先交费", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            WaterTemp f = new WaterTemp();
            f.Month = DateTime.Now.ToString("MM") + " 月";
            f.OrderId = txtWaterOrder.Text;
            f.xm = txtxm.Text;
            f.MerName = txtmerName.Text;
            f.cs = txtbuyWatercount.Text+" 次";
            f.gsl = txtWaterTotal.Text;
            f.zb = "0";
            f.dj = txtWaterPrice.Text;
            f.je = nudWaterBuyMoney.Value.ToString();
            f.Owner = this;
            f.ShowDialog();


            //ShouJuReport f = new ShouJuReport();
            //f.MerUserName = txtxm.Text;
            //f.MerName = txtmerName.Text;
            //f.Money = nudWaterBuyMoney.Value.ToString();
            //f.OrderId = txtWaterOrder.Text;
            //f.Remark = "      项目                      说明                        金额 \r\n "
            //         + " 预购卡水费           单价: " + txtWaterPrice.Text + "数量: " + txtWaterTotal.Text + "                  ￥" + nudWaterBuyMoney.Value.ToString() + " ";
            //f.Owner = this;
            //f.ShowDialog();



        //    Type = "1";

        //    //if (printDialog1.ShowDialog() == DialogResult.OK)
        //    //{
        //    //    printDocument1.Print();
        //    //}
      
        //   // printDocument1.Print();
        //    PrintDialog MySettings = new PrintDialog();
        //    MySettings.Document = this.printDocument1;  
        //    //PrintDocument pd = new PrintDocument();
        //    PaperSize p = null;
        //    foreach (PaperSize ps in printDocument1.PrinterSettings.PaperSizes)
        //    {
        //        if (ps.PaperName.Equals("A4"))
        //            p = ps;
        //    }
        //    printDocument1.DefaultPageSettings.PaperSize = p;
        //    if (MySettings.ShowDialog() == DialogResult.OK)
        //    {
        //       printDocument1.Print();

        //    }
            
        }

        private void nudyue_ValueChanged(object sender, EventArgs e)
        {
            nudyue.Value = (int)nudyue.Value;
        }

        private void nudDianBuyMoney_ValueChanged(object sender, EventArgs e)
        {
            nudDianBuyMoney.Value = (int)nudDianBuyMoney.Value;
        }

        private void nudWaterBuyMoney_ValueChanged(object sender, EventArgs e)
        {
            nudWaterBuyMoney.Value = (int)nudWaterBuyMoney.Value;
        }

    }
}
