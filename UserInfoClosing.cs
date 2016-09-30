using dmsk;
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
    public partial class UserInfoClosing : Form
    {
        public UserInfoClosing()
        {
            InitializeComponent();
            this.printDocument1.OriginAtMargins = false;//启用页边距
            this.pageSetupDialog1.EnableMetric = true; //以毫米为单位
        }
        common com = new common();
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        /// <summary>
        /// 水卡作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //string MerNum = txtMerNum.Text.Trim();
            string one = (cbbone.SelectedValue ?? "").ToString();
            string two = (cbbtwo.SelectedValue ?? "").ToString();
            string three = (cbbthree.SelectedValue ?? "").ToString();
            string four = (cbbfour.Text ?? "").ToString();
            string msg = "";
            if (string.IsNullOrWhiteSpace(one))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(two))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(three))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (string.IsNullOrWhiteSpace(four))
            //{
            //    MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            if (string.IsNullOrWhiteSpace(four))
            {
                MerNum = "";
            }
            string where = "";
            string phone = txtqphone.Text;
            string sfz = txtqsfz.Text;

            if (!string.IsNullOrWhiteSpace(four))
            {
                where += " and  MerNum='" + MerNum + "' ";
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                where += " and  PhoneNum='" + phone + "' ";
            }
            if (!string.IsNullOrWhiteSpace(sfz))
            {
                where += " and  ZJHM='" + sfz + "' ";
            }
            if (string.IsNullOrWhiteSpace(where))
            {
                MessageBox.Show("请填写最少一种查询条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dt = db.ExecuteDataTable("select ShuiCardNum,ID from MerAllInfo_View where 1=1 " + where + "");

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("此商户还没开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dt.Rows.Count != 1)
            {
                MessageBox.Show("数据异常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            string Cardid = dt.Rows[0]["ShuiCardNum"].ToString();//(db.Scalar(" select ShuiCardNum from  MerInfo where MerNum='" + MerNum + "'") ?? "0").ToString();
            if (Cardid != "0")
            {
                string cardid2 = com.get_UID(out msg);
                if (cardid2 == "-1")
                {
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Cardid != cardid2)
                {
                    MessageBox.Show("此卡不是该商户的卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            button2.Enabled = false;
            int flag = com.zuofei(out msg);
            //if (flag != 0)
            //{
            //    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //  db.NonQuery(" delete from MerInfo where MerNum='" + MerNum + "'");
            //  db.NonQuery(" update MerInfo set isCancel=0,ShuiCardNum='',DianCardNum='' where MerNum='" + MerNum + "'");
            db.NonQuery(" update MerInfo set isCancel=0,CancelTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ID='" + dt.Rows[0]["ID"].ToString() + "'");
            db.NonQuery("insert into dbo.MerInfoHistory ([MerUserID],[ShuiCardNum],[MerNum],[DianCardNum],[CarNianFei],[CarStart],[CarEnd],"
               + " [Fangwushiyong],[Wuyefuwu],[Zhianguanli],[Zhandibuchang],[Shichangguanli],[FangZu],[FangZuStart],[FangZuEnd],[Yue],[isCancel],"
               + " [isShuiCardLost],[SBBH],[ZGL],[BJL],[TZL],[JFCS],[CarNum] ,[Credate],[GouDianNum] ,[OpenTime] ,CancelTime)"
               + " select [MerUserID],[ShuiCardNum],[MerNum],[DianCardNum],[CarNianFei],[CarStart],[CarEnd],"
               + " [Fangwushiyong],[Wuyefuwu],[Zhianguanli],[Zhandibuchang],[Shichangguanli],[FangZu],[FangZuStart],[FangZuEnd],[Yue],[isCancel],"
               + " [isShuiCardLost],[SBBH],[ZGL],[BJL],[TZL],[JFCS],[CarNum] ,[Credate],[GouDianNum] ,[OpenTime] ,CancelTime from MerInfo where ID='" + dt.Rows[0]["ID"].ToString() + "' ");
            db.NonQuery(" delete from MerInfo where ID='" + dt.Rows[0]["ID"].ToString() + "' ");
            string MerName = "";
            if (!string.IsNullOrWhiteSpace(MerNum))
            {
                MerName = GetMerName(MerNum);
            }
            db.NonQuery("insert into U_AuditLog(OpeType,UserID,MerNum,CarType,CardNum,MerName) values('销户','" + Common.UserId + "','" + MerNum + "',0,'" + Cardid + "','" + MerName + "')");
            string TradeDate = DateTime.Now.ToString("yyyyMMdd");
            string TradeTime = DateTime.Now.ToString("HH:mm");
            string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string sql1 = "insert into S_Water(MerNum,RetCode,TermID,ZJRXM,ZJHM,TradeDate,TradeTime,LiquiDate,cardid,TransMoney,TradeType,IsCash,CreateDate)  values ('" + MerNum + "','00','U_" + Common.UserId + "','" + txtUserName.Text + "','" + txtsfz.Text + "','" + TradeDate + "','" + TradeTime + "','" + TradeDate + "','','" + decimal.Parse(txtAccountBalance.Text.ToString())*100 + "',8,1,'"+createtime+"')";
            db.NonQuery(sql1);
            Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtUserName.Text + "销户" + txtAccountBalance.Text + "元");
            MessageBox.Show("销户成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button2.Enabled = true;
            // }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //   string MerNum = txtMerNum.Text.Trim();
            string one = (cbbone.SelectedValue ?? "").ToString();
            string two = (cbbtwo.SelectedValue ?? "").ToString();
            string three = (cbbthree.SelectedValue ?? "").ToString();
            string four = (cbbfour.Text ?? "").ToString();
            if (string.IsNullOrWhiteSpace(one))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(two))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(three))
            {
                MessageBox.Show("无可用数据，请联系管理员添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (string.IsNullOrWhiteSpace(four))
            //{
            //    MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
                string where = "";
                string phone = txtqphone.Text;
                string sfz = txtqsfz.Text;

                if (!string.IsNullOrWhiteSpace(four))
                {
                    where += " and  MerNum='" + MerNum + "' ";
                }
                if (!string.IsNullOrWhiteSpace(phone))
                {
                    where += " and  PhoneNum='" + phone + "' ";
                }
                if (!string.IsNullOrWhiteSpace(sfz))
                {
                    where += " and  ZJHM='" + sfz + "' ";
            }
            DataTable dt = db.ExecuteDataTable("select * from MerAllInfo_View where 1=1 " + where + "");
            if (string.IsNullOrWhiteSpace(where))
            {
                MessageBox.Show("请填写最少一种查询条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dt != null && dt.Rows.Count >= 2)
            {
                MessageBox.Show("读取数据异常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dt != null && dt.Rows.Count == 0)
            {
                MessageBox.Show("无此商户信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dt != null && dt.Rows.Count == 1)
            {
                txtUserName.Text = dt.Rows[0]["ZJRXM"].ToString();
                txtsfz.Text = dt.Rows[0]["ZJHM"].ToString();
                txtphone.Text = dt.Rows[0]["PhoneNum"].ToString();
                txtAccountBalance.Text = dt.Rows[0]["Yue"].ToString();
                txtCardid.Text = dt.Rows[0]["ShuiCardNum"].ToString();
                button2.Enabled = true;
            }
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserInfoClosing_Load(object sender, EventArgs e)
        {
            string sql = "select Name,Value from dbo.Dict_View where TypeName='One' order by cast(seq as int) ";
            cbbone.ValueMember = "Value";
            cbbone.DisplayMember = "Name";
            cbbone.DataSource = db.ExecuteDataTable(sql);
            sql = "select Name,Value from dbo.Dict_View where TypeName='Two' order by cast(seq as int) ";
            cbbtwo.ValueMember = "Value";
            cbbtwo.DisplayMember = "Name";
            cbbtwo.DataSource = db.ExecuteDataTable(sql);
            sql = "select Name,Value from dbo.Dict_View where TypeName='Three' order by cast(seq as int) ";
            cbbthree.ValueMember = "Value";
            cbbthree.DisplayMember = "Name";
            cbbthree.DataSource = db.ExecuteDataTable(sql);
            //sql = "select Name,Value from dbo.Dict_View where TypeName='Four' order by seq ";
            //cbbfour.ValueMember = "Value";
            //cbbfour.DisplayMember = "Name";
            //cbbfour.DataSource = db.ExecuteDataTable(sql);
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

        /// <summary>
        /// 打印销户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font = new Font("宋体", 8);
            Brush bru = Brushes.Black;
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string xm = txtUserName.Text;
            string yue = txtAccountBalance.Text;
            string carid = txtCardid.Text;

            e.Graphics.DrawString("衡水东明村集团（正联）", font, bru, 25, 20);
            e.Graphics.DrawString("销卡结算单", font, bru, 50, 50);
            e.Graphics.DrawString("卡号：" + carid + "", font, bru, 10, 80);
            e.Graphics.DrawString("卡持有人：" + xm + "", font, bru, 10, 100);
            e.Graphics.DrawString("销卡金额：" + yue + "", font, bru, 10, 120);
            e.Graphics.DrawString("时间：" + time + "", font, bru, 10, 140);
            e.Graphics.DrawString("操作员：" + Common.realName + "", font, bru, 10, 160);
            e.Graphics.DrawString("领款人签字：", font, bru, 10, 180);

            e.Graphics.DrawString("衡水东明村集团（副联）", font, bru, 25, 210);
            e.Graphics.DrawString("销卡结算单", font, bru, 50, 240);
            e.Graphics.DrawString("卡号：" + carid + "", font, bru, 10, 270);
            e.Graphics.DrawString("卡持有人：" + xm + "", font, bru, 10, 290);
            e.Graphics.DrawString("销卡金额：" + yue + "", font, bru, 10, 310);
            e.Graphics.DrawString("时间：" + time + "", font, bru, 10, 330);
            e.Graphics.DrawString("操作员：" + Common.realName + " ", font, bru, 10, 350);
            e.Graphics.DrawString("领款人签字：", font, bru, 10, 370);
        }
    }
}
