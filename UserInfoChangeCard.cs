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
    public partial class UserInfoChangeCard : Form
    {
        public UserInfoChangeCard()
        {
            InitializeComponent();
        }
        common com = new common();
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
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
                where += " and  ZJHM='" + sfz + "'";
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
                MessageBox.Show("此商户还没开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dt != null && dt.Rows.Count == 1)
            {
                txtUserName.Text = dt.Rows[0]["ZJRXM"].ToString();
                txtsfz.Text = dt.Rows[0]["ZJHM"].ToString();
                txtphone.Text = dt.Rows[0]["PhoneNum"].ToString();
                txtAccountBalance.Text = dt.Rows[0]["Yue"].ToString();
                txtsbbh.Text = dt.Rows[0]["SBBH"].ToString();
                txtzgl.Text = dt.Rows[0]["ZGL"].ToString();
                txtbjl.Text = dt.Rows[0]["BJL"].ToString();
                txttzl.Text = dt.Rows[0]["TZL"].ToString();
                txtjfcs.Text = dt.Rows[0]["JFCS"].ToString();

                txtdbbh.Text = dt.Rows[0]["DianCardNum"].ToString();
                txtdbjfcs.Text = string.IsNullOrWhiteSpace(dt.Rows[0]["GouDianNum"].ToString()) ? "0" : dt.Rows[0]["GouDianNum"].ToString();
                txtGLD1.Text = string.IsNullOrWhiteSpace(dt.Rows[0]["DGDL"].ToString()) ? "0" : dt.Rows[0]["DGDL"].ToString();
                txttjl1.Text = string.IsNullOrWhiteSpace(dt.Rows[0]["DTJL"].ToString()) ? "0" : dt.Rows[0]["DTJL"].ToString();
                txtyb1.Text = string.IsNullOrWhiteSpace(dt.Rows[0]["DYB"].ToString()) ? "0" : dt.Rows[0]["DYB"].ToString();
                txtyhbh1.Text = string.IsNullOrWhiteSpace(dt.Rows[0]["DYHBH"].ToString()) ? "0" : dt.Rows[0]["DYHBH"].ToString();
                txtbjl1.Text = string.IsNullOrWhiteSpace(dt.Rows[0]["DBJL"].ToString()) ? "0" : dt.Rows[0]["DBJL"].ToString();
                txtsql1.Text = string.IsNullOrWhiteSpace(dt.Rows[0]["DSQL"].ToString()) ? "0" : dt.Rows[0]["DSQL"].ToString();
                txtxr1.Text = string.IsNullOrWhiteSpace(dt.Rows[0]["DXR"].ToString()) ? "0" : dt.Rows[0]["DXR"].ToString();
                txtglybh1.Text = string.IsNullOrWhiteSpace(dt.Rows[0]["DGLYBH"].ToString()) ? "0" : dt.Rows[0]["DGLYBH"].ToString();



                button2.Enabled = true;
                button4.Enabled = true;
                button6.Enabled = true;
                button5.Enabled = true;
            }
        }

        /// <summary>
        /// 补卡
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
            button2.Enabled = false;
            int flag2 = com.pd_gsk();
            if (flag2 == 0)
            {
                MessageBox.Show("非公司卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button2.Enabled = true;
                return;
            }
            //0未查过1插过
            string msg = "";
            string sbbh = txtsbbh.Text;
            string bk = "1";
            //if (string.IsNullOrWhiteSpace(cbbbk.Text))
            //{
            //    MessageBox.Show("请选择上次充值后是否插过水表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    button2.Enabled = true;
            //    return;
            //}
            //if (cbbbk.Text == "插过")
            //    bk = "1";
            string carduid = com.get_UID(out msg);
            if (carduid == "-1")
            {
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button2.Enabled = true;
                return;
            }
            string result = (db.Scalar("select count(*) from MerInfo where ShuiCardNum='" + carduid + "'") ?? "0").ToString();
            if (int.Parse(result) > 0)
            {
                MessageBox.Show("该卡正在使用中，请换张卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button2.Enabled = true;
                return;
            }
            int flag = com.buka(MerNum.PadLeft(12, '0'), sbbh.PadLeft(12, '0'), txtzgl.Text, txtbjl.Text, txttzl.Text, txtjfcs.Text, bk, out msg);
            if (flag != 0)
            {
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                db.NonQuery(" update MerInfo set ShuiCardNum='" + carduid + "',isShuiCardLost=1 where MerNum='" + MerNum + "'");
                db.NonQuery("insert into U_AuditLog(OpeType,UserID,MerNum,CarType,CardNum,MerName) values('补卡','" + Common.UserId + "','" + MerNum + "',0,'" + carduid + "','" + GetMerName(MerNum) + "')");
                Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtUserName.Text + "水卡补卡");
                MessageBox.Show("补卡成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            button2.Enabled = true;
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
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserInfoChangeCard_Load(object sender, EventArgs e)
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
            txtDianPrice.Text = GetDianPrice();
        }

        /// <summary>
        /// 电卡补卡按钮事件
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
            button4.Enabled = false;
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            DataTable dt = db.ExecuteDataTable("select * from MerAllInfo_View where MerNum='" + MerNum + "'");
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("此商户还没开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button4.Enabled = true;
                return;
            }
            string dbbh = txtdbbh.Text;
            string jfcs = txtdbjfcs.Text;
            //if (dbcobjfcs.Text == "插过")
            //  jfcs = (int.Parse(jfcs) + 1).ToString();
            if (string.IsNullOrWhiteSpace(dbbh) || dbbh == "0")
            {
                MessageBox.Show("电表编号不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button4.Enabled = true;
                return;
            }
            //if (string.IsNullOrWhiteSpace(dbcobjfcs.Text))
            //{
            //    MessageBox.Show("请选择上次充值后是否插过电表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    button4.Enabled = true;
            //    return;
            //}
            if (IsExitDianCar(dbbh))
            {
                MessageBox.Show("电表编号重复", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button4.Enabled = true;
                return;
            }
            string dl = "0", sql = "", msg = "";
            int result = 0;
            //if (!string.IsNullOrWhiteSpace(nudDianBuyMoney.Value.ToString()) && nudDianBuyMoney.Value != 0)
            //{
            //    txtDianTotal.Text = (float.Parse(nudDianBuyMoney.Value.ToString()) / float.Parse(txtDianPrice.Text)).ToString("0");
            //    dl = txtDianTotal.Text;
            //}
            int flag = com.dian_buka(dbbh, dl, jfcs, txtbjl1.Text, txttjl1.Text, txtsql1.Text, txtyhbh1.Text, txtyb1.Text, txtglybh1.Text, out msg);
            if (flag == 0)
            {
                db.NonQuery(" update MerInfo set DianCardNum='" + dbbh + "',isShuiCardLost=1,DGDL=0,GouDianNum='" + jfcs + "' where MerNum='" + MerNum + "' ");
                //  db.NonQuery("insert into U_AuditLog(OpeType,UserID,MerNum,CarType) values('补卡','" + Common.UserId + "','" + MerNum + "',1)");
                Common.WriteOpearateLog(Common.UserId, "商户" + MerNum + txtUserName.Text + "电卡补卡");
                MessageBox.Show("电卡补卡成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button4.Enabled = true;
                return;
            }
            else
            {
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            button4.Enabled = true;
        }

        public string GetDianPrice()
        {
            string sql = " select top 1 DianPrice from Price";
            return (db.Scalar(sql) ?? "0").ToString();
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
            if (int.Parse(result) > 1)
                state = true;
            return state;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            int flag = com.geshihua();
            if (flag != 0)
            {
                MessageBox.Show("格式化卡失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("格式化卡成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            button6.Enabled = true;
        }
        /// <summary>
        /// 擦除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            int flag = com.cachu();
            if (flag != 0)
            {
                MessageBox.Show("擦除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("擦除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            button5.Enabled = true;
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
        /// 电卡接入读卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            string msg = "", carduid = "", buycount = "";
            string cs, id, glybh, sq, tjl, xr, dbbh, yb, gdl, bjl;
            int flag = com.readusercard_all(out cs, out id, out glybh, out sq, out tjl, out xr, out dbbh, out yb, out gdl, out bjl);
            carduid = carduid.PadLeft(6, '0');
            if (flag != 0)
            {
                txtdbbh2.Text = "";
                txtjfcs2.Text = "";
                MessageBox.Show("读卡失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                txtdbbh2.Text = dbbh.PadLeft(6, '0');
                txtjfcs2.Text = cs;
                txtDgdl.Text = gdl;
                txtdtjl.Text = tjl;
                txtdyb.Text = yb;
                txtdyhbh.Text = id.PadLeft(6, '0');
                txtdbjl.Text = bjl;
                txtdsql.Text = sq;
                txtdxr.Text = xr;
                txtdglybh.Text = glybh.PadLeft(6, '0');
            }
        }

        /// <summary>
        /// 电卡接入保存
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
            button8.Enabled = false;
            string MerNum = one + two + three + four;//txtMerNum.Text.Trim();
            DataTable dt = db.ExecuteDataTable("select * from MerAllInfo_View where MerNum='" + MerNum + "'");
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("此商户还没开户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button8.Enabled = true;
                return;
            }
            string dbbh = txtdbbh2.Text;
            string jfcs = txtjfcs2.Text;
            if (string.IsNullOrWhiteSpace(dbbh))
            {
                MessageBox.Show("电表编号不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button8.Enabled = true;
                return;
            }
            if (IsExitDianCar(dbbh))
            {
                MessageBox.Show("电表编号重复", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button8.Enabled = true;
                return;
            }
            string gdl = txtDgdl.Text;
            string tjl = txtdtjl.Text;
            string yb = txtdyb.Text;
            string id = txtdyhbh.Text;
            string bjl = txtdbjl.Text;
            string sq = txtdsql.Text;
            string xr = txtdxr.Text;
            string glybh = txtdglybh.Text;
            string result = (db.Scalar("select DianCardNum from MerInfo where MerNum='" + MerNum + "'") ?? "0").ToString();
            if (!string.IsNullOrWhiteSpace(result) && result != "0")
            {
                if (DialogResult.Yes == MessageBox.Show("该商户已绑定电卡,是否继续", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    result = (db.Scalar("select count(*) from MerInfo where DianCardNum='" + dbbh + "'") ?? "0").ToString();
                    if (result == "1")
                    {
                        MessageBox.Show("电卡号重复", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        db.NonQuery(" update MerInfo set DianCardNum='" + dbbh + "', isShuiCardLost=1,GouDianNum='" + jfcs + "',DBJL='" + bjl + "',DGDL='" + gdl + "',DGLYBH='" + glybh + "',DYHBH='" + id + "',DSQL='" + sq + "',DTJL='" + tjl + "',DXR='" + xr + "',DYB='" + yb + "' where MerNum='" + MerNum + "' ");
                        MessageBox.Show("电卡接入成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                result = (db.Scalar("select count(*) from MerInfo where DianCardNum='" + dbbh + "'") ?? "0").ToString();
                if (result == "1")
                {
                    MessageBox.Show("电卡号重复", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    db.NonQuery(" update MerInfo set DianCardNum='" + dbbh + "',isShuiCardLost=1,GouDianNum='" + jfcs + "',DBJL='" + bjl + "',DGDL='" + gdl + "',DGLYBH='" + glybh + "',DYHBH='" + id + "',DSQL='" + sq + "',DTJL='" + tjl + "',DXR='" + xr + "',DYB='" + yb + "' where MerNum='" + MerNum + "' ");
                    MessageBox.Show("电卡接入成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            button8.Enabled = true;
        }

        /// <summary>
        /// 市场卡补卡
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
                MessageBox.Show("非商户卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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
            button9.Enabled = false;
            int flag2 = com.pd_gsk();
            if (flag2 == 0)
            {
                MessageBox.Show("非公司卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button9.Enabled = true;
                return;
            }
            //0未查过1插过
            string msg = "";
            string sbbh = txtsbbh.Text;
            string bk = "1";
            string carduid = com.get_UID(out msg);
            if (carduid == "-1")
            {
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button9.Enabled = true;
                return;
            }
            string result = (db.Scalar("select count(*) from MerInfo where ShuiCardNum='" + carduid + "'") ?? "0").ToString();
            if (int.Parse(result) > 0)
            {
                MessageBox.Show("该卡正在使用中，请换张卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button9.Enabled = true;
                return;
            }

            db.NonQuery(" update MerInfo set ShuiCardNum='" + carduid + "',isShuiCardLost=1 where MerNum='" + MerNum + "'");
            db.NonQuery("insert into U_AuditLog(OpeType,UserID,MerNum,CarType,CardNum,MerName) values('补卡','" + Common.UserId + "','" + MerNum + "',2,'" + carduid + "','" + GetMerName(MerNum) + "')");
            MessageBox.Show("补卡成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button9.Enabled = true;
        }
    }
}
