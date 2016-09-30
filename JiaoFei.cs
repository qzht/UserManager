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

namespace UserManager
{
    public partial class JiaoFei : Form
    {
        public JiaoFei()
        {
            InitializeComponent();
        }
        common com = new common();
        int CardType = -1;//-1默认 0 水卡 1电卡
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");

        #region 按钮事件

        /// <summary>
        /// 房租设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
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
            if (string.IsNullOrWhiteSpace(four))
            {
                MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            DataTable dt = db.ExecuteDataTable("select * from MerAllInfo_View where MerNum='" + MerNum + "'");
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("此商户还没开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblsumpay.Text = GetSum();
            if (lblsumpay.Text == "0")
            {
                MessageBox.Show("充值金额不能为0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string start = dtpstart.Text;
            string end = dtpend.Text;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始日期不能大于结束日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "update MerInfo set Fangwushiyong='" + nudHouse.Value + "', Wuyefuwu='" + nudWuye.Value + "', Zhianguanli='" + nudZhiAn.Value + "', Zhandibuchang='" + nudZhandi.Value + "', Shichangguanli='" + nudShiChang.Value + "', FangZu='" + lblsumpay.Text + "',FangZuStart='" + dtpstart.Text + "', FangZuEnd='" + dtpend.Text + "'  where MerNum='" + MerNum + "'";
            string result = db.NonQuery(sql).ToString();
            MessageBox.Show("设置成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 房租充值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
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
            if (string.IsNullOrWhiteSpace(four))
            {
                MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            DataTable dt = db.ExecuteDataTable("select * from MerAllInfo_View where MerNum='" + MerNum + "'");
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("此商户还没开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lblsumpay.Text = GetSum();
            if (lblsumpay.Text == "0")
            {
                MessageBox.Show("充值金额不能为0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string start = dtpstart.Text;
            string end = dtpend.Text;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始日期不能大于结束日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (DialogResult.Yes == MessageBox.Show("确定是否收到现金?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                button9.Enabled = false;
                string result = (db.Scalar("update MerInfo set  Fangwushiyong='" + nudHouse.Value + "', Wuyefuwu='" + nudWuye.Value + "', Zhianguanli='" + nudZhiAn.Value + "', Zhandibuchang='" + nudZhandi.Value + "', Shichangguanli='" + nudShiChang.Value + "', FangZu='" + lblsumpay.Text + "',FangZuStart='" + dtpstart.Text + "', FangZuEnd='" + dtpend.Text + "'  where MerNum='" + MerNum + "'") ?? "0").ToString();
                string TradeDate = DateTime.Now.ToString("yyyyMMdd");
                string TradeTime = DateTime.Now.ToString("HH:mm");
                string OrderId = Common.GetOrder("FZ");
                txtFzOrder.Text = OrderId;
                string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "insert into S_Water(OrderID,MerNum,RetCode,TermID,ZJRXM,ZJHM,TradeDate,TradeTime,LiquiDate,cardid,TransMoney,TradeType,IsCash,JiaoFeiStart,JiaoFeiEnd,CreateDate)  values ('" + OrderId + "','" + MerNum + "','00','U_" + Common.UserId + "','" + txtUserName.Text + "','" + txtsfz.Text + "','" + TradeDate + "','" + TradeTime + "','" + TradeDate + "','','" + (int.Parse(lblsumpay.Text) * 100).ToString("0") + "',4,1,'" + dtpstart.Text + "','" + dtpend.Text + "','" + createtime + "')";
                db.NonQuery(sql);
                Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtUserName.Text+ "房租" + lblsumpay.Text+ "元");
                MessageBox.Show("缴费成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button9.Enabled = true;
            }

        }

        /// <summary>
        /// 车辆年费设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
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
            if (string.IsNullOrWhiteSpace(four))
            {
                MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            DataTable dt = db.ExecuteDataTable("select * from MerAllInfo_View where MerNum='" + MerNum + "'");
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("此商户还没开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCPHM.Text.Trim()))
            {
                MessageBox.Show("请输入车牌号码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //string[] cphmlist = txtCPHM.Text.Trim().Split('|');
            if (nudCar.Value == 0)
            {
                MessageBox.Show("充值金额不能为0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string start = dtpstart2.Text;
            string end = dtpend2.Text;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始日期不能大于结束日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string cphm = txtCPHM.Text.Trim().ToUpper();
            bool IsExitCard = false;
            string result2 = "0";
            if (cphm.Contains("|"))
            {
                foreach (string o in cphm.Split('|'))
                {
                    if (!string.IsNullOrWhiteSpace(o))
                    {
                        result2 = db.Scalar(" select count(*) from MerInfo where CarNum like '%" + o + "%' ").ToString();
                        if (int.Parse(result2) > 1)
                        {
                            IsExitCard = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                result2 = db.Scalar(" select count(*) from MerInfo where CarNum like '%" + cphm + "%' ").ToString();
                if (int.Parse(result2) > 1)
                {
                    IsExitCard = true;
                }
            }
            if (IsExitCard)
            {
                MessageBox.Show("此车牌已被其它商户绑定", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string result = db.NonQuery("update MerInfo set CarNianFei='" + nudCar.Value + "',CarNum='" + txtCPHM.Text.Trim().ToUpper() + "',CarStart='" + start + "',CarEnd='" + end + "'  where MerNum='" + MerNum + "'").ToString();
            MessageBox.Show("设置成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 车费充值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
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
            if (string.IsNullOrWhiteSpace(four))
            {
                MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            DataTable dt = db.ExecuteDataTable("select * from MerAllInfo_View where MerNum='" + MerNum + "'");
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("此商户还没开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCPHM.Text.Trim()))
            {
                MessageBox.Show("请输入车牌号码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //string[] cphmlist = txtCPHM.Text.Trim().Split('|');
            if (nudCar.Value == 0)
            {
                MessageBox.Show("充值金额不能为0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string start = dtpstart2.Text;
            string end = dtpend2.Text;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始日期不能大于结束日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string cphm = txtCPHM.Text.Trim().ToUpper();
            bool IsExitCard = false;
            string result2 = "0";
            if (cphm.Contains("|"))
            {
                foreach (string o in cphm.Split('|'))
                {
                    if (!string.IsNullOrWhiteSpace(o))
                    {
                        result2 = db.Scalar(" select count(*) from MerInfo where CarNum like '%" + o + "%' ").ToString();
                        if (int.Parse(result2) > 1)
                        {
                            IsExitCard = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                result2 = db.Scalar(" select count(*) from MerInfo where CarNum like '%" + cphm + "%' ").ToString();
                if (int.Parse(result2) > 1)
                {
                    IsExitCard = true;
                }
            }
            if (IsExitCard)
            {
                MessageBox.Show("此车牌已被其它商户绑定", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("确定是否收到现金?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                button3.Enabled = false;
                string result = (db.Scalar("update MerInfo set CarNianFei='" + nudCar.Value + "',CarNum='" + txtCPHM.Text.Trim().ToUpper()+ "',CarStart='" + start + "',CarEnd='" + end + "'   where MerNum='" + MerNum + "'") ?? "0").ToString();
                string TradeDate = DateTime.Now.ToString("yyyyMMdd");
                string TradeTime = DateTime.Now.ToString("HH:mm");
                string OrderId = Common.GetOrder("CL");
                txtCLOrder.Text = OrderId;
                string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "insert into S_Water(OrderID,MerNum,RetCode,TermID,ZJRXM,ZJHM,TradeDate,TradeTime,LiquiDate,cardid,TransMoney,TradeType,IsCash,JiaoFeiStart,JiaoFeiEnd,CreateDate)  values ('" + OrderId + "','" + MerNum + "','00','U_" + Common.UserId + "','" + txtUserName.Text + "','" + txtsfz.Text + "','" + TradeDate + "','" + TradeTime + "','" + TradeDate + "','','" + int.Parse((nudCar.Value * 100).ToString("0")) + "',3,1,'" + start + "','" + end + "','" + createtime + "')";
                db.NonQuery(sql);
                Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtUserName.Text + "车辆号"+txtCPHM.Text+"年费" + nudCar.Value + "元");
                MessageBox.Show("缴费成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button3.Enabled = true;
            }
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

        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
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
            if (string.IsNullOrWhiteSpace(four))
            {
                MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            DataTable dt = db.ExecuteDataTable("select * from MerAllInfo_View where MerNum='" + MerNum + "'");
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
                txtCPHM.Text = dt.Rows[0]["CarNum"].ToString();
                nudCar.Value = decimal.Parse((dt.Rows[0]["CarNianFei"] ?? "0").ToString() == "-1.00" ? "0" : (dt.Rows[0]["CarNianFei"] ?? "0").ToString());
                nudHouse.Value = decimal.Parse((dt.Rows[0]["Fangwushiyong"] ?? "0").ToString() == "-1.00" ? "0" : (dt.Rows[0]["Fangwushiyong"] ?? "0").ToString());
                nudWuye.Value = decimal.Parse((dt.Rows[0]["Wuyefuwu"] ?? "0").ToString() == "-1.00" ? "0" : (dt.Rows[0]["Wuyefuwu"] ?? "0").ToString()); ;
                nudZhiAn.Value = decimal.Parse((dt.Rows[0]["Zhianguanli"] ?? "0").ToString() == "-1.00" ? "0" : (dt.Rows[0]["Zhianguanli"] ?? "0").ToString()); ;
                nudZhandi.Value = decimal.Parse((dt.Rows[0]["Zhandibuchang"] ?? "0").ToString() == "-1.00" ? "0" : (dt.Rows[0]["Zhandibuchang"] ?? "0").ToString()); ;
                nudShiChang.Value = decimal.Parse((dt.Rows[0]["Shichangguanli"] ?? "0").ToString() == "-1.00" ? "0" : (dt.Rows[0]["Shichangguanli"] ?? "0").ToString()); ;
                lblsumpay.Text = (dt.Rows[0]["FangZu"] ?? "0").ToString() == "-1.00" ? "0" : (dt.Rows[0]["FangZu"] ?? "0").ToString();
                if (lblsumpay.Text.IndexOf(".") != -1)
                {
                    lblsumpay.Text = lblsumpay.Text.Substring(0, lblsumpay.Text.IndexOf("."));
                }
                nudad.Value = decimal.Parse((dt.Rows[0]["AdFei"] ?? "0").ToString() == "-1.00" ? "0" : (dt.Rows[0]["AdFei"] ?? "0").ToString()); ;


                nudyj.Value = decimal.Parse((dt.Rows[0]["YaJin"] ?? "0").ToString() == "-1.00" ? "0" : (dt.Rows[0]["YaJin"] ?? "0").ToString()); ;


                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["FangZuStart"].ToString()))
                    dtpstart.Text = DateTime.Parse(dt.Rows[0]["FangZuStart"].ToString()).ToString("yyyy-MM-dd");
                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["FangZuEnd"].ToString()))
                    dtpend.Text = DateTime.Parse(dt.Rows[0]["FangZuEnd"].ToString()).ToString("yyyy-MM-dd");
                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["CarStart"].ToString()))
                    dtpstart2.Text = DateTime.Parse(dt.Rows[0]["CarStart"].ToString()).ToString("yyyy-MM-dd");
                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["CarEnd"].ToString()))
                    dtpend2.Text = DateTime.Parse(dt.Rows[0]["CarEnd"].ToString()).ToString("yyyy-MM-dd");


                txtAdPosition.Text = dt.Rows[0]["AdPosition"].ToString();
                txtAdRemark.Text = dt.Rows[0]["AdRemark"].ToString();

                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["AdStart"].ToString()))
                    dtpstart2.Text = DateTime.Parse(dt.Rows[0]["AdStart"].ToString()).ToString("yyyy-MM-dd");
                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["AdEnd"].ToString()))
                    dtpend2.Text = DateTime.Parse(dt.Rows[0]["AdEnd"].ToString()).ToString("yyyy-MM-dd");
                //  button2.Enabled = true;
            }
        }

        private void JiaoFei_Load(object sender, EventArgs e)
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
            dtpend.Text = DateTime.Now.Year + "-12-31";
            dtpend2.Text = DateTime.Now.Year + "-12-31";
            dtpadend.Text = DateTime.Now.Year + "-12-31";
            //sql = "select Name,Value from dbo.Dict_View where TypeName='Four' order by seq ";
            //cbbfour.ValueMember = "Value";
            //cbbfour.DisplayMember = "Name";
            //cbbfour.DataSource = db.ExecuteDataTable(sql);
        }

        public string GetSum()
        {
            string sum = "0";
            sum = (nudHouse.Value + nudWuye.Value + nudZhiAn.Value + nudZhandi.Value + nudShiChang.Value).ToString("0");
            return sum;
        }

        private void nudHouse_Leave(object sender, EventArgs e)
        {
            lblsumpay.Text = GetSum();
        }

        private void nudWuye_Leave(object sender, EventArgs e)
        {
            lblsumpay.Text = GetSum();
        }

        private void nudZhiAn_Leave(object sender, EventArgs e)
        {
            lblsumpay.Text = GetSum();
        }

        private void nudZhandi_Leave(object sender, EventArgs e)
        {
            lblsumpay.Text = GetSum();
        }

        private void nudShiChang_Leave(object sender, EventArgs e)
        {
            lblsumpay.Text = GetSum();
        }

        /// <summary>
        /// 广告租赁费设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
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
            if (string.IsNullOrWhiteSpace(four))
            {
                MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            DataTable dt = db.ExecuteDataTable("select * from MerAllInfo_View where MerNum='" + MerNum + "'");
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("此商户还没开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudad.Value == 0)
            {
                MessageBox.Show("充值金额不能为0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string start = dtpadStart.Text;
            string end = dtpadend.Text;
            string AdPosition = txtAdPosition.Text;
            string AdRemark = txtAdRemark.Text;
            if (string.IsNullOrWhiteSpace(AdPosition))
            {
                MessageBox.Show("地址不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(AdRemark))
            {
                MessageBox.Show("备注不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始日期不能大于结束日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string result = db.NonQuery("update MerInfo set AdPosition='" + AdPosition + "',AdRemark='" + AdRemark + "', AdFei='" + nudCar.Value + "',AdStart='" + start + "',AdEnd='" + end + "'  where MerNum='" + MerNum + "'").ToString();
            MessageBox.Show("设置成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 广告租赁费缴费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
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
            if (string.IsNullOrWhiteSpace(four))
            {
                MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            DataTable dt = db.ExecuteDataTable("select * from MerAllInfo_View where MerNum='" + MerNum + "'");
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("此商户还没开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudad.Value == 0)
            {
                MessageBox.Show("充值金额不能为0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string start = dtpadStart.Text;
            string end = dtpadend.Text;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            string AdPosition = txtAdPosition.Text;
            string AdRemark = txtAdRemark.Text;
            if (string.IsNullOrWhiteSpace(AdPosition))
            {
                MessageBox.Show("地址不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(AdRemark))
            {
                MessageBox.Show("备注不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始日期不能大于结束日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("确定是否收到现金?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                button3.Enabled = false;
                string result = (db.Scalar("update MerInfo set AdPosition='" + AdPosition + "',AdRemark='" + AdRemark + "',AdFei='" + nudad.Value + "',AdStart='" + start + "',AdEnd='" + end + "'  where MerNum='" + MerNum + "'") ?? "0").ToString();
                string TradeDate = DateTime.Now.ToString("yyyyMMdd");
                string TradeTime = DateTime.Now.ToString("HH:mm");
                string OrderId = Common.GetOrder("GG");
                txtadOrder.Text = OrderId;
                string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "insert into S_Water(OrderID,MerNum,RetCode,TermID,ZJRXM,ZJHM,TradeDate,TradeTime,LiquiDate,cardid,TransMoney,TradeType,IsCash,JiaoFeiStart,JiaoFeiEnd,CreateDate)  values ('" + OrderId + "','" + MerNum + "','00','U_" + Common.UserId + "','" + txtUserName.Text + "','" + txtsfz.Text + "','" + TradeDate + "','" + TradeTime + "','" + TradeDate + "','','" + int.Parse((nudad.Value * 100).ToString("0")) + "',10,1,'" + start + "','" + end + "','" + createtime + "')";
                db.NonQuery(sql);
                Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtUserName.Text + "广告租赁费" + nudad.Value + "元");
                MessageBox.Show("缴费成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button3.Enabled = true;
            }
        }

        /// <summary>
        /// 房租打印凭条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFzOrder.Text))
            {
                MessageBox.Show("请先交费", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FzReport f = new FzReport();
            f.ShiYong = nudShiChang.Value.ToString("#0");
            f.WuYe = nudWuye.Value.ToString("#0");
            f.ZhiAn = nudZhiAn.Value.ToString("#0");
            f.ZhanDi = nudZhandi.Value.ToString("#0");
            f.ShiChang = nudShiChang.Value.ToString("#0");
            f.FangZu = decimal.Parse(lblsumpay.Text).ToString("#0");
            f.MerUserName = txtUserName.Text;
            f.MerName = cbbone.Text + cbbtwo.Text + cbbthree.Text + cbbfour.Text;
            f.PayTime = dtpstart.Value.ToString("yyyy.MM.dd") + "-" + dtpend.Value.ToString("yyyy.MM.dd");
            f.OrderId = txtFzOrder.Text;
            f.Remark = "收据";
            f.Owner = this;
            f.ShowDialog();
        }

        /// <summary>
        /// 车辆年费打印凭条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCLOrder.Text))
            {
                MessageBox.Show("请先交费", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
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
            if (string.IsNullOrWhiteSpace(four))
            {
                MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            TempOther f = new TempOther();
            f.OrderId = txtCLOrder.Text;
            f.sfxm = "车辆年费";
            f.MerName = GetMerName(MerNum);
            f.Remark = txtCPHM.Text.ToUpper();
            f.PayTime = dtpstart2.Value.ToString("yyyy.MM.dd") + "-" + dtpend2.Value.ToString("yyyy.MM.dd");
            f.Money = nudCar.Value.ToString("#0");
            f.jkr = txtUserName.Text;
            f.Owner = this;
            f.ShowDialog();
        }

        /// <summary>
        /// 广告租赁费打印凭条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtadOrder.Text))
            {
                MessageBox.Show("请先交费", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
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
            if (string.IsNullOrWhiteSpace(four))
            {
                MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            TempOther f = new TempOther();
            f.OrderId = txtadOrder.Text;
            f.sfxm = txtUserName.Text +"-"+ txtAdPosition.Text;
            f.MerName=GetMerName(MerNum);
            f.Remark = txtAdRemark.Text;
            f.PayTime = dtpadStart.Value.ToString("yyyy.MM.dd") + "-" + dtpadend.Value.ToString("yyyy.MM.dd");
            f.Money = nudad.Value.ToString("#0");
            f.jkr = txtUserName.Text;
            f.Owner = this;
            f.ShowDialog();
        }

        /// <summary>
        /// 押金设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
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
            if (string.IsNullOrWhiteSpace(four))
            {
                MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            DataTable dt = db.ExecuteDataTable("select * from MerAllInfo_View where MerNum='" + MerNum + "'");
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("此商户还没开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudyj.Value == 0)
            {
                MessageBox.Show("充值金额不能为0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string result = db.NonQuery("update MerInfo set YaJin='" + nudyj.Value + "'  where MerNum='" + MerNum + "'").ToString();
            MessageBox.Show("设置成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 押金缴费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
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
            if (string.IsNullOrWhiteSpace(four))
            {
                MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            DataTable dt = db.ExecuteDataTable("select * from MerAllInfo_View where MerNum='" + MerNum + "'");
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("此商户还没开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudyj.Value == 0)
            {
                MessageBox.Show("充值金额不能为0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (DialogResult.Yes == MessageBox.Show("确定是否收到现金?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                button3.Enabled = false;
                string result = db.NonQuery("update MerInfo set YaJin='" + nudyj.Value + "'  where MerNum='" + MerNum + "'").ToString();
                string TradeDate = DateTime.Now.ToString("yyyyMMdd");
                string TradeTime = DateTime.Now.ToString("HH:mm");
                string OrderId = Common.GetOrder("YJ");
                txtYJOrder.Text = OrderId;
                string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "insert into S_Water(OrderID,MerNum,RetCode,TermID,ZJRXM,ZJHM,TradeDate,TradeTime,LiquiDate,cardid,TransMoney,TradeType,IsCash,CreateDate)  values ('" + OrderId + "','" + MerNum + "','00','U_" + Common.UserId + "','" + txtUserName.Text + "','" + txtsfz.Text + "','" + TradeDate + "','" + TradeTime + "','" + TradeDate + "','','" + int.Parse((nudyj.Value * 100).ToString("0")) + "',11,1,'" + createtime + "')";
                db.NonQuery(sql);
                Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtUserName.Text + "押金" + nudyj.Value + "元");
                MessageBox.Show("缴费成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button3.Enabled = true;
            }

        }

        /// <summary>
        /// 押金打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtYJOrder.Text))
            {
                MessageBox.Show("请先交费", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
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
            if (string.IsNullOrWhiteSpace(four))
            {
                MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            TempYJ f = new TempYJ();
            f.OrderId = txtYJOrder.Text;
            f.sfxm = "房租押金";
            f.zjhm = txtsfz.Text;
            f.MerName = GetMerName(MerNum);
            f.fzr = txtUserName.Text;
            f.Money = nudyj.Value.ToString("#0");
            f.Owner = this;
            f.ShowDialog();
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

        private void nudyj_ValueChanged(object sender, EventArgs e)
        {
            nudyj.Value = (int)nudyj.Value;
        }

        private void nudad_ValueChanged(object sender, EventArgs e)
        {
            nudad.Value = (int)nudad.Value;
        }

        private void nudCar_ValueChanged(object sender, EventArgs e)
        {
            nudCar.Value = (int)nudCar.Value;
        }

        private void nudHouse_ValueChanged(object sender, EventArgs e)
        {
            nudHouse.Value = (int)nudHouse.Value;
        }

        private void nudWuye_ValueChanged(object sender, EventArgs e)
        {
            nudWuye.Value = (int)nudWuye.Value;
        }

        private void nudZhiAn_ValueChanged(object sender, EventArgs e)
        {
            nudZhiAn.Value = (int)nudZhiAn.Value;
        }

        private void nudZhandi_ValueChanged(object sender, EventArgs e)
        {
            nudZhandi.Value = (int)nudZhandi.Value;
        }

        private void nudShiChang_ValueChanged(object sender, EventArgs e)
        {
            nudShiChang.Value = (int)nudShiChang.Value;
        }
    }
}
