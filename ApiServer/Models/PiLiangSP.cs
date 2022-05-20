using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiServer.Models
{
    /// <summary>
    /// 批量审批操作请求实体
    /// </summary>
    public class PiLiangSP
    {
        public string aNodeIds { get; set; }

        public string targetType { get; set; }

        public string aUserCode { get; set; }

        public string aAction { get; set; }

        public string actionNote { get; set; }

        public string aCCRoles { get; set; }

        public string aDataTypeCurrent { get; set; }
    }
}