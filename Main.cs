using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using XK.NBear.DB;
using dmsk;

namespace UserManager
{
    public partial class Main : Form
    {
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
      //  common com = new common();
        public Main()
        {
            InitializeComponent();

            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            // skinEngine1.SkinFile = AppDomain.CurrentDomain.BaseDirectory + "Warm.ssk";
            this.WindowState = FormWindowState.Maximized;
            //string imagepath = Application.StartupPath + @"/Images/mainbg.jpg";
            //foreach (Control ctr in this.Controls)
            //{
            //    if (ctr.GetType().ToString().Equals("System.Windows.Forms.MdiClient") && (File.Exists(imagepath)))
            //    {
            //        Image img = new Bitmap(imagepath);
            //        Bitmap bmp = new Bitmap(img, this.Width, this.Height);
            //        ((MdiClient)ctr).BackgroundImage = bmp;
            //    }
            //}


            //    Image img = new Bitmap(imagepath);
            //    Bitmap bmp = new Bitmap(img, this.Width, this.Height);
            // panelEnhanced1.BackgroundImage = bmp;
        }

        #region 公用方法

        /// <summary>  
        /// 在MDI父窗体中打开子窗体，判断是否已经重复打开  
        /// 如果已经打开，则激活这个子窗体，否则返回false值。  
        /// </summary>  
        /// <param name="p_ChildrenFormText"></param>  
        /// <returns></returns>  
        private bool showChildrenForm(string p_ChildrenFormName)
        {
            bool state = false;
            int i;
            //依次检测当前窗体的子窗体  
            for (i = 0; i < this.MdiChildren.Length; i++)
            {
                //判断当前子窗体的name属性值是否与传入的字符串值相同  
                if (this.MdiChildren[i].Name == p_ChildrenFormName)
                {
                    //此子窗体是目标子窗体，激活之  
                    this.MdiChildren[i].Activate();
                    state = true;
                    break;
                }
            }
            return state;
        }

        #endregion

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;////用双缓冲从下到上绘制窗口的所有子孙
        //        return cp;
        //    }
        //}

