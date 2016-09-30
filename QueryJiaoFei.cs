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
    public partial class QueryJiaoFei : Temp
    {
        public QueryJiaoFei()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryJiaoFei_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string sql = "select Name,Value from dbo.Dict_View where TypeName='One' order by cast(seq as int) ";
            dt = db.ExecuteDataTable(sql);
            DataRow dr = dt.NewRow();
            dr["Name"] = "请选择";
            dr["Value"] = "-1";
            if (dt != null && dt.Rows.Count != 0)
                dt.Rows.InsertAt(dr, 0);
            cbbone.ValueMember = "Value";
            cbbone.DisplayMember = "Name";
            cbbone.DataSource = dt;
            sql = "select Name,Value from dbo.Dict_View where TypeName='Two' order by cast(seq as int) ";
            dt = db.ExecuteDataTable(sql);
            dr = dt.NewRow();
            dr["Name"] = "请选择";
            dr["Value"] = "-1";
            if (dt != null && dt.Rows.Count != 0)
                dt.Rows.InsertAt(dr, 0);
            cbbtwo.ValueMember = "Value";
            cbbtwo.DisplayMember = "Name";
            cbbtwo.DataSource = dt;
            sql = "select Name,Value from dbo.Dict_View where TypeName='Three' order by cast(seq as int) ";
            dt = db.ExecuteDataTable(sql);
            dr = dt.NewRow();
            dr["Name"] = "请选择";
            dr["Value"] = "-1";
            if (dt != null && dt.Rows.Count != 0)
                dt.Rows.InsertAt(dr, 0);
            cbbthree.ValueMember = "Value";
            cbbthree.DisplayMember = "Name";
            cbbthree.DataSource = dt;
            //sql = "select Name,Value from dbo.Dict_View where TypeName='Four' order by seq ";
            //dt = db.ExecuteDataTable(sql);
            //dr = dt.NewRow();
            //dr["Name"] = "请选择";
            //dr["Value"] = "-1";
            //if (dt != null && dt.Rows.Count != 0)
            //    dt.Rows.InsertAt(dr, 0);
            //cbbfour.ValueMember = "Value";
            //cbbfour.DisplayMember = "Name";
            //cbbfour.DataSource = dt;

            cbbtype.Items.Insert(0, "全部");
            cbbtype.Items.Insert(1, "现金");
            cbbtype.Items.Insert(2, "网络");
            cbbtype.SelectedIndex = 0;

        }

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
            //if (string.IsNullOrWhiteSpace(four))
            //{
            //    MessageBox.Show("请输入门牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            string MerNum = "";//txtMerNum.Text.Trim();
            if (one == "-1")
            {
                MerNum += "_";
                //MessageBox.Show("请选择商户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }
            else
            {
                MerNum += one;
            }
            if (two == "-1")
            {
                MerNum += "_";
                //MessageBox.Show("请选择商户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }
            else
            {
                MerNum += two;
            }
            if (three == "-1")
            {
                MerNum += "_";
                //MessageBox.Show("请选择商户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }
            else
            {
                MerNum += three;
            }
            if (string.IsNullOrWhiteSpace(four))
            {
                MerNum += "_";
                //MessageBox.Show("请选择商户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }
            else
            {
                MerNum += four;
            }
            //if (MerNum == "____")
            //{
            //    MessageBox.Show("请选择商户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            string where = "";
            string phone = txtqphone.Text;
            string sfz = txtqsfz.Text;

            if (MerNum != "____")
            {
                where += " and  MerNum='" + MerNum + "' ";
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                string zjhm = (db.Scalar(" select zjhm from  dbo.MerUser where PhoneNum='" + phone + "' ") ?? "0").ToString();
                if (string.IsNullOrWhiteSpace(zjhm) || zjhm == "0")
                {
                    MessageBox.Show("无此商户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                where += " and  ZJHM='" + zjhm + "' ";
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
            string type = cbbtype.Text;

            //string sql = " select mernum 商户编号,zjrxm 姓名, phonenum 手机号,zjhm 身份证号,bankcardno 银行卡号,shuicardnum 水卡号,diancardnum 电卡号,"
            //           + "carnianfei 车辆年费,fangzu 房租费用,  yue 余额,case  when isshuicardlost='0' then '挂失' else '正常' end 卡状态  from  dbo.MerAllInfo_View where 1=1";

            string sql = " select zjrxm 姓名, case when tradetype=1 then '水费' when tradetype=2 then '电费' when tradetype=3 then '车辆年费'"
                       + " when tradetype=4 then '房租'when tradetype=5 then '余额' when tradetype=6 then '地泵扣费' when tradetype=7 then '车辆出入扣费' "
                       + " when tradetype=8 then '销户' when tradetype=10 then '广告租赁费' when tradetype=11 then '押金'  else '其他' end   类型,"
                       + " createdate 时间, cast(cast(transmoney as decimal(10,2))/100  as  decimal(10,2)) 金额, case when IsCash=1 then '现金' else '网络' end 缴费方式 from S_Water where RetCode='00' " + where + " ";
            if (type == "现金")
                sql += " and IsCash='" + 1 + "'";
            if (type == "网络")
                sql += " and IsCash='" + 0 + "'";
            //if (!string.IsNullOrWhiteSpace(MerNum))
            //{
            //    sql += " and mernum like '" + MerNum + "' ";
            //}

            sql += " order by createdate desc";

            DataTable dt = db.ExecuteDataTable(sql);
            int width = 0;
            if (dt != null && dt.Rows.Count != 0)
            {
                dataGridView1.DataSource = dt;
                for (int i = 0; i < this.dataGridView1.Columns.Count; i++)//对于DataGridView的每一个列都调整
                {
                    //this.dataGridView1.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.NotSet);//将每一列都调整为自动适应模式
                    width += this.dataGridView1.Columns[i].Width;//记录整个DataGridView的宽度
                }
                if (width > this.dataGridView1.Size.Width)//判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，则将DataGridView的列自动调整模式设置为显示的列即可，如果是小于原来设定的宽度，将模式改为填充。
                {
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                }
                else
                {
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            else
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("该商户无缴费信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
