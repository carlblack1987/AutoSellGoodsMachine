using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using Business.Enum;

namespace AutoSellGoodsMachine
{
    /// <summary>
    /// FrmDeviceCfg_BarCode.xaml 的交互逻辑
    /// </summary>
    public partial class FrmDeviceCfg_BarCode : Window
    {
        private string m_ScanModel_XDL = string.Empty;
        private string m_ScanModel_XK = string.Empty;

        /// <summary>
        /// 是否关闭窗体
        /// </summary>
        private bool blnClose = false;

        public FrmDeviceCfg_BarCode()
        {
            InitializeComponent();

            InitForm();

            Loaded += (FrmDeviceCfg_Loaded);
            Closing += (FrmDeviceCfg_Closing);
        }

        private void FrmDeviceCfg_Loaded(object sender, RoutedEventArgs e)
        {
            // 启动日志记录线程
            Thread TrdReadLog = new Thread(new ThreadStart(ReadLog));
            TrdReadLog.IsBackground = true;
            TrdReadLog.Start();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmDeviceCfg_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            blnClose = true;
        }

        private void InitForm()
        {
            #region 初始化界面

            tbTitle.Text = PubHelper.p_LangOper.GetStringBundle("SysCfg_Menu_DeviceCfg_BarCode_Title");
            tbCfgTitle.Text = PubHelper.p_LangOper.GetStringBundle("Pub_DeviceCfg");

            tbBaudRate.Text = PubHelper.p_LangOper.GetStringBundle("Pub_BaudRate");
            tbSoftVer.Text = PubHelper.p_LangOper.GetStringBundle("Pub_SoftVer");
            tbScanModel.Text = PubHelper.p_LangOper.GetStringBundle("SysCfg_Menu_DeviceCfg_BarCode_ScanModel");

            tbSerPort.Text = PubHelper.p_LangOper.GetStringBundle("SysCfg_Menu_DeviceCfg_BarCode_SerPort");
            for (int i = 1; i < 11; i++)
            {
                cmbPort.Items.Add("COM" + i.ToString());
            }
            cmbPort.Text = "COM" + PubHelper.p_BusinOper.BarCodeOper.ComPort.ToString();

            m_ScanModel_XDL = PubHelper.p_LangOper.GetStringBundle("SysCfg_Menu_DeviceCfg_BarCode_ScanModel_BarCodeXDL");
            m_ScanModel_XK = PubHelper.p_LangOper.GetStringBundle("SysCfg_Menu_DeviceCfg_BarCode_ScanModel_QRXK");
            cmbScanModel.Items.Add(m_ScanModel_XDL);
            cmbScanModel.Items.Add(m_ScanModel_XK);

            btnSave.Content = PubHelper.p_LangOper.GetStringBundle("Pub_Btn_Save");
            btnCancel.Content = PubHelper.p_LangOper.GetStringBundle("Pub_Btn_Cancel");

            tbDeviceTest.Text = PubHelper.p_LangOper.GetStringBundle("Pub_DeviceTest");

            btnOpen.Content = PubHelper.p_LangOper.GetStringBundle("SysCfg_Menu_DeviceCfg_BarCode_Open");
            btnSet.Content = PubHelper.p_LangOper.GetStringBundle("SysCfg_Menu_DeviceCfg_BarCode_Set");
            btnBeginScan.Content = PubHelper.p_LangOper.GetStringBundle("SysCfg_Menu_DeviceCfg_BarCode_BeginScan");
            btnStopScan.Content = PubHelper.p_LangOper.GetStringBundle("SysCfg_Menu_DeviceCfg_BarCode_StopScan");
            btnClearLog.Content = PubHelper.p_LangOper.GetStringBundle("Pub_Btn_ClearLog");

            #endregion

            #region 加载参数值

            tbBaudRate_Value.Text = PubHelper.p_BusinOper.BarCodeOper.BaudRate.ToString();
            tbSoftVer_Value.Text = PubHelper.p_BusinOper.BarCodeOper.SoftName;
            tbLog.Text = string.Empty;
            if (PubHelper.p_BusinOper.ConfigInfo.BarCodeScanModel == BusinessEnum.BarCodeScanModel.BarCode_XDL)
            {
                cmbScanModel.Text = m_ScanModel_XDL;
            }
            else
            {
                cmbScanModel.Text = m_ScanModel_XK;
            }
            
            #endregion

            if (PubHelper.p_BusinOper.UserType != Business.Enum.BusinessEnum.UserType.SystemUser)
            {
                // 不是厂商管理员
                cmbPort.IsEnabled = btnSave.IsEnabled = cmbScanModel.IsEnabled = false;
            }

            btnOpen.IsEnabled = btnClearLog.IsEnabled = true;
            btnSet.IsEnabled = btnBeginScan.IsEnabled = btnStopScan.IsEnabled = false;
        }

