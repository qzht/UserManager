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
    public partial class QueryMerInfo : Temp
    {
        public QueryMerInfo()
        {
            InitializeComponent();
        }

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
            string MerNum = "";//txtMerNum.Text.Trim();
            if (one == "-1")
            {
                //  MerNum += "_";
                MessageBox.Show("请选择商户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MerNum += one;
            }
            if (two == "-1")
            {
                //  MerNum += "_";
                MessageBox.Show("请选择商户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MerNum += two;
            }
            if (three == "-1")
            {
                // MerNum += "_";
                MessageBox.Show("请选择商户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MerNum += three;
            }
            if (four == "-1")
            {
                //  MerNum += "_";
                MessageBox.Show("请选择商户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MerNum += four;
            }
            if (MerNum == "____")
            {
                MessageBox.Show("请选择商户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //string sql = " select mernum 商户编号,zjrxm 姓名, phonenum 手机号,zjhm 身份证号,bankcardno 银行卡号,shuicardnum 水卡号,diancardnum 电卡号,"
            //           + "carnianfei 车辆年费,fangzu 房租费用,  yue 余额,case  when isshuicardlost='0' then '挂失' else '正常' end 卡状态  from  dbo.MerAllInfo_View where 1=1";

            //   string sql = " select zjrxm 姓名, case when tradetype=1 then '水费' when tradetype=2 then '电费' when tradetype=3 then '车辆年费'when tradetype=4 then '房租'when tradetype=5 then '余额'  else '其他' end   类型,"
            //    + " createdate 时间, cast(cast(transmoney as decimal(10,2))/100  as  decimal(10,2)) 金额, case when IsCash=1 then '现金' else '网络' end 缴费方式 from S_Water where RetCode='00' and MerNum='" + MerNum + "' ";

            //if (!string.IsNullOrWhiteSpace(MerNum))
            //{
            //    sql += " and mernum like '" + MerNum + "' ";
            //}

            // sql += " order by createdate desc";
            string sql = " select * from ( select zjrxm 姓名,phoneNum 电话,OpenTime 开户时间,CancelTime 销户时间 from dbo.MerAllInfo_View where MerNum='" + MerNum + "'"
                       + " union all"
                       + " select zjrxm 姓名,phoneNum 电话,OpenTime 开户时间,CancelTime 销户时间  from dbo.MerHistoryAllInfo_View where MerNum='" + MerNum + "'"
                       + " ) as a order by 开户时间 desc";

            DataTable dt = db.ExecuteDataTable(sql);
            int width = 0;
            if (dt != null && dt.Rows.Count != 0)
            {
                dataGridView1.DataSource = dt;
                for (int i = 0; i < this.dataGridView1.Columns.Count; i++)//对于DataGridView的每一个列都调整
                {
                  //  this.dataGridView1.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);//将每一列都调整为自动适应模式
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

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryMerInfo_Load(object sender, EventArgs e)
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
        }
    }
}
