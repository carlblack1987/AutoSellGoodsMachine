#region [ KIMMA Co.,Ltd. Copyright (C) 2016 ]
//-------------------------------------------------------------------------------------
// 开发单位：湖南金码智能设备制造有限公司
// 业务模块：iVend终端软件业务处理类
// 业务功能：在线会员实体卡读取处理类
// 创建标识：2016-12-22		谷霖
//-------------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RFIDCardEqu;
using Business.Enum;

namespace Business.Common
{
    public class OnlineEntityCardHelper
    {
        #region 变量声明

        private RFIDCARDOper m_RFIDOper = new RFIDCARDOper();

        #endregion

        #region 属性

        /// <summary>
        /// 是否记录通信日志到文件，False：不记录 True：记录
        /// </summary>
        public bool IsLogToFile
        {
            get { return m_RFIDOper.IsLogToFile; }
            set { m_RFIDOper.IsLogToFile = value; }
        }

        /// <summary>
        /// 是否记录通信日志到队列，False：不记录 True：记录
        /// </summary>
        public bool IsLogToQueue
        {
            get { return m_RFIDOper.IsLogToQueue; }
            set { m_RFIDOper.IsLogToQueue = value; }
        }

        /// <summary>
        /// 串口号
        /// </summary>
        public int ComPort
        {
            get { return m_RFIDOper.ComPort; }
            set { m_RFIDOper.ComPort = value; }
        }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate
        {
            get { return m_RFIDOper.BaudRate; }
        }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get { return m_RFIDOper.Version; }
        }
        /// <summary>
        /// 组件名称
        /// </summary>
        public string SoftName
        {
            get { return m_RFIDOper.SoftName; }
        }

        private string m_RFIDDeviceStatus = "01";
        /// <summary>
        /// 设备状态
        /// </summary>
        public string DeviceStatus
        {
            get { return m_RFIDDeviceStatus; }
        }

        #endregion

        #region 公共接口函数

        /// <summary>
        /// 初始化设备
        /// </summary>
        /// <returns></returns>
        public int InitOnlineEntityCardScan()
        {
            int intErrCode = 0;

            string strLogType = "InitOnlineEntityCardScan";

            LogHelper.AddBusLog(strLogType + "  " + "Port:" + ComPort);

            intErrCode = Initialize();

            string strStatus = string.Empty;
            if (intErrCode != 0)
            {
                strStatus = "01";
            }
            else
            {
                strStatus = "02";
            }
            m_RFIDDeviceStatus = strStatus;

            LogHelper.AddBusLog_Code(strLogType, intErrCode.ToString(), strStatus);

            return intErrCode;
        }

        /// <summary>
        /// 初始化扫描设备
        /// </summary>
        /// <returns></returns>
        private int Initialize()
        {
            m_RFIDOper.Displose();
            return m_RFIDOper.Initialize();
        }

        /////// <summary>
        /////// 检测设备状态
        /////// </summary>
        /////// <returns></returns>
        ////public int CheckDeviceStatus()
        ////{
        ////    string strLogType = "QueryOnlineEntityCardStatus";

        ////    int intErrCode = m_RFIDOper.CheckRfidCardDevice();

        ////    if (intErrCode == 0)
        ////    {
        ////        m_RFIDDeviceStatus = "02";
        ////    }
        ////    else
        ////    {
        ////        m_RFIDDeviceStatus = "01";
        ////    }

        ////    LogHelper.AddBusLog_Code(strLogType, intErrCode.ToString(), m_RFIDDeviceStatus);

        ////    return intErrCode;
        ////}

        /// <summary>
        /// 查询扫描结果
        /// </summary>
        /// <param name="_cardData"></param>
        /// <param name="_errICCode"></param>
        /// <returns></returns>
        public int QueryOnlineEntityCardNum(out string _phyNo,out string _cardNum)
        {
            _cardNum = string.Empty;
            _phyNo = string.Empty;
            string _errICCode = string.Empty;

            int intErrCode = m_RFIDOper.QueryCardNum(out _cardNum, out _errICCode);
            if (intErrCode == 0)
            {
                _phyNo = _cardNum;
                if (string.IsNullOrEmpty(_cardNum))
                {
                    intErrCode = 9;// 无卡
                }
            }
            return intErrCode;
        }

        /// <summary>
        /// 清除扫描内容
        /// </summary>
        /// <returns></returns>
        public int ClearOnlineEntityCardNum()
        {
            return m_RFIDOper.ClearCardNum();
        }

        /////// <summary>
        /////// 开始扫描
        /////// </summary>
        /////// <returns></returns>
        ////public int BeginScan()
        ////{
        ////    string strLogType = "BeginOnlineEntityCardScan";

        ////    int intErrCode = m_RFIDOper.BegingRfidCard();
        ////    ////m_RFIDOper.IsRecData = true;

        ////    LogHelper.AddBusLog_Code(strLogType, intErrCode.ToString(), "");

        ////    return intErrCode;
        ////}

        /////// <summary>
        /////// 停止扫描
        /////// </summary>
        /////// <returns></returns>
        ////public int StopScan()
        ////{
        ////    string strLogType = "StopOnlineEntityCardScan";

        ////    int intErrCode = m_RFIDOper.StopRfidCard();
        ////    ////m_RFIDOper.IsRecData = false;
        ////    LogHelper.AddBusLog_Code(strLogType, intErrCode.ToString(), "");

        ////    return intErrCode;
        ////}

        #endregion
    }
}
