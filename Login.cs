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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private IDO db = DatabaseFactory.CreateOperation("SYSTEM");
        common com = new common();
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text;
                string password = txtpassword.Text;
                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("用户名不能为空");
                    return;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("密码不能为空");
                    return;
                }

                password = XK.NBear.Common.XinDES.Encrypt(password);
                string sql = " select * from dbo.GuanLiUser where isLeave=0 and username='" + username + "' and password='" + password + "' ";
                DataTable dt = db.ExecuteDataTable(sql);
                if (dt != null && dt.Rows.Count != 0)
                {
                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    Common.LoginTime = time;

                    Common.userName = dt.Rows[0]["username"].ToString();
                    Common.realName = dt.Rows[0]["realName"].ToString();
                    Common.UserId = dt.Rows[0]["id"].ToString();
                    Common.Company = dt.Rows[0]["Company"].ToString();
                    //if (string.IsNullOrWhiteSpace(dt.Rows[0]["LoginTime"].ToString()))
                    //{
                    //    db.NonQuery(" update GuanLiUser set LoginTime='" + time + "' where id='" + dt.Rows[0]["id"].ToString() + "'");
                    //}
                    DataTable temp = db.ExecuteDataTable("select top 1 *from  dbo.RecordWork where userid='" + dt.Rows[0]["id"].ToString() + "' order by LoginTime desc");
                    if (temp != null && temp.Rows.Count != 0)
                    {
                        if (!string.IsNullOrWhiteSpace(temp.Rows[0]["UnLoginTime"].ToString()))
                        {
                            db.NonQuery(" insert into RecordWork (Userid,LoginTime,RealName,Company) values('" + dt.Rows[0]["id"].ToString() + "','" + time + "','" + Common.realName + "','" + Common.Company + "')");
                        }
                        else
                        {
                            Common.LoginTime = temp.Rows[0]["LoginTime"].ToString();
                        }
                    }
                    else
                    {
                        db.NonQuery(" insert into RecordWork (Userid,LoginTime,RealName,Company) values('" + dt.Rows[0]["id"].ToString() + "','" + time + "','" + Common.realName + "','" + Common.Company + "')");
                    }

                    //Common.LoginTime = time;
                    //    Form1 m = new Form1();
                    Common.WriteOpearateLog(Common.UserId, Common.realName + "于" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "登录系统");
                    Main m = new Main();
                    m.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("账号或密码错误");
                }
            }
            catch
            {
                MessageBox.Show("连接数据库失败，请检查网络", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "versin.bin";
            if (File.Exists(path))
            {
                // bindData();
            }
            else
            {
                SetDatabase setdb = new SetDatabase();
                setdb.ShowDialog();
            }
            try
            {
                com.InitCard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 键盘按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            // 组合键

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.B)         //Ctrl+F1
            {
                SetDatabase set = new SetDatabase();
                set.ShowDialog();
            }
            if (e.KeyCode == Keys.Enter)
            {

                string username = txtusername.Text;
                string password = txtpassword.Text;
                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("用户名不能为空");
                    return;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("密码不能为空");
                    return;
                }

                password = XK.NBear.Common.XinDES.Encrypt(password);
                string sql = " select * from dbo.GuanLiUser where isLeave=0 and username='" + username + "' and password='" + password + "' ";
                DataTable dt = db.ExecuteDataTable(sql);
                if (dt != null && dt.Rows.Count != 0)
                {
                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    Common.LoginTime = time;

                    Common.userName = dt.Rows[0]["username"].ToString();
                    Common.realName = dt.Rows[0]["realName"].ToString();
                    Common.UserId = dt.Rows[0]["id"].ToString();
                    Common.Company = dt.Rows[0]["Company"].ToString();
                    //if (string.IsNullOrWhiteSpace(dt.Rows[0]["LoginTime"].ToString()))
                    //{
                    //    db.NonQuery(" update GuanLiUser set LoginTime='" + time + "' where id='" + dt.Rows[0]["id"].ToString() + "'");
                    //}
                    DataTable temp = db.ExecuteDataTable("select top 1 *from  dbo.RecordWork where userid='" + dt.Rows[0]["id"].ToString() + "' order by LoginTime desc");
                    if (temp != null && temp.Rows.Count != 0)
                    {
                        if (!string.IsNullOrWhiteSpace(temp.Rows[0]["UnLoginTime"].ToString()))
                        {
                            db.NonQuery(" insert into RecordWork (Userid,LoginTime,RealName,Company) values('" + dt.Rows[0]["id"].ToString() + "','" + time + "','" + Common.realName + "','" + Common.Company + "')");
                        }
                        else
                        {
                            Common.LoginTime = temp.Rows[0]["LoginTime"].ToString();
                        }
                    }
                    else
                    {
                        db.NonQuery(" insert into RecordWork (Userid,LoginTime,RealName,Company) values('" + dt.Rows[0]["id"].ToString() + "','" + time + "','" + Common.realName + "','" + Common.Company + "')");
                    }

                    //Common.LoginTime = time;
                    Main m = new Main();
                    m.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("账号或密码错误");
                }
            }
        }


    }
}
