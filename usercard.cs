using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace UserManager
{
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct usercard
    {
        /// <summary>
        /// 电表id
        /// </summary>
        public uint MeterID;
        /// <summary>
        /// 购电量
        /// </summary>
        public uint Vol;
        /// <summary>
        /// 购电次数
        /// </summary>
        public uint BuyCount;
        /// <summary>
        /// 报警量
        /// </summary>
        public uint WarnVol;
        /// <summary>
        /// 囤积限量
        /// </summary>
        public uint Limit;
        /// <summary>
        /// 赊欠
        /// </summary>
        public uint Duty;
        /// <summary>
        /// 限容，0 不限
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public byte[] LimitVol;
        /// <summary>
        /// 用户ID
        /// </summary>
        [MarshalAs(UnmanagedType.U4, SizeConst = 4)]
        public uint UserID;
        /// <summary>
        /// 邮编
        /// </summary>
        public uint PostCode;
        /// <summary>
        /// 管理员编号
        /// </summary>
        public uint Admin;
    }
}
