using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XK.NBear.DB;
using System.Text.RegularExpressions;
using dmsk;

namespace UserManager
{
    public partial class ShiChangKaKaiHu : Temp
    {
        public ShiChangKaKaiHu()
        {
            InitializeComponent();
        }
        common com = new common();
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");

        /// <summary>
        /// 市场卡开户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            string xm = txtxm.Text.Trim();
            string phone = txtphone.Text.Trim();
            string sfz = txtsfz.Text.Trim();
            string banknum = txtbanknum.Text.Trim();
            string cphm = txtCPHM.Text.Trim().ToUpper();
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
            if (string.IsNullOrWhiteSpace(cphm))
            {
                MessageBox.Show("车牌号码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
            //if (IsExit(sfz, phone, ""))
            //{
            //    MessageBox.Show("手机号或身份证号不能重复", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            string password = sfz.Substring(sfz.Length - 6).ToLower();
            password = password.Replace("x", "0");
            string BindNum = phone;
            string sql = "", userid = "", carduid = "";
            if (!IsExit(sfz, phone, banknum))//不存在用户信息
            {
                sql = " INSERT INTO [MerUser] ([ZJRXM],[ZJHM],[BankCardNo],[PhoneNum],[Password],[BindNum]) output inserted.id VALUES ('" + xm + "','" + sfz + "','" + banknum + "','" + phone + "','" + password + "','" + BindNum + "')";
                userid = (db.Scalar(sql) ?? "0").ToString();
                lbluserid.Text = userid;
                if (userid != "0")
                {
                    int flag2 = com.pd_gsk();
                    if (flag2 == 0)
                    {
                        MessageBox.Show("非公司卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    carduid = com.get_UID(out msg);
                    if (carduid == "-1")
                    {
                        MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (IsExitWaterCar(carduid))
                    {
                        MessageBox.Show("卡号重复，请换张卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    sql = "INSERT INTO [MerInfo] ([MerUserID],[ShuiCardNum],[OpenTime],[CarNum]) VALUES " +
                  "('" + lbluserid.Text + "','" + carduid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+cphm+"')";
                    int result = db.NonQuery(sql);
                    if (result == 1)
                    {
                        db.NonQuery("insert into U_AuditLog(OpeType,UserID,CarType,CardNum) values('开户','" + Common.UserId + "',2,'" + carduid + "')");
                        Common.WriteOpearateLog(Common.UserId, "用户" + txtxm.Text + "市场卡开户");
                       
                        MessageBox.Show("开户成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("开户失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
                else
                {
                    MessageBox.Show("添加用户信息失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else//存在用户信息
            {
                sql = " select id from MerUser where ZJHM='" + sfz + "' and PhoneNum='" + phone + "' ";
                string result = (db.Scalar(sql) ?? "0").ToString();
                lbluserid.Text = result;
                int flag2 = com.pd_gsk();
                if (flag2 == 0)
                {
                    MessageBox.Show("非公司卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                carduid = com.get_UID(out msg);
                if (carduid == "-1")
                {
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (IsExitWaterCar(carduid))
                {
                    MessageBox.Show("卡号重复，请换张卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (IsExitMerUser(result))
                {
                    MessageBox.Show("此手机号或身份证号已开过户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                sql = "INSERT INTO [MerInfo] ([MerUserID],[ShuiCardNum],[OpenTime],[CarNum]) VALUES " +
              "('" + lbluserid.Text + "','" + carduid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + cphm + "')";
                int result3 = db.NonQuery(sql);
                if (result3 == 1)
                {
                    db.NonQuery("insert into U_AuditLog(OpeType,UserID,CarType,CardNum) values('开户','" + Common.UserId + "',2,'" + carduid + "')");
                    MessageBox.Show("开户成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("开户失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 查询
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
                string cphm = (db.Scalar(" select CarNum from MerInfo where MerUserID='" + dt.Rows[0]["id"].ToString() + "'") ?? "").ToString();
                txtCPHM.Text = cphm;
            }
            else
            {
                MessageBox.Show("无此用户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

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
    }
}
