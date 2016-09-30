using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using XK.NBear.Common;
using XK.NBear.DB;
using System.Data.Common;
using System.IO;

namespace UserManager
{
    public partial class SetDatabase : Form
    {
        public SetDatabase()
        {
            InitializeComponent();
        }

        private void SetDatabase_Load(object sender, EventArgs e)
        {
            ReadDataParam();
        }
        private void ReadDataParam()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(Application.StartupPath + @"\DataParam.xml");
                XmlNodeList list = document.SelectNodes("Root/Database");

                for (int i = 0; i < list.Count; i++)
                {
                    XmlNode node = list[i];
                    comboBox1.Items.Add(node.Attributes["Name"].Value);
                    comboBox1.SelectedIndex = 0;
                    //txtDatasource.Text = XinDES.Decrypt(node.Attributes["DataSource"].Value);
                    //txtDataName.Text = XinDES.Decrypt(node.Attributes["DataName"].Value);
                    //txtUserName.Text = XinDES.Decrypt(node.Attributes["User"].Value);
                    //txtPwd.Text = XinDES.Decrypt(node.Attributes["Password"].Value);
                }
            }
            catch
            {
                MessageBox.Show("参数读取失败!", "友情提示");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.txtDatasource.Text.Trim() == "")
            {
                MessageBox.Show("请输入服务器地址！", "友情提示");
                this.txtDatasource.Focus();
            }
            else if (this.txtDataName.Text.Trim() == "")
            {
                MessageBox.Show("请输入数据库名！", "友情提示");
                this.txtDataName.Focus();
            }
            else if (this.txtUserName.Text.Trim() == "")
            {
                MessageBox.Show("请输入数据库登录名！", "友情提示");
                this.txtUserName.Focus();
            }
            else if (this.txtPwd.Text.Trim() == "")
            {
                MessageBox.Show("请输入数据库名密码！", "友情提示");
                this.txtPwd.Focus();
            }
            else
            {
                try
                {
                    string dataCode = comboBox1.Text.ToUpper().Trim();
                    XmlDocument document = new XmlDocument();
                    document.Load(Application.StartupPath + @"\DataParam.xml");
                        XmlNodeList list = document.SelectNodes("Root/Database");
                        for (int i = 0; i < list.Count; i++)
                        {
                            XmlNode node = list[i];
                            if (node.Attributes["Name"].Value.ToUpper().Trim() == dataCode)
                            {
                                node.Attributes["DataSource"].Value = XinDES.Encrypt(txtDatasource.Text.ToString().Trim());
                                node.Attributes["DataName"].Value = XinDES.Encrypt(txtDataName.Text.ToString().Trim());
                                node.Attributes["User"].Value = XinDES.Encrypt(txtUserName.Text.ToString().Trim());
                                node.Attributes["Password"].Value = XinDES.Encrypt(txtPwd.Text.ToString().Trim());
                                break;

                            }
                        }
  
                    document.Save(Application.StartupPath + @"\DataParam.xml");
                    document = null;
                    string path = AppDomain.CurrentDomain.BaseDirectory + "versin.bin";
                    if (!File.Exists(path))
                    {
                        FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    }
                    MessageBox.Show("设置完成!", "友情提示");
                    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    Application.ExitThread();
                }
                catch
                {
                    MessageBox.Show("设置失败!", "友情提示");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPwd.PasswordChar = (char)0;
            }
            else {
                txtPwd.PasswordChar = '*';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string conStr = "server='" + txtDatasource.Text.ToString().Trim() + "';User ID='" + txtUserName.Text.ToString().Trim() + "';Password='" + txtPwd.Text.ToString().Trim() + "';database='" + txtDataName.Text.ToString().Trim() + "';";
           // string conStr = "Data Source='" + txtDatasource.Text.ToString().Trim() + "';User ID='" + txtUserName.Text.ToString().Trim() + "';Password='" + txtPwd.Text.ToString().Trim() + "';lnitial Catalog='" + txtDataName.Text.ToString().Trim() + "';";
          
            try
            {
                IDO db = DatabaseFactory.CreateOperation(conStr, "System.Data.SqlClient");
                DbConnection con = db.CreateConnection();
                con.Open();
                if (con.State.ToString().ToLower().Trim() != "open")
                {
                    MessageBox.Show("连接失败");
                }
                else
                {
                    MessageBox.Show("连接成功");
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dataCode = comboBox1.Text.ToUpper().Trim();
            XmlDocument document = new XmlDocument();
            document.Load(Application.StartupPath + @"\DataParam.xml");
            XmlNodeList list = document.SelectNodes("Root/Database");

            for (int i = 0; i < list.Count; i++)
            {
                XmlNode node = list[i];
                if (node.Attributes["Name"].Value.ToUpper().Trim() == dataCode)
                {
                    txtDatasource.Text = XinDES.Decrypt(node.Attributes["DataSource"].Value);
                    txtDataName.Text = XinDES.Decrypt(node.Attributes["DataName"].Value);
                    txtUserName.Text = XinDES.Decrypt(node.Attributes["User"].Value);
                    txtPwd.Text = XinDES.Decrypt(node.Attributes["Password"].Value);
                }
            }
        }
    }
}
