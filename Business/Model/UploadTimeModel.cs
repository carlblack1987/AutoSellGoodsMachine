#region [ KIMMA Co.,Ltd. Copyright (C) 2016 ]
//-------------------------------------------------------------------------------------
// 开发单位：湖南金码智能设备制造有限公司
// 业务模块：iVend终端软件业务处理类
// 业务功能：检测更新时间模板
// 创建标识：2016-12-22		谷霖
//-------------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Model
{
    public class UploadTimeModel
    {
        /// <summary>
        /// 析构
        /// </summary>
        public UploadTimeModel()
        {
            Upload_DateTime_Time = string.Empty;
            Upload_DateTime_LastTime = string.Empty;
            Upload_DateTime_LastDate = string.Empty;
        }

        /// <summary>
        /// 定时更新时间 格式为：HHmm
        /// </summary>
        public string Upload_DateTime_Time { get; set; }

        /// <summary>
        /// 上次更新完成时间 格式为：yyyyMMddHHmm
        /// </summary>
        public string Upload_DateTime_LastTime { get; set; }

        /// <summary>
        /// 上次更新完成日期 格式为：yyyyMMdd
        /// </summary>
        public string Upload_DateTime_LastDate{ get; set; }
    }
}
