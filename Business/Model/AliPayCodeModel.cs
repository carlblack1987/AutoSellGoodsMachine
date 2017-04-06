#region [ KIMMA Co.,Ltd. Copyright (C) 2014 ]
//-------------------------------------------------------------------------------------
// 开发单位：湖南金码智能设备制造有限公司
// 业务模块：iVend终端软件业务处理类
// 业务功能：条形码扫描在线支付信息模板
// 创建标识：2015-01-16		谷霖
//-------------------------------------------------------------------------------------
#endregion

namespace Business.Model
{
    public class BarCodeNetPayModel
    {
        /// <summary>
        /// 析构
        /// </summary>
        public BarCodeNetPayModel()
        {
            PayCode = string.Empty;
            PayNum = string.Empty;
            PayType = string.Empty;
            Money = 0;
            PointsMoney = string.Empty;
            CouponMoeny = string.Empty;
            InAccount = string.Empty;
            OutAccount = string.Empty;
            OrderNo = string.Empty;
            LgsFee = string.Empty;
        }

        /// <summary>
        /// 条形码
        /// </summary>
        public string PayCode { get; set; }

        /// <summary>
        /// 支付账号
        /// </summary>
        public string PayNum { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 扣款金额
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// 补助金额
        /// </summary>
        public string PointsMoney { get; set; }

        /// <summary>
        /// 抵扣金额
        /// </summary>
        public string CouponMoeny { get; set; }

        /// <summary>
        /// 接入账号
        /// </summary>
        public string InAccount { get; set; }

        /// <summary>
        /// 分账账号
        /// </summary>
        public string OutAccount { get; set; }

        /// <summary>
        /// 金码订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 费用
        /// </summary>
        public string LgsFee { get; set; }
    }
}
