using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiServer.Models
{
    /// <summary>
    /// 查询审批数据分组信息请求实体
    /// </summary>
    public class ShenPiFZ
    {
        public string aTargetType { get; set; }

        public string aUserCode { get; set; }

        public string aBeg_date { get; set; }

        public string aEnd_date { get; set; }

        public string aDataType { get; set; }
    }
}