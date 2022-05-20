using System;

namespace ApiServer.Models
{
    /// <summary>
    /// 分页查询审批数据用
    /// </summary>
    public class ShenPiSJ
    {
        public string aPage { get; set; }
        public string aTargetType { get; set; }
        public string aUserCode { get; set; }
        public DateTime aBegDate { get; set; }
        public DateTime aEndDate { get; set; }
        public int aDataType { get; set; }
        public int aGroupCode { get; set; }
    }
}