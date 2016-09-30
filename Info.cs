using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using dmsk;
using System.Text.RegularExpressions;
using XK.NBear.DB;

namespace UserManager
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }
        common com = new common();
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");

        #region 按钮事件

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string sfz = txtsfz.Text.Trim();
            string msg = CheckIDCard(sfz);
            if (msg != "合法身份证")
            {
                MessageBox.Show("身份证" + msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dt = db.ExecuteDataTable("select * from MerUser where ZJHM='" + sfz + "'");
            if (dt != null && dt.Rows.Count != 0)
            {
                if (dt.Rows.Count > 1)
                {
                    MessageBox.Show("身份重复", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                txtxm.Text = dt.Rows[0]["ZJRXM"].ToString();
                txtphone.Text = dt.Rows[0]["PhoneNum"].ToString();
                txtsfz.Text = dt.Rows[0]["ZJHM"].ToString();
                txtbanknum.Text = dt.Rows[0]["BankCardNo"].ToString();
                lbluserid.Text = dt.Rows[0]["id"].ToString();
            }
            else
            {
                MessageBox.Show("无此用户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        /// <summary>
        /// 保存基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string xm = txtxm.Text.Trim();
            string phone = txtphone.Text.Trim();
            string sfz = txtsfz.Text.Trim();
            string banknum = txtbanknum.Text.Trim();
            if (string.IsNullOrWhiteSpace(xm))
            {
                MessageBox.Show("姓名不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((!Regex.IsMatch(phone, @"^(^1\d{10}$)$", RegexOptions.IgnoreCase)))
            {
                MessageBox.Show("请输入正确的手机号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string msg = CheckIDCard(sfz);
            if (msg != "合法身份证")
            {
                MessageBox.Show("身份证" + msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(banknum))
            {
                MessageBox.Show("银行卡号不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string password = sfz.Substring(sfz.Length - 6).ToLower();
            password = password.Replace("x", "0");
            string BindNum = phone;
            string sql = "";
            if (!IsExit(sfz, phone, banknum))
            {
                sql = " INSERT INTO [MerUser] ([ZJRXM],[ZJHM],[BankCardNo],[PhoneNum],[Password],[BindNum])VALUES ('" + xm + "','" + sfz + "','" + banknum + "','" + phone + "','" + password + "','" + BindNum + "')";
                db.NonQuery(sql);
                MessageBox.Show("添加用户信息成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("手机号或身份证号不能与其他人重复", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            sql = " select id from MerUser where ZJHM='" + sfz + "' and PhoneNum='" + phone + "' and BankCardNo='" + banknum + "' ";
            string result = (db.Scalar(sql) ?? "0").ToString();
            lbluserid.Text = result;
        }


        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            int flag = com.geshihua();
            if (flag != 0)
            {
                MessageBox.Show("格式化卡失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("格式化卡成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            button5.Enabled = true;
        }

        /// <summary>
        /// 擦除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            int flag = com.cachu();
            if (flag != 0)
            {
                MessageBox.Show("擦除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("擦除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            button6.Enabled = true;
        }
        /// <summary>
        /// 绑定电卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            txtDianOrder.Text = "";
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
            if (lbluserid.Text == "0")
            {
                MessageBox.Show("请先填写用户基本信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            int flag = 0;
            string DianCardNum = txtDianCardNum.Text.Trim();
            if (string.IsNullOrWhiteSpace(DianCardNum))
            {
                MessageBox.Show("电表编号不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string dl = "0", sql = "", msg = "";
            int result = 0;
            if (string.IsNullOrWhiteSpace(txtDianPrice.Text))
            {
                MessageBox.Show("请在系统设置中设置电价格", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (string.IsNullOrWhiteSpace(nudDianBuyMoney.Value.ToString()) || nudDianBuyMoney.Value.ToString() == "0")
            //{
            //    MessageBox.Show("请输入充值金额", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            if (!string.IsNullOrWhiteSpace(nudDianBuyMoney.Value.ToString()) && nudDianBuyMoney.Value != 0)
            {
                txtDianTotal.Text = (float.Parse(nudDianBuyMoney.Value.ToString()) / float.Parse(txtDianPrice.Text)).ToString("0");
                dl = txtDianTotal.Text;
            }
            if (IsExitDianCar(DianCardNum))
            {
                MessageBox.Show("电表编号重复", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql2 = " select ShuiCardNum from MerInfo where MerNum='" + MerNum + "'";
            string Cardid = (db.Scalar(sql2) ?? "0").ToString();
            if (string.IsNullOrWhiteSpace(Cardid) || Cardid == "0")
            {
                lblCardid.Text = "";
                MessageBox.Show("请先水卡开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                lblCardid.Text = Cardid;
            }
            button1.Enabled = false;
            if (!IsExitMer(MerNum))
            {
                sql = "INSERT INTO [MerInfo] ([MerUserID],[DianCardNum],[MerNum],[DYHBH],[GouDianNum],[DGDL]) VALUES " +
              "('" + lbluserid.Text + "','" + DianCardNum + "','" + MerNum + "','"+DianCardNum+"','1','"+dl+"')";
                result = db.NonQuery(sql);
                if (result == 1)
                {
                    try
                    {
                        flag = com.dian_kaihu(DianCardNum, dl, "20", "9999", "0", DianCardNum, "111111", "111111", out msg);
                        if (flag != 0)
                        {
                            txtDianOrder.Text = "";
                            db.NonQuery(" update MerInfo set DianCardNum='',GouDianNum='0',DGDL='0' where MerNum='" + MerNum + "' ");
                            MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            button1.Enabled = true;
                            return;
                        }
                        else
                        {
                            //  db.NonQuery("insert into U_AuditLog(OpeType,UserID,MerNum,CarType) values('开户','" + Common.UserId + "','" + MerNum + "',1)");
                            string TradeDate = DateTime.Now.ToString("yyyyMMdd");
                            string TradeTime = DateTime.Now.ToString("HH:mm");
                            string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            string OrderId = Common.GetOrder("DF");
                            txtDianOrder.Text = OrderId;
                            sql = "insert into S_Water(OrderID,MerNum,RetCode,TermID,ZJRXM,ZJHM,TradeDate,TradeTime,LiquiDate,cardid,TransMoney,TradeType,IsCash,CreateDate) values ('" + OrderId + "','" + MerNum + "','00','U_" + Common.UserId + "','" + txtxm.Text + "','" + txtsfz.Text + "','" + TradeDate + "','" + TradeTime + "','" + TradeDate + "','" + MerNum + "','" + int.Parse((nudDianBuyMoney.Value * 100).ToString("0")) + "',2,1,'"+createtime+"')";
                            db.NonQuery(sql);
                            Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtxm.Text + "电卡编号" + txtDianCardNum.Text +"充值"+ nudDianBuyMoney.Value + "元");
                            MessageBox.Show("电卡开户成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch
                    {
                        db.NonQuery(" update MerInfo set DianCardNum='',GouDianNum='0',DGDL='0' where MerNum='" + MerNum + "' ");
                        MessageBox.Show("操作卡失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("电卡开户失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {

                if (!IsOkUser(MerNum, lbluserid.Text))
                {
                    MessageBox.Show("该用户与商户不匹配", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    button1.Enabled = true;
                    return;
                }
                if (!IsExitMer(MerNum, null, DianCardNum))
                {
                    sql = "update MerInfo set DianCardNum='" + DianCardNum + "',DYHBH='" + DianCardNum + "',GouDianNum='1',DGDL='"+dl+"'  where MerNum='" + MerNum + "'";
                    result = db.NonQuery(sql);
                    if (result == 1)
                    {
                        try
                        {
                            flag = com.dian_kaihu(DianCardNum, dl, "20", "9999", "0", DianCardNum, "111111", "111111", out msg);
                            if (flag != 0)
                            {
                                txtDianOrder.Text = "";
                                db.NonQuery(" update MerInfo set DianCardNum='',GouDianNum='0',DGDL='0' where MerNum='" + MerNum + "' ");
                                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                button1.Enabled = true;
                                return;
                            }
                            else
                            {
                                db.NonQuery("insert into U_AuditLog(OpeType,UserID,MerNum,CarType) values('开户','" + Common.UserId + "','" + MerNum + "',1)");

                                string TradeDate = DateTime.Now.ToString("yyyyMMdd");
                                string TradeTime = DateTime.Now.ToString("HH:mm");
                                string OrderId = Common.GetOrder("DF");
                                txtDianOrder.Text = OrderId;
                                string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                sql = "insert into S_Water(OrderID,MerNum,RetCode,TermID,ZJRXM,ZJHM,TradeDate,TradeTime,LiquiDate,cardid,TransMoney,TradeType,IsCash,CreateDate) values ('" + OrderId + "','" + MerNum + "','00','U_" + Common.UserId + "','" + txtxm.Text + "','" + txtsfz.Text + "','" + TradeDate + "','" + TradeTime + "','" + TradeDate + "','" + MerNum + "','" + int.Parse((nudDianBuyMoney.Value * 100).ToString("0")) + "',2,1,'"+createtime+"')";
                                db.NonQuery(sql);
                                Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtxm.Text + "电卡编号" + txtDianCardNum.Text + "充值" + nudDianBuyMoney.Value + "元");
                                MessageBox.Show("电卡开户成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch
                        {
                            db.NonQuery(" update MerInfo set DianCardNum='',GouDianNum='0',DGDL='0' where MerNum='" + MerNum + "' ");
                            MessageBox.Show("操作卡失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("电卡开户失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("开户失败-该商户已开户完成，请勿重复开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            button1.Enabled = true;
        }
        /// <summary>
        /// 绑定水卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            txtWaterOrder.Text = "";
            if (lbluserid.Text == "0")
            {
                MessageBox.Show("请先填写用户基本信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //string MerNum = txtMerNum.Text.Trim();
            //if (string.IsNullOrWhiteSpace(MerNum))
            //{
            //    MessageBox.Show("商户编号不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
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
            button4.Enabled = false;
            int flag2 = com.pd_gsk();
            if (flag2 == 0)
            {
                MessageBox.Show("非公司卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button4.Enabled = true;
                return;
            }
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            string carduid = "";
            string msg = "";
            string sql = "";
            int result = 0;
            int flag = 0;//开户卡状态
            carduid = com.get_UID(out msg);
            if (carduid == "-1")
            {
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button4.Enabled = true;
                return;
            }
            txtwatercardid.Text = carduid;
            if (IsExitWaterCar(carduid))
            {
                MessageBox.Show("卡号重复，请换张卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button4.Enabled = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(txtWaterPrice.Text))
            {
                MessageBox.Show("请在系统设置中设置水价格", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button4.Enabled = true;
                return;
            }
            //if (string.IsNullOrWhiteSpace(nudWaterBuyMoney.Value.ToString()) || nudWaterBuyMoney.Value.ToString() == "0")
            //{
            //    MessageBox.Show("请输入充值金额", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    button4.Enabled = true;
            //    return;
            //}
            string sl = "0";
            if (!string.IsNullOrWhiteSpace(nudWaterBuyMoney.Value.ToString()) && nudWaterBuyMoney.Value != 0)
            {
                txtWaterTotal.Text = (float.Parse(nudWaterBuyMoney.Value.ToString()) / float.Parse(txtWaterPrice.Text)).ToString("0.0");
                sl = txtWaterTotal.Text;
            }
            //   string sql = "INSERT INTO [MerInfo] ([MerUserID],[ShuiCardNum],[MerNum],[DianCardNum],[CarNianFei],[FangZu],[Yue],[isCancel],[Treaty_State],[isShuiCardLost],[SBBH],[ZGL],[BJL],[TZL],[JFCS]) VALUES "+
            if (!IsExitMer(MerNum))
            {
                sql = "INSERT INTO [MerInfo] ([MerUserID],[ShuiCardNum],[MerNum],[OpenTime],[ZGL],[JFCS])  output inserted.id VALUES " +
              "('" + lbluserid.Text + "','" + carduid + "','" + MerNum + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + sl + "','1')";
               string res = (db.Scalar(sql) ?? "0").ToString();
                if (res != "0")
                {
                    try
                    {
                     //   com.InitCard();
                        flag = com.kaihu(MerNum, "1", sl, out msg);
                    }
                    catch
                    {
                        db.NonQuery(" update MerInfo set ShuiCardNum='',ZGL='0',JFCS='0' where MerNum='" + MerNum + "' ");
                        MessageBox.Show("操作卡失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    if (flag == 0)
                    {
                        db.NonQuery("insert into U_AuditLog(OpeType,UserID,MerNum,CarType,CardNum,MerName) values('开户','" + Common.UserId + "','" + MerNum + "',0,'" + carduid + "','" + GetMerName(MerNum) + "')");
                        string TradeDate = DateTime.Now.ToString("yyyyMMdd");
                        string TradeTime = DateTime.Now.ToString("HH:mm");
                        string OrderId = Common.GetOrder("SF");
                        txtWaterOrder.Text = OrderId;
                        txtDianCardNum.Text = "1" + res.PadLeft(5, '0');
                        string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        sql = "insert into S_Water(OrderID,MerNum,RetCode,TermID,ZJRXM,ZJHM,TradeDate,TradeTime,LiquiDate,cardid,TransMoney,TradeType,IsCash,CreateDate) values ('" + OrderId + "','" + MerNum + "','00','U_" + Common.UserId + "','" + txtxm.Text + "','" + txtsfz.Text + "','" + TradeDate + "','" + TradeTime + "','" + TradeDate + "','" + carduid + "','" + int.Parse((nudWaterBuyMoney.Value * 100).ToString("0")) + "',1,1,'"+createtime+"')";
                        db.NonQuery(sql);
                        Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtxm.Text + "水卡编号" + txtwatercardid.Text+ "开户" + nudWaterBuyMoney.Value + "元");
                        MessageBox.Show("水卡开户成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        txtWaterOrder.Text = "";
                        db.NonQuery(" update MerInfo set ShuiCardNum='',ZGL='0',JFCS='0' where MerNum='" + MerNum + "' ");
                        MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("水卡开户失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (!IsOkUser(MerNum, lbluserid.Text))
                {
                    MessageBox.Show("该用户与商户不匹配", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    button4.Enabled = true;
                    return;
                }

                if (!IsExitMer(MerNum, carduid, null))
                {
                    sql = "update MerInfo set ShuiCardNum='" + carduid + "',OpenTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',ZGL='" + sl + "' ,JFCS='1' where MerNum='" + MerNum + "'";
                    result = db.NonQuery(sql);
                    if (result == 1)
                    {
                        try
                        {
                            flag = com.kaihu(MerNum, "1", sl, out msg);
                        }
                        catch
                        {
                            db.NonQuery(" update MerInfo set ShuiCardNum='',ZGL='0',JFCS='0' where MerNum='" + MerNum + "' ");
                            MessageBox.Show("操作卡失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (flag == 0)
                        {
                            db.NonQuery("insert into U_AuditLog(OpeType,UserID,MerNum,CarType,CardNum,MerName) values('开户','" + Common.UserId + "','" + MerNum + "',0,'" + carduid + "','" + GetMerName(MerNum) + "')");

                            string TradeDate = DateTime.Now.ToString("yyyyMMdd");
                            string TradeTime = DateTime.Now.ToString("HH:mm");
                            string OrderId = Common.GetOrder("SF");
                            txtWaterOrder.Text = OrderId;
                            string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            sql = "insert into S_Water(OrderID,MerNum,RetCode,TermID,ZJRXM,ZJHM,TradeDate,TradeTime,LiquiDate,cardid,TransMoney,TradeType,IsCash,CreateDate) values ('" + OrderId + "','" + MerNum + "','00','U_" + Common.UserId + "','" + txtxm.Text + "','" + txtsfz.Text + "','" + TradeDate + "','" + TradeTime + "','" + TradeDate + "','" + carduid + "','" + int.Parse((nudWaterBuyMoney.Value * 100).ToString("0")) + "',1,1,'"+createtime+"')";
                            db.NonQuery(sql);
                            Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtxm.Text + "水卡编号" + txtwatercardid.Text + "开户" + nudWaterBuyMoney.Value + "元");
                            MessageBox.Show("水卡开户成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            txtWaterOrder.Text = "";
                            db.NonQuery(" update MerInfo set ShuiCardNum='',ZGL='0',JFCS='0' where MerNum='" + MerNum + "' ");
                            MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("水卡开户失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("开户失败-该商户已开户完成，请勿重复开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            button4.Enabled = true;
        }

        #endregion

        #region 窗体事件

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Info_Load(object sender, EventArgs e)
        {
            txtWaterPrice.Text = GetWaterPrice();
            txtDianPrice.Text = GetDianPrice();
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

        #endregion

        #region 方法


        #region 身份证号码验证

        /// <summary>
        /// 验证身份证号码
        /// </summary>
        /// <param name="Id">身份证号码</param>
        /// <returns>验证成功为True，否则为False</returns>
        public string CheckIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                string check = CheckIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                string check = CheckIDCard15(Id);
                return check;
            }
            else
            {
                return "位数不正确";
            }
        }

        /// <summary>
        /// 验证15位身份证号
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        private string CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return "数字验证失败";//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return "省份验证失败";//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return "生日验证失败";//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return "校验码验证失败";//校验码验证
            }
            return "合法身份证";//符合GB11643-1999标准
        }

        /// <summary>
        /// 验证18位身份证号
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        private string CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return "数字验证失败";//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return "省份验证失败";//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return "生日验证失败";//生日验证
            }
            return "合法身份证";//符合15位身份证标准
        }
        #endregion

        /// <summary>
        /// 根据身份证号、手机号、银行卡号判断用户表数据是否唯一
        /// </summary>
        /// <param name="sfz"></param>
        /// <param name="phone"></param>
        /// <param name="bank"></param>
        /// <returns></returns>
        public bool IsExit(string sfz, string phone, string bank)
        {
            bool state = false;
            //  string sql = " select count(*) from MerUser where ZJHM='" + sfz + "' and PhoneNum='" + phone + "' and BankCardNo='" + bank + "' ";
            string sql = " select count(*) from MerUser where ZJHM='" + sfz + "' ";
            string result = (db.Scalar(sql) ?? "0").ToString();
            if (result != "0")
            {
                state = true;
                return state;
            }
            sql = " select count(*) from MerUser where PhoneNum='" + phone + "' ";
            result = (db.Scalar(sql) ?? "0").ToString();
            if (result != "0")
                state = true;
            return state;
        }

        /// <summary>
        /// 根据商户编号判断是否是唯一
        /// </summary>
        /// <param name="MerNum"></param>
        /// <returns></returns>
        public bool IsExitMer(string MerNum)
        {
            bool state = false;
            string sql = " select count(*) from MerInfo where MerNum='" + MerNum + "' ";//首先判断商户编号唯一
            string result = (db.Scalar(sql) ?? "0").ToString();
            if (int.Parse(result) > 0)
                state = true;
            return state;
        }
        /// <summary>
        /// 判断MerInfo 中是否存在该用户
        /// </summary>
        /// <param name="MerUserId"></param>
        /// <returns></returns>
        public bool IsExitMerUser(string MerUserId)
        {
            bool state = false;
            string sql = " select count(*) from MerInfo where MerUserID='" + MerUserId + "' ";//首先判断商户编号唯一
            string result = (db.Scalar(sql) ?? "0").ToString();
            if (int.Parse(result) > 0)
                state = true;
            return state;
        }

        /// <summary>
        /// 判断卡号是否重复
        /// </summary>
        /// <param name="ShuiCardNum"></param>
        /// <returns></returns>
        public bool IsExitWaterCar(string ShuiCardNum)
        {
            bool state = false;
            string sql = " select count(*) from MerInfo where ShuiCardNum='" + ShuiCardNum + "' ";//首先判断商户编号唯一
            string result = (db.Scalar(sql) ?? "0").ToString();
            if (int.Parse(result) > 0)
                state = true;
            return state;
        }

        /// <summary>
        /// 判断电卡是否重复
        /// </summary>
        /// <param name="DianCardNum"></param>
        /// <returns></returns>
        public bool IsExitDianCar(string DianCardNum)
        {
            bool state = false;
            string sql = " select count(*) from MerInfo where DianCardNum='" + DianCardNum + "' ";//首先判断商户编号唯一
            string result = (db.Scalar(sql) ?? "0").ToString();
            if (int.Parse(result) > 0)
                state = true;
            return state;
        }
        /// <summary>
        /// 根据商户编号判断是否已绑定电卡或水卡
        /// </summary>
        /// <param name="MerUserID"></param>
        /// <param name="shuiKaNum"></param>
        /// <param name="DianKaNum"></param>
        /// <returns></returns>
        public bool IsExitMer(string MerNum, string shuiKaNum, string DianKaNum)
        {
            bool state = false;
            string column = "";
            if (!string.IsNullOrWhiteSpace(shuiKaNum))
            {
                column = "ShuiCardNum";
            }
            if (!string.IsNullOrWhiteSpace(DianKaNum))
            {
                column = "DianKaNum";
            }
            string sql = " select " + column + " from MerInfo where MerNum='" + MerNum + "' ";
            string result = (db.Scalar(sql) ?? "0").ToString();
            if (!string.IsNullOrWhiteSpace(result) && result != "0")
                state = true;
            return state;
        }

        /// <summary>
        /// 判断用户与商户是否匹配正确
        /// </summary>
        /// <param name="MerNum"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool IsOkUser(string MerNum, string userid)
        {
            bool state = false;
            string sql = " select MerUserID from MerInfo where MerNum='" + MerNum + "' ";
            string result = (db.Scalar(sql) ?? "0").ToString();
            if (!string.IsNullOrWhiteSpace(result) && result != "0" && result == userid)
                state = true;
            return state;
        }

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
        private void button7_Click_1(object sender, EventArgs e)
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
                txtxm.Text = "";
                txtphone.Text = "";
                txtsfz.Text = "";
                txtbanknum.Text = "";
                lbluserid.Text = "";
                return;
            }
            if (dt != null && dt.Rows.Count == 0)
            {
                MessageBox.Show("无此商户信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //txtxm.Text = "";
                //txtphone.Text = "";
                //txtsfz.Text = "";
                //txtbanknum.Text = "";
                //lbluserid.Text = "";
                return;
            }

            if (dt != null && dt.Rows.Count == 1)
            {
                txtxm.Text = dt.Rows[0]["ZJRXM"].ToString();
                txtphone.Text = dt.Rows[0]["PhoneNum"].ToString();
                txtsfz.Text = dt.Rows[0]["ZJHM"].ToString();
                txtbanknum.Text = dt.Rows[0]["BankCardNo"].ToString();
                lbluserid.Text = dt.Rows[0]["MerUserID"].ToString();
                if (string.IsNullOrWhiteSpace(dt.Rows[0]["DianCardNum"].ToString()))
                {
                    txtDianCardNum.Text = "1" + dt.Rows[0]["ID"].ToString().PadLeft(5, '0');
                }
                else
                {
                    txtDianCardNum.Text = dt.Rows[0]["DianCardNum"].ToString();
                }
            }
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
        /// 市场卡开户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
          

        }
        public string type = "1";//1水卡2电卡
        /// <summary>
        /// 水卡打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            type = "1";
            if (string.IsNullOrWhiteSpace(txtWaterOrder.Text))
            {
                MessageBox.Show("请先缴费", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            WaterTemp f = new WaterTemp();
            f.Month = DateTime.Now.ToString("MM") + " 月";
            f.OrderId = txtWaterOrder.Text;
            f.xm = txtxm.Text;
            f.MerName = GetMerName(MerNum);// txtmerName.Text;
            f.cs = "1 次";
            f.gsl = txtWaterTotal.Text;
            f.zb = "0";
            f.dj = txtWaterPrice.Text;
            f.je = nudWaterBuyMoney.Value.ToString();
            f.Owner = this;
            f.ShowDialog();
            //if (printDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    printDocument1.Print();
            //}
        }

        /// <summary>
        /// 电卡打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            type = "2";
            if (string.IsNullOrWhiteSpace(txtDianOrder.Text))
            {
                MessageBox.Show("请先缴费", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            BatteryTemp f = new BatteryTemp();
            f.Month = DateTime.Now.ToString("MM") + " 月";
            f.OrderId = txtDianOrder.Text;
            f.xm = txtxm.Text;
            f.MerName = GetMerName(MerNum);
            f.cs = "1 次";
            f.gsl = txtDianTotal.Text;
            f.zb = "0";
            f.dj = txtDianPrice.Text;
            f.je = nudDianBuyMoney.Value.ToString();
            f.Owner = this;
            f.ShowDialog();

        }

        /// <summary>
        /// 打印内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //打印内容 为 自定义文本内容 
            Font font = new Font("宋体", 8);
            Brush bru = Brushes.Black;
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region 一卡通充值单
            if (type == "1")//水卡
            {
                e.Graphics.DrawString("衡水东明村集团（正联）", font, bru, 25, 20);
                e.Graphics.DrawString("储值卡充值单", font, bru, 50, 50);
                e.Graphics.DrawString("金额：" + nudWaterBuyMoney.Value + "", font, bru, 10, 80);
                e.Graphics.DrawString("流水号：" + txtWaterOrder.Text + "", font, bru, 10, 100);
                e.Graphics.DrawString("时间：" + time + "", font, bru, 10, 120);
                e.Graphics.DrawString("卡号：" + txtwatercardid.Text + "", font, bru, 10, 140);
                e.Graphics.DrawString("收费员：" + Common.realName + "", font, bru, 10, 160);
                e.Graphics.DrawString("交款人签字：", font, bru, 10, 180);

                e.Graphics.DrawString("衡水东明村集团（副联）", font, bru, 25, 210);
                e.Graphics.DrawString("储值卡充值单", font, bru, 50, 240);
                e.Graphics.DrawString("金额：" + nudWaterBuyMoney.Value + "", font, bru, 10, 270);
                e.Graphics.DrawString("流水号：" + txtDianOrder.Text + "", font, bru, 10, 290);
                e.Graphics.DrawString("时间：" + time + "", font, bru, 10, 310);
                e.Graphics.DrawString("卡号：" + txtwatercardid.Text + "", font, bru, 10, 330);
                e.Graphics.DrawString("收费员：" + Common.realName + "", font, bru, 10, 350);
                e.Graphics.DrawString("交款人签字：", font, bru, 10, 370);

            }
            else
            {

                e.Graphics.DrawString("衡水东明村集团（正联）", font, bru, 25, 20);
                e.Graphics.DrawString("储值卡充值单", font, bru, 50, 50);
                e.Graphics.DrawString("金额：" + nudDianBuyMoney.Value + "", font, bru, 10, 80);
                e.Graphics.DrawString("流水号：8131164706378281250", font, bru, 10, 100);
                e.Graphics.DrawString("时间：" + time + "", font, bru, 10, 120);
                e.Graphics.DrawString("卡号：" + lblCardid.Text + "", font, bru, 10, 140);
                e.Graphics.DrawString("收费员：" + Common.realName + "", font, bru, 10, 160);
                e.Graphics.DrawString("交款人签字：", font, bru, 10, 180);

                e.Graphics.DrawString("衡水东明村集团（副联）", font, bru, 25, 210);
                e.Graphics.DrawString("储值卡充值单", font, bru, 50, 240);
                e.Graphics.DrawString("金额：" + nudDianBuyMoney.Value + "", font, bru, 10, 270);
                e.Graphics.DrawString("流水号：8131164706378281250", font, bru, 10, 290);
                e.Graphics.DrawString("时间：" + time + "", font, bru, 10, 310);
                e.Graphics.DrawString("卡号：" + lblCardid.Text + "", font, bru, 10, 330);
                e.Graphics.DrawString("收费员：" + Common.realName + "", font, bru, 10, 350);
                e.Graphics.DrawString("交款人签字：", font, bru, 10, 370);

            }
            #endregion
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
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
                if (dt != null && dt.Rows.Count == 1)
                {
                    if (string.IsNullOrWhiteSpace(dt.Rows[0]["DianCardNum"].ToString()))
                    {
                        txtDianCardNum.Text = "1" + dt.Rows[0]["ID"].ToString().PadLeft(5, '0');
                    }
                    else
                    {
                        txtDianCardNum.Text = dt.Rows[0]["DianCardNum"].ToString();
                    }
                }
                else
                {
                    txtDianCardNum.Text = "";
                }
            }
        }

        private void nudWaterBuyMoney_ValueChanged(object sender, EventArgs e)
        {
            nudWaterBuyMoney.Value = (int)nudWaterBuyMoney.Value;
        }

        private void nudDianBuyMoney_ValueChanged(object sender, EventArgs e)
        {
            nudDianBuyMoney.Value = (int)nudDianBuyMoney.Value;
        }
    }
}
