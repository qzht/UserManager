using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using XK.NBear.DB;
using System.Data;
using System.IO;
using System.Xml;

namespace UserManager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //设置应用程序处理异常方式：ThreadException处理
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                string path = AppDomain.CurrentDomain.BaseDirectory + "versin.bin";
                if (File.Exists(path))
                {
                    DataTable da = db.ExecuteDataTable("SELECT * FROM UpdateForOrder");
                    string news = "", old = "";
                    //如果表中没有任何数据,则直接登录,不用更新  
                    if (da.Rows.Count < 1)
                    {
                        StartForm();
                    }
                    else
                    {
                        //如果表中的新版本和老版本一致,也不需要更新  
                        news = da.Rows[0]["New"].ToString();
                        string localXmlFile = Application.StartupPath + "\\UpdateList.xml";
                        if (!File.Exists(localXmlFile))
                        {
                            StartForm();
                        }
                        else
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.Load(localXmlFile);
                            XmlNode xmlNode = xml.SelectSingleNode("AutoUpdater/Application/Version");
                            old = xmlNode.InnerText;
                            if (news == old)
                            {
                                //设置登录成功之后关闭登录窗体 显示主窗体  
                                StartForm();
                            }
                            else
                            {
                                //如果表中的新版本和老版本不一致   则需要把老版本更新成新版本   同时启动自动更新窗口  
                                // db.NonQuery("UPDATE UpdateForOrder set Old='" + news + "'");
                                System.Diagnostics.Process.Start(Application.StartupPath + @"\AutoUpdate.exe");
                            }
                        }
                    }
                }
                else
                {
                    SetDatabase setdb = new SetDatabase();
                    setdb.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                string str = GetExceptionMsg(ex, string.Empty);
                MessageBox.Show("连接数据库失败，请检查网络", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
               // Common.LogTxt(str);
            }
        }
        private static IDO db = DatabaseFactory.CreateOperation("SYSTEM");

        private static void StartForm()
        {
            //1.这里判定是否已经有实例在运行
            //只运行一个实例
            Process instance = RunningInstance();
            if (instance == null)
            {
                //1.1 没有实例在运行
                Application.Run(new Login());
            }
            else
            {
                //1.2 已经有一个实例在运行
                HandleRunningInstance(instance);
            }
        }

        //2.在进程中查找是否已经有实例在运行
        #region 确保程序只运行一个实例
        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //遍历与当前进程名称相同的进程列表 
            foreach (Process process in processes)
            {
                //如果实例已经存在则忽略当前进程 
                if (process.Id != current.Id)
                {
                    //保证要打开的进程同已经存在的进程来自同一文件路径
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        //返回已经存在的进程
                        return process;
                    }
                }
            }
            return null;
        }
        //3.已经有了就把它激活，并将其窗口放置最前端
        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, 1); //调用api函数，正常显示窗口
            SetForegroundWindow(instance.MainWindowHandle); //将窗口放置最前端
        }
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(System.IntPtr hWnd);
        #endregion

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            Common.LogTxt(str);
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            Common.LogTxt(str);
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
        }
        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
    }
}