        /// <summary>
        /// 按钮—清除日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearLog_Click(object sender, RoutedEventArgs e)
        {
            tbLog.Text = string.Empty;
        }

        /// <summary>
        /// 按钮—停止扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopScan_Click(object sender, RoutedEventArgs e)
        {
            int intErrCode = PubHelper.p_BusinOper.BarCodeOper.StopScan();
            if (intErrCode == 0)
            {
                btnSet.IsEnabled = true;
                btnBeginScan.IsEnabled = btnStopScan.IsEnabled = false;
            }
        }

        /// <summary>
        /// 按钮—开始扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBeginScan_Click(object sender, RoutedEventArgs e)
        {
            int intErrCode = PubHelper.p_BusinOper.BarCodeOper.BeginScan();
            if (intErrCode == 0)
            {
                btnBeginScan.IsEnabled = btnSet.IsEnabled = false;
                btnStopScan.IsEnabled = true;
            }
        }

        /// <summary>
        /// 按钮—设置参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            int intErrCode = PubHelper.p_BusinOper.BarCodeOper.SetScanPara();
            if (intErrCode == 0)
            {
                btnBeginScan.IsEnabled = true;
                btnStopScan.IsEnabled = btnSet.IsEnabled = false;
            }
        }

        /// <summary>
        /// 按钮—打开设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            PubHelper.p_BusinOper.BarCodeOper.ComPort = Convert.ToInt32(cmbPort.Text.Replace("COM", ""));
            PubHelper.p_BusinOper.BarCodeOper.ScanModel = ConvertScanModel();
            int intErrCode = PubHelper.p_BusinOper.BarCodeOper.Initialize();
            if (intErrCode == 0)
            {
                btnOpen.IsEnabled = btnBeginScan.IsEnabled = btnStopScan.IsEnabled = false;
                btnSet.IsEnabled = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string strPort = cmbPort.Text.Replace("COM", "");

            ////if (strPort == PubHelper.p_BusinOper.BarCodeOper.ComPort.ToString())
            ////{
            ////    return;
            ////}

            string strScanModel = ConvertScanModel();

           PubHelper.p_BusinOper.UpdateSysCfgValue("BarCodeScanPort", strPort);
           PubHelper.p_BusinOper.BarCodeOper.ComPort = Convert.ToInt32(strPort);
           PubHelper.p_BusinOper.UpdateSysCfgValue("BarCodeScanModel", strScanModel);
           PubHelper.p_BusinOper.BarCodeOper.ScanModel = strScanModel;
           PubHelper.ShowMsgInfo(PubHelper.p_LangOper.GetStringBundle("Pub_OperSuc"), PubHelper.MsgType.Ok);
            ////if (result)
            ////{
            ////    // 保存成功
                
            ////}
            ////else
            ////{
            ////    PubHelper.ShowMsgInfo(PubHelper.p_LangOper.GetStringBundle("Pub_OperFail"), PubHelper.MsgType.Ok);
            ////}
        }

        private string ConvertScanModel()
        {
            string strScanModel = "0";
            if (cmbScanModel.Text == m_ScanModel_XK)
            {
                strScanModel = "1";
            }
            return strScanModel;
        }

        /// <summary>
        /// 显示日志
        /// </summary>
        private void ReadLog()
        {
            string strCardNum = string.Empty;
            string strErrCode = string.Empty;
            int intErrCode = 0;

            while (!blnClose)
            {
                Thread.Sleep(100);

                intErrCode = PubHelper.p_BusinOper.BarCodeOper.QueryBarCodeNum(out strCardNum, out strErrCode);
                if ((intErrCode == 0) && (!string.IsNullOrEmpty(strCardNum)))
                {
                    this.tbTitle.Dispatcher.Invoke(new Action(() =>
                    {
                        tbLog.AppendText(strCardNum);
                    }));
                }
            }
        }
    }
}
