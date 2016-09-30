using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace UserManager
{
    public class jk
    {

        #region 水控


        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="CardType"></param>
        /// <param name="Dest"></param>
        /// <returns></returns>
        [DllImport("CSDataIntf.dll", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadCard(ref  int CardType, [MarshalAs(UnmanagedType.LPArray)] byte[] Dest);



        /// <summary>
        /// 读卡-err
        /// </summary>
        /// <param name="CardType"></param>
        /// <param name="Dest"></param>
        /// <returns></returns>
        [DllImport("CSDataIntf.dll", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadCard(ref  int CardType, int Dest);


        /// <summary>
        /// 写用户卡
        /// </summary>
        /// <param name="Src"></param>
        /// <param name="Action"></param>
        /// <returns></returns>
        [DllImport("CSDataIntf.dll", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteUserCard([MarshalAs(UnmanagedType.LPArray)] byte[] Src, int Action);

        /// <summary>
        /// 写功能卡
        /// </summary>
        /// <param name="CardType"></param>
        /// <param name="Src"></param>
        /// <returns></returns>
        [DllImport("CSDataIntf.dll")]
        public static extern int WriteToolCard(int CardType, [MarshalAs(UnmanagedType.LPArray)] byte[] Src);

        #endregion


        #region 读卡器

        /// <summary>
        /// 连接设备-读卡器
        /// </summary>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll")]
        public static extern int Open_USB();
        /// <summary>
        /// 断开设备-读卡器
        /// </summary>
        /// <param name="icdev"></param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll")]
        public static extern int Close_USB(int icdev);


        /// <summary>
        /// 寻卡,返回卡序列号-读卡器
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="mode"></param>
        /// <param name="Snr"></param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_card")]
        public static extern Int16 rf_card(int icdev, int mode, out uint Snr);


        /// <summary>
        /// 结束操作
        /// </summary>
        /// <param name="icdev"></param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll")]
        public static extern int rf_halt(int icdev);

        /// <summary>
        /// 蜂鸣器
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_beep", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern int rf_beep(int icdev, int version);


        /// <summary>
        /// 验证某一扇区秘钥
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="_Mode"></param>
        /// <param name="_SecNr"></param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll")]
        public static extern short rf_authentication(int icdev, int _Mode, int _SecNr);

        /// <summary>
        /// 将密码装入读写模块RAM中
        /// </summary>
        /// <param name="icdev">设备id</param>
        /// <param name="_Mode">装入密码模式(对于M1卡的每个扇区，在读写器中只有一套密码(密码A和密码B)，动态库为了和RF-25兼容，
        /// <para>仍对应三套密码（KEYSET0、KEYSET1、KEYSET2），每套密码包括A密码（KEYA）和B密码（KEYB），</para>
        /// <para>共六个密码，用0～2、4～6来表示这六个密码：0——KEYSET0的KEYA`4——KEYSET0的KEYB)</para>
        /// </param>
        /// <param name="_SecNr">扇区号(0-15)</param>
        /// <param name="NKey">密码</param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll")]
        public static extern short rf_load_key(int icdev, int _Mode, int _SecNr, string NKey);


        /// <summary>
        /// 修改扇区密码
        /// </summary>
        /// <param name="icdev">设备id</param>
        /// <param name="SecNr">扇区号(0-15)</param>
        /// <param name="KeyA">密码a</param>
        /// <param name="B0">0x00</param>
        /// <param name="B1">0x00</param>
        /// <param name="B2">0x00</param>
        /// <param name="B3">0x01</param>
        /// <param name="Bk">0</param>
        /// <param name="KeyB">密码b,没有就写12个F</param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll")]
        public static extern Int16 rf_changeb3(int icdev, int SecNr, byte[] KeyA, byte B0, byte B1, byte B2, byte B3, byte Bk, byte[] KeyB);

        /// <summary>
        /// 防卡冲突
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="Bcnt"></param>
        /// <param name="Snr"></param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll")]
        public static extern Int16 rf_anticoll(int icdev, int Bcnt, ref int Snr);


        /// <summary>
        /// 将密码装入读写模块RAM中
        /// </summary>
        /// <param name="icdev">设备id</param>
        /// <param name="_Mode">装入密码模式(对于M1卡的每个扇区，在读写器中只有一套密码(密码A和密码B)，动态库为了和RF-25兼容，
        /// <para>仍对应三套密码（KEYSET0、KEYSET1、KEYSET2），每套密码包括A密码（KEYA）和B密码（KEYB），</para>
        /// <para>共六个密码，用0～2、4～6来表示这六个密码：0——KEYSET0的KEYA`4——KEYSET0的KEYB)</para>
        /// </param>
        /// <param name="_SecNr">扇区号(0-15)</param>
        /// <param name="NKey">密码</param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll")]
        public static extern short rf_load_key_hex(int icdev, int _Mode, int _SecNr, string NKey);

        [DllImport("mwhrf_bj.dll", EntryPoint = "a_hex", SetLastError = true,
            CharSet = CharSet.Auto, ExactSpelling = false,
            CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern Int16 a_hex([MarshalAs(UnmanagedType.LPArray)]byte[] asc, [MarshalAs(UnmanagedType.LPArray)]byte[] hex, int len);

        #endregion

        #region 电卡

        /// <summary>
        /// 初始化读卡器
        /// </summary>
        /// <returns>返回>0正确</returns>
        [DllImport("ICCard.dll")]
        public static extern int InitCard();


        /// <summary>
        /// 关闭读卡器
        /// </summary>
        /// <param name="nDev">设备号</param>
        /// <returns>小于0 失败 ; </returns>
        [DllImport("ICCard.dll")]
        public static extern int ExitCard(int nDev);

        /// <summary>
        /// 擦除卡片
        /// </summary>
        /// <returns>返回=0正确,返回小于0错误</returns>
        [DllImport("ICCard.dll")]
        public static extern short ClearCard(int nDev);



        /// <summary>
        /// 售电
        /// </summary>
        /// <param name="nDev">设备id</param>
        /// <param name="nVol">售电度数</param>
        /// <returns>返回=0正确,返回小于0错误</returns>
        [DllImport("ICCard.dll")]
        public static extern short AddVol(int nDev, uint nVol);


        /// <summary>
        /// 预置卡
        /// </summary>
        /// <param name="nDev">设备id</param>
        /// <param name="nPostCode">邮编(系统注册码)</param>
        /// <param name="nAdmin">管理员编号</param>
        /// <param name="nWarnVol">报警电量</param>
        /// <param name="nLimit">囤积电量</param>
        /// <param name="nVol">预置电量</param>
        /// <returns>返回=0正确,返回小于0错误</returns>
        [DllImport("ICCard.dll")]
        public static extern short MakePresetCard(int nDev, uint nPostCode, uint nAdmin, uint nWarnVol, uint nLimit, uint nVol);




        /// <summary>
        /// 查电卡
        /// </summary>
        /// <param name="nDev">设备id</param>
        /// <param name="nPostCode">邮编(系统注册码)</param>
        /// <param name="nAdmin">管理员编号</param>
        /// <returns>返回=0正确,返回小于0错误</returns>
        [DllImport("ICCard.dll")]
        public static extern short MakeQueryCard(int nDev, uint nPostCode, uint nAdmin);


        /// <summary>
        /// 蜂鸣
        /// </summary>
        /// <param name="nDev">设备id</param>
        /// <param name="nPostCode">邮编(系统注册码)</param>
        /// <param name="nAdmin">管理员编号</param>
        /// <returns>返回=0正确,返回小于0错误</returns>
        [DllImport("Mwic_32.dll")]
        public static extern short dv_beep(int nDev, int time);





        /// <summary>
        /// 开户卡
        /// </summary>
        /// <param name="nDev">设备id</param>
        /// <param name="nMeterID">电表编号</param>
        /// <param name="nVol">电量</param>
        /// <param name="nBuyCount">购电次数</param>
        /// <param name="nWarnVol">报警电量</param>
        /// <param name="nLimit">囤积电量</param>
        /// <param name="nDuty">赊欠限量</param>
        /// <param name="nCardType">卡类型03 补卡 01 新开 02 正常</param>
        /// <param name="nUserID">用户编号</param>
        /// <param name="nPostCode">邮编(系统注册码)</param>
        /// <param name="nAdmin">管理员编号</param>
        /// <returns>返回=0正确,返回小于0错误</returns>
        [DllImport("ICCard.dll")]
        public static extern short MakeUserCard(int nDev, uint nMeterID, uint nVol, uint nBuyCount, uint nWarnVol, uint nLimit, uint nDuty, uint nCardType, uint nUserID, uint nPostCode, uint nAdmin);



        /// <summary>
        /// 格式化白卡
        /// <param name="nDev">设备id</param>
        /// <param name="nPass">密码</param>
        /// </summary>
        [DllImport("ICCard.dll")]
        public static extern short FormatCard(int nDev, uint nPass);




        /// <summary>
        /// 读取用户卡
        /// <param name="nDev">设备id</param>
        /// </summary>
        [DllImport("ICCard.dll")]
        public static extern short ReadUserCard(int nDev, out usercard pCard);


        #endregion
    }
}
