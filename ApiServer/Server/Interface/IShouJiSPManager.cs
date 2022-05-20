using ApiServer.DTO;
using ApiServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServer.Server.Interface
{
    public interface IShouJiSPManager
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        ResultInfo<RowEntity> Login(string ausercode, string apwd, string amac);
        /// <summary>
        /// 分页查询审批数据列表信息
        /// </summary>
        /// <param name="shenPiSJ"></param>
        /// <returns></returns>
        ResultInfo<RowEntity> GetTaskNodeLstByPage(ShenPiSJ shenPiSJ);
        /// <summary>
        /// 查询单笔审批单审批流程图
        /// </summary>
        /// <param name="ausercode"></param>
        /// <param name="ataskid"></param>
        /// <returns></returns>
        ResultInfo<RowEntity> MobileSingleTaskNodeTree(string ausercode, string ataskid);
        /// <summary>
        /// 查询审批日志
        /// </summary>
        /// <param name="ausercode"></param>
        /// <param name="ataskid"></param>
        /// <returns></returns>
        ResultInfo<RowEntity> MobileSingleTraceLog(string ausercode, string ataskid);
        /// <summary>
        /// 查询限额检查结果
        /// </summary>
        /// <param name="ausercode"></param>
        /// <param name="ataskid"></param>
        /// <returns></returns>
        ResultInfo<RowEntity> MobileSingleLimitresult(string ausercode, string ataskid,string aisincludeconfirmed, string aisincludeordered);
        /// <summary>
        /// 查询交易指标
        /// </summary>
        /// <param name="ausercode"></param>
        /// <param name="ataskid"></param>
        /// <returns></returns>
        ResultInfo<RowEntity> MobileSingleTradeTarget(string ausercode, string ataskid);
        /// <summary>
        /// 查询单笔交易详情
        /// </summary>
        /// <param name="ausercode"></param>
        /// <param name="asysordid"></param>
        /// <returns></returns>
        ResultInfo<RowEntity> MobileSingleTradeDetailNew(string ausercode, string asysordid);
        /// <summary>
        /// 批量审批前检查
        /// </summary>
        /// <param name="anodeids"></param>
        /// <param name="targettype"></param>
        /// <param name="ausercode"></param>
        /// <param name="adatatypecurrent"></param>
        /// <returns></returns>
        ResultInfo<RowEntity> MobileCheckBeforeActionByNodeId_Batch(string anodeids, string targettype, string ausercode, string adatatypecurrent);
        /// <summary>
        /// 批量审批操作
        /// </summary>
        /// <param name="piLiangSP"></param>
        /// <returns></returns>
        ResultInfo<RowEntity> MobileApproveTaskByNodeId_Batch(PiLiangSP piLiangSP);

        /// <summary>
        /// 查询审批数据分组信息
        /// </summary>
        /// <param name="shenPiFZ"></param>
        /// <returns></returns>
        ResultInfo<RowEntity> MobileGetTaskNodeGroup(ShenPiFZ shenPiFZ);
    }
}