        #region 窗体事件

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            if (Common.userName == "admin")
            {
                用户管理ToolStripMenuItem.Visible = true;
                系统设置ToolStripMenuItem.Visible = true;
            }
            toolStripStatusLabel1.Text = Common.LoginTime;
            toolStripStatusLabel4.Text = Common.realName;
            toolStripStatusLabel6.Text = Common.Company;
       
   
        }
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0014) // 禁掉清除背景消息
        //        return;
        //    base.WndProc(ref m);
        //}

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res= MessageBox.Show("是否需要打印交接单? 点击\"否\"则退出系统", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (DialogResult.Yes == res)
            {
                e.Cancel = true;
                JiaoJieBan y = new JiaoJieBan();
                y.Owner = this;
                y.ShowDialog();
            }
            else if (DialogResult.Cancel == res)
            {
                e.Cancel = true;
            }
            else
            {
                Common.WriteOpearateLog(Common.UserId, Common.realName + "于" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "退出系统");
                DataTable temp = db.ExecuteDataTable("select top 1 *from  dbo.RecordWork where userid='" + Common.UserId + "' order by LoginTime desc");
                if (temp != null && temp.Rows.Count != 0)
                {
                    if (!string.IsNullOrWhiteSpace(temp.Rows[0]["UnLoginTime"].ToString()))
                        db.NonQuery(" update RecordWork set UnLoginTime='" + DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss") + "' where id='" + temp.Rows[0]["id"].ToString() + "' ");
                }
                Application.ExitThread();
            }
        }

        #endregion

        #region 菜单栏事件

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("YongHuGuanLi"))
            {
                YongHuGuanLi y = new YongHuGuanLi();
                y.Owner = this;
                y.Show();
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("Password"))
            {
                Password y = new Password();
                y.Owner = this;
                y.Show();
            }
        }

        /// <summary>
        /// 水价格设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 系统参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("WaterPrice"))
            {
                WaterPrice y = new WaterPrice();
                y.Owner = this;
                y.Show();
            }
        }

        /// <summary>
        /// 电价格设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 电价格设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!showChildrenForm("DianPrice"))
            {
                DianPrice y = new DianPrice();
                y.Owner = this;
                y.Show();
            }
        }

        /// <summary>
        /// 字典类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 字典类别ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("ZiDianLeiBie"))
            {
                ZiDianLeiBie y = new ZiDianLeiBie();
                y.Owner = this;
                y.Show();
            }
        }
        /// <summary>
        /// 字典项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 字典项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("ZiDianXiangMu"))
            {
                ZiDianXiangMu y = new ZiDianXiangMu();
                y.Owner = this;
                y.Show();
            }
        }

        /// <summary>
        /// 退出菜单栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 挂失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 电卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!showChildrenForm("UserInfoLost"))
            //{
            //    UserInfoLost y = new UserInfoLost();
            //    y.MdiParent = this;
            //    y.Show();
            //}
            UserInfoLost y = new UserInfoLost();
            y.Owner = this;
            y.ShowDialog();
        }

        /// <summary>
        /// 解挂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 水卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!showChildrenForm("UserInfoUnLost"))
            //{
            //    UserInfoUnLost y = new UserInfoUnLost();
            //    y.MdiParent = this;
            //    y.Show();
            //}
            UserInfoUnLost y = new UserInfoUnLost();
            y.Owner = this;
            y.ShowDialog();
        }

        /// <summary>
        /// 补卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 车费ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!showChildrenForm("UserInfoChangeCard"))
            //{
            //    UserInfoChangeCard y = new UserInfoChangeCard();
            //    y.MdiParent = this;
            //    y.Show();
            //}
            UserInfoChangeCard y = new UserInfoChangeCard();
            y.Owner = this;
            y.ShowDialog();
        }

        //销户
        private void 房租ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!showChildrenForm("UserInfoClosing"))
            //{
            //    UserInfoClosing y = new UserInfoClosing();
            //    y.MdiParent = this;
            //    y.Show();
            //}
            UserInfoClosing y = new UserInfoClosing();
            y.Owner = this;
            y.ShowDialog();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 余额ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!showChildrenForm("UsersInfo"))
            //{
            //    UsersInfo y = new UsersInfo();
            //    y.MdiParent = this;
            //    y.Show();
            //}
            UsersInfo y = new UsersInfo();
            y.Owner = this;
            y.ShowDialog();
            //UsersInfo y = new UsersInfo();
            //y.Show();
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 充值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!showChildrenForm("Recharge"))
            //{
            //    Recharge y = new Recharge();
            //    y.MdiParent = this;
            //    y.Show();
            //}
            Recharge y = new Recharge();
            y.Owner = this;
            y.ShowDialog();
        }

        #endregion

        #region 工具栏按钮事件

        /// <summary>
        /// 开户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //if (!showChildrenForm("Info"))
            //{
            ////    panelEnhanced1.Controls.Clear();
            //    Info y = new Info();
            //    y.MdiParent = this;
            // //  y.Parent = panelEnhanced1;
            //    //  Bitmap bmp = new Bitmap(16, 16);
            //    //   bmp.MakeTransparent();
            //    //   y.Icon = Icon.FromHandle(bmp.GetHicon());
            //   // y.WindowState = FormWindowState.Maximized;
            //    y.Show();
            //}
            Info y = new Info();
            y.Owner = this;
            y.ShowDialog();
        }

        /// <summary>
        /// 挂失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //if (!showChildrenForm("UserInfoLost"))
            //{
            //    UserInfoLost y = new UserInfoLost();
            //    y.MdiParent = this;
            //    y.Show();
            //}
            UserInfoLost y = new UserInfoLost();
            y.Owner = this;
            y.ShowDialog();
        }

        /// <summary>
        /// 解挂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            //if (!showChildrenForm("UserInfoUnLost"))
            //{
            //    UserInfoUnLost y = new UserInfoUnLost();
            //    y.MdiParent = this;
            //    y.Show();
            //}
            UserInfoUnLost y = new UserInfoUnLost();
            y.Owner = this;
            y.ShowDialog();
        }

        /// <summary>
        /// 补卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //if (!showChildrenForm("UserInfoChangeCard"))
            //{
            //    UserInfoChangeCard y = new UserInfoChangeCard();
            //    y.MdiParent = this;
            //    y.Show();
            //}
            UserInfoChangeCard y = new UserInfoChangeCard();
            y.Owner = this;
            y.ShowDialog();
        }

        /// <summary>
        /// 销户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //if (!showChildrenForm("UserInfoClosing"))
            //{
            //    UserInfoClosing y = new UserInfoClosing();
            //    y.MdiParent = this;
            //    y.Show();
            //}
            UserInfoClosing y = new UserInfoClosing();
            y.Owner = this;
            y.ShowDialog();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            //if (!showChildrenForm("UsersInfo"))
            //{
            //    UsersInfo y = new UsersInfo();
            //    y.MdiParent = this;
            //    y.Show();
            //}
            UsersInfo y = new UsersInfo();
            y.Owner = this;
            y.ShowDialog();
            //UsersInfo y = new UsersInfo();
            //y.Show();
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            //if (!showChildrenForm("Recharge"))
            //{
            //    Recharge y = new Recharge();
            //    y.MdiParent = this;
            //    y.Show();
            //}
            Recharge y = new Recharge();
            y.Owner = this;
            y.ShowDialog();
        }

        /// <summary>
        /// 退出图标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion


        /// <summary>
        /// 缴费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            JiaoFei y = new JiaoFei();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 当日卡统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ReportCard y = new ReportCard();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 历次卡统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportHistoryCard y = new ReportHistoryCard();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 当日流水统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportWater y = new ReportWater();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 历史流水统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportHistoryWater y = new ReportHistoryWater();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 缴费记录查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QueryJiaoFei y = new QueryJiaoFei();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 卡详细查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QueryCard y = new QueryCard();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 流水详细查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QueryWater y = new QueryWater();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 商户查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QueryMerInfo y = new QueryMerInfo();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 水电稽查ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuditFee y = new AuditFee();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 交接班ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JiaoJieBan y = new JiaoJieBan();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 交接班记录查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QueryRecordWork Y = new QueryRecordWork();
            Y.Owner = this;
            Y.ShowDialog();
        }

        private void 收费单位统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportCompany Y = new ReportCompany();
            Y.Owner = this;
            Y.ShowDialog();
        }

        /// <summary>
        /// 商户卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 商户卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Info y = new Info();
            y.Owner = this;
            y.ShowDialog();
        }

        /// <summary>
        /// 市场卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 市场卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShiChangKaKaiHu y = new ShiChangKaKaiHu();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 营业售水电月报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportPayWater y = new ReportPayWater();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 营业售电月报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReprotPayDian y = new ReprotPayDian();
            y.Owner = this;
            y.ShowDialog();
        }

        private void 记账凭证ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportJiZhang y = new ReportJiZhang();
            y.Owner = this;
            y.ShowDialog();
        }

        ///// <summary>
        ///// OnPaintBackground 事件
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    // 重载基类的背景擦除函数，
        //    // 解决窗口刷新，放大，图像闪烁
        //    return;
        //}

        ///// <summary>
        ///// OnPaint 事件
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    // 使用双缓冲
        //    this.DoubleBuffered = true;
        //    // 背景重绘移动到此
        //    if (this.BackgroundImage != null)
        //    {
        //        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //        e.Graphics.DrawImage(
        //            this.BackgroundImage,
        //            new System.Drawing.Rectangle(0, 0, this.Width, this.Height),
        //            0,
        //            0,
        //            this.BackgroundImage.Width,
        //            this.BackgroundImage.Height,
        //            System.Drawing.GraphicsUnit.Pixel);
        //    }
        //    base.OnPaint(e);
        //}







    }
}
