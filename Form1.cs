using dmsk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace UserManager
{
    public partial class Form1 : Form
    {

        common cm = new common();


        /// <summary>
        /// 初始化读卡器
        /// </summary>
        /// <returns>返回>0正确</returns>
        [DllImport("ICCard.dll")]
        public static extern int InitCard();

        /// <summary>
        /// 关闭读卡器
        /// </summary>
        /// <param name="nDev">设备号</param>
        /// <returns>小于0 失败 ; </returns>
        [DllImport("ICCard.dll")]
        public static extern int ExitCard(int nDev);


        /// <summary>
        /// 擦除卡片
        /// </summary>
        /// <returns>返回=0正确,返回小于0错误</returns>
        [DllImport("ICCard.dll")]
        public static extern short ClearCard(int nDev);



        /// <summary>
        /// 售电
        /// </summary>
        /// <param name="nDev">设备id</param>
        /// <param name="nVol">售电度数</param>
        /// <returns>返回=0正确,返回小于0错误</returns>
        [DllImport("ICCard.dll")]
        public static extern short AddVol(int nDev, uint nVol);


        /// <summary>
        /// 预置卡
        /// </summary>
        /// <param name="nDev">设备id</param>
        /// <param name="nPostCode">邮编(系统注册码)</param>
        /// <param name="nAdmin">管理员编号</param>
        /// <param name="nWarnVol">报警电量</param>
        /// <param name="nLimit">囤积电量</param>
        /// <param name="nVol">预置电量</param>
        /// <returns>返回=0正确,返回小于0错误</returns>
        [DllImport("ICCard.dll")]
        public static extern short MakePresetCard(int nDev, uint nPostCode, uint nAdmin, uint nWarnVol, uint nLimit, uint nVol);




        /// <summary>
        /// 查电卡
        /// </summary>
        /// <param name="nDev">设备id</param>
        /// <param name="nPostCode">邮编(系统注册码)</param>
        /// <param name="nAdmin">管理员编号</param>
        /// <returns>返回=0正确,返回小于0错误</returns>
        [DllImport("ICCard.dll")]
        public static extern short MakeQueryCard(int nDev, uint nPostCode, uint nAdmin);


        /// <summary>
        /// 蜂鸣
        /// </summary>
        /// <param name="nDev">设备id</param>
        /// <param name="nPostCode">邮编(系统注册码)</param>
        /// <param name="nAdmin">管理员编号</param>
        /// <returns>返回=0正确,返回小于0错误</returns>
        [DllImport("Mwic_32.dll")]
        public static extern short dv_beep(int nDev, int time);





        /// <summary>
        /// 开户卡
        /// </summary>
        /// <param name="nDev">设备id</param>
        /// <param name="nMeterID">电表编号</param>
        /// <param name="nVol">电量</param>
        /// <param name="nBuyCount">购电次数</param>
        /// <param name="nWarnVol">报警电量</param>
        /// <param name="nLimit">囤积电量</param>
        /// <param name="nDuty">赊欠限量</param>
        /// <param name="nCardType">卡类型03 补卡 01 新开 02 正常</param>
        /// <param name="nUserID">用户编号</param>
        /// <param name="nPostCode">邮编(系统注册码)</param>
        /// <param name="nAdmin">管理员编号</param>
        /// <returns>返回=0正确,返回小于0错误</returns>
        [DllImport("ICCard.dll")]
        public static extern short MakeUserCard(int nDev, uint nMeterID, uint nVol, uint nBuyCount, uint nWarnVol, uint nLimit, uint nDuty, uint nCardType, uint nUserID, uint nPostCode, uint nAdmin);





        /// <summary>
        /// 格式化白卡
        /// <param name="nDev">设备id</param>
        /// <param name="nPass">密码</param>
        /// </summary>
        [DllImport("ICCard.dll")]
        public static extern short FormatCard(int nDev, uint nPass);




        /// <summary>
        /// 读取用户卡
        /// <param name="nDev">设备id</param>
        /// </summary>
        [DllImport("ICCard.dll")]
        public static extern short ReadUserCard(int nDev, out usercard pCard);



        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化卡片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int r = InitCard();
                if (r > 0)
                {
                   
                    MessageBox.Show("初始化成功,返回值为: " + r);
                }
                else
                {
                    MessageBox.Show("初始化失败,返回值为:"+ r);
                }
            }
            catch (Exception xx)
            {

                MessageBox.Show(xx.Message);
            }
        }

        /// <summary>
        /// 售电
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int r = InitCard();
                if (r > 0)
                {
                    short k = AddVol(r, 200);
                    if (k == 0)
                    {
                        MessageBox.Show("售电成功");
                    }
                    else
                    {
                        MessageBox.Show("售电失败");
                    }
                }
                else
                {
                    MessageBox.Show("初始化设备失败");
                }
            }
            catch (Exception xx)
            {

                MessageBox.Show("异常信息 : "+ xx.Message);
            }

           
        }

        /// <summary>
        /// 开户卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int r = InitCard();
                if (r > 0)
                {
                    short k = MakeUserCard(r,Convert.ToUInt32("123456"), Convert.ToUInt32( "20"), 1, 5, 9999, 0, 01, 001211, 111111, 111111);
                    if(k==0)
                    {
                        MessageBox.Show("开户成功");
                    }
                    else
                    {
                        MessageBox.Show("开户失败");
                    }
                }
                else
                {
                    MessageBox.Show("初始化设备失败");
                }
            }
            catch (Exception xx)
            {

                MessageBox.Show("异常信息 : " + xx.Message);
            }
        }

        /// <summary>
        /// 格式化白卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                int r = InitCard();
                if (r > 0)
                {
                    short k = FormatCard(r, Convert.ToUInt32("ffffff",16));
                    if (k == 0)
                    {
                        MessageBox.Show("格式化白卡成功");
                    }
                    else
                    {
                        MessageBox.Show("格式化白卡失败");
                    }
                }
                else
                {
                    MessageBox.Show("初始化设备失败");
                }
            }
            catch (Exception xx)
            {

                MessageBox.Show("异常信息 : " + xx.Message);
            }
        }

        /// <summary>
        /// 预置卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int r = InitCard();
                if (r > 0)
                {
                   //内容
                    short k = MakePresetCard(r, 111111, 111111, 5, 0, 10);
                    if(k==0)
                    {
                        MessageBox.Show("预置卡制作成功");
                    }
                    else
                    {
                        MessageBox.Show("预置卡失败");
                    }
                }
                else
                {
                    MessageBox.Show("初始化设备失败");
                }
            }
            catch (Exception xx)
            {

                MessageBox.Show("异常信息 : " + xx.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int r = InitCard();
                if (r > 0)
                {
                    //内容
                    short k = dv_beep(r,10);
                    if(k!=0)
                    {
                        MessageBox.Show("蜂鸣失败");
                    }
                }
                else
                {
                    MessageBox.Show("初始化设备失败");
                }
            }
            catch (Exception xx)
            {

                MessageBox.Show("异常信息 : " + xx.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int r = InitCard();
                string dbid = "";
                if (r > 0)
                {
                    //内容
                 usercard cc = new usercard();
                 short k = ReadUserCard(r, out cc);
                    if(k== 0)
                    {
                        string nr = string.Format("电表id:{0} -- 购电d数:{1} -- 购电次数:{2} -- 报警:{3} -- 囤积限量:{4} -- 赊欠:{5} -- 限容:{6} -- 用户ID:{7} -- 邮编:{8} -- 管理员编号:{9}",cc.MeterID,cc.Vol,cc.BuyCount,cc.WarnVol,cc.Limit,cc.Duty,cc.LimitVol[0],cc.UserID,cc.PostCode,cc.Admin);
                        MessageBox.Show(nr);
                    }
                    else
                    {
                        MessageBox.Show("读用户卡失败");
                    }

                }
                else
                {
                    MessageBox.Show("初始化设备失败");
                }
            }
            catch (Exception xx)
            {

                MessageBox.Show("异常信息 : " + xx.Message);
            }
        }

        /// <summary>
        /// 擦除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            int r= InitCard();
            if(r>0)
            {
                short j = ClearCard(r);
                if(j != 0)
                {
                    MessageBox.Show("擦除失败");
                }
                else
                {
                    MessageBox.Show("擦除成功");
                }

                int g = ExitCard(r);
            }
            else
            {
                MessageBox.Show("打开读卡器失败");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string error = "";
           int r = cm.dian_buka("5", "0", "1", "20", "9999", "0", "5", "1230", "1", out error);
            if(r == 0)
            {
                MessageBox.Show("补卡成功");
            }
            else
            {
                MessageBox.Show(error);
            }
        }

        /// <summary>
        /// 水卡开户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            //sk f = new sk();
            //f.ShowDialog();
        }

        /// <summary>
        /// 关闭读卡器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 水卡初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
          int t=jk.Open_USB();
          jk.Close_USB(t);
          MessageBox.Show(t.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                int r = InitCard();
                if (r > 0)
                {

                    MessageBox.Show("初始化成功,返回值为: " + r);
                }
                else
                {
                    MessageBox.Show("初始化失败,返回值为:" + r);
                }
            }
            catch (Exception xx)
            {

                MessageBox.Show(xx.Message);
            }
        }
    }
}
