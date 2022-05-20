using System;
using System.Configuration;
using System.Xml;
using ApiServer.Common;
using ApiServer.DTO;
using ApiServer.Models;
using ApiServer.Server.Interface;

namespace ApiServer.Server
{
    public class ShouJiSPManager : IShouJiSPManager
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public ResultInfo<RowEntity> Login(string ausercode, string apwd, string amac)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                string baseUrl = ConfigurationManager.AppSettings["RemoteURL"].ToString();
                RowEntity param = new RowEntity();
                param.Add("aUserCode", ausercode);
                param.Add("aPwd", apwd);
                param.Add("aMAC", amac);
                result = CommHepler.HttpPost<ResultInfo<RowEntity>>(baseUrl + "MobileLogon", param);
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 分页查询审批数据列表信息
        /// </summary>
        /// <param name="shenPiSJ">审批数据请求实体</param>
        /// <returns></returns>
        public ResultInfo<RowEntity> GetTaskNodeLstByPage(ShenPiSJ shenPiSJ)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                string baseUrl = ConfigurationManager.AppSettings["RemoteURL"].ToString();
                RowEntity param = new RowEntity();
                param.Add("aBegDate", shenPiSJ.aBegDate);
                param.Add("aDataType", shenPiSJ.aDataType);
                param.Add("aEndDate", shenPiSJ.aEndDate);
                param.Add("aGroupCode", shenPiSJ.aGroupCode);
                param.Add("aPage", shenPiSJ.aPage);
                param.Add("aTargetType", shenPiSJ.aTargetType);
                param.Add("aUserCode", shenPiSJ.aUserCode);
                result.Data = CommHepler.HttpPost<RowEntity>(baseUrl + "MobileGetTaskNodeLstByPage", param);
                result.Code = 200;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询单笔审批单审批流程图
        /// </summary>
        /// <param name="ausercode">用户代码</param>
        /// <param name="ataskid">流程编号</param>
        /// <returns></returns>
        public ResultInfo<RowEntity> MobileSingleTaskNodeTree(string ausercode, string ataskid)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                XmlDocument doc = new XmlDocument();
                string baseUrl = ConfigurationManager.AppSettings["RemoteURL"].ToString();
                RowEntity param = new RowEntity();
                param.Add("aTaskId", ataskid);
                param.Add("aUserCode", ausercode);
                result.Data = CommHepler.HttpPost<RowEntity>(baseUrl + "MobileSingleTaskNodeTree", param);
                result.Code = 200;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询审批日志
        /// </summary>
        /// <param name="ausercode">用户代码</param>
        /// <param name="ataskid">流程编号</param>
        /// <returns></returns>
        public ResultInfo<RowEntity> MobileSingleTraceLog(string ausercode, string ataskid)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                string baseUrl = ConfigurationManager.AppSettings["RemoteURL"].ToString();
                RowEntity param = new RowEntity();
                param.Add("aTaskId", ataskid);
                param.Add("aUserCode", ausercode);
                result.Data = CommHepler.HttpPost<RowEntity>(baseUrl + "MobileSingleTraceLog", param);
                result.Code = 200;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询限额检查结果
        /// </summary>
        /// <param name="ausercode">用户编码</param>
        /// <param name="ataskid">流程编号</param>
        /// <param name="aisincludeconfirmed"></param>
        /// <param name="aisincludeordered"></param>
        /// <returns></returns>
        public ResultInfo<RowEntity> MobileSingleLimitresult(string ausercode, string ataskid, string aisincludeconfirmed, string aisincludeordered)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                string baseUrl = ConfigurationManager.AppSettings["RemoteURL"].ToString();
                RowEntity param = new RowEntity();
                param.Add("aTaskId", ataskid);
                param.Add("aUserCode", ausercode);
                param.Add("aIsIncludeConfirmed", aisincludeconfirmed);
                param.Add("aIsIncludeOrdered", aisincludeordered);
                result.Data = CommHepler.HttpPost<RowEntity>(baseUrl + "MobileSingleLimitresult", param);
                result.Code = 200;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询交易指标
        /// </summary>
        /// <param name="ausercode">用户编码</param>
        /// <param name="ataskid">流程编号</param>
        /// <returns></returns>
        public ResultInfo<RowEntity> MobileSingleTradeTarget(string ausercode, string ataskid)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                string baseUrl = ConfigurationManager.AppSettings["RemoteURL"].ToString();
                RowEntity param = new RowEntity();
                param.Add("aTaskId", ataskid);
                param.Add("aUserCode", ausercode);
                result.Data = CommHepler.HttpPost<RowEntity>(baseUrl + "MobileSingleTradeTarget", param);
                result.Code = 200;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询单笔交易详情
        /// </summary>
        /// <param name="ausercode">用户编码</param>
        /// <param name="asysordid">交易编号</param>
        /// <returns></returns>
        public ResultInfo<RowEntity> MobileSingleTradeDetailNew(string ausercode, string asysordid)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                string baseUrl = ConfigurationManager.AppSettings["RemoteURL"].ToString();
                RowEntity param = new RowEntity();
                param.Add("aSysOrdId", asysordid);
                param.Add("aUserCode", ausercode);
                result.Data = CommHepler.HttpPost<RowEntity>(baseUrl + "MobileSingleTradeDetailNew", param);
                result.Code = 200;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 批量审批前检查
        /// </summary>
        /// <param name="anodeids"></param>
        /// <param name="targettype"></param>
        /// <param name="ausercode"></param>
        /// <param name="adatatypecurrent"></param>
        /// <returns></returns>
        public ResultInfo<RowEntity> MobileCheckBeforeActionByNodeId_Batch(string anodeids, string targettype, string ausercode, string adatatypecurrent)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                string baseUrl = ConfigurationManager.AppSettings["RemoteURL"].ToString();
                RowEntity param = new RowEntity();
                param.Add("aNodeIds", anodeids);
                param.Add("aUserCode", ausercode);
                param.Add("targetType", targettype);
                param.Add("aDataTypeCurrent", adatatypecurrent);
                result.Data = CommHepler.HttpPost<RowEntity>(baseUrl + "MobileCheckBeforeActionByNodeId_Batch", param);
                result.Code = 200;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 批量审批操作
        /// </summary>
        /// <param name="piLiangSP">批量审批请求实体</param>
        /// <returns></returns>
        public ResultInfo<RowEntity> MobileApproveTaskByNodeId_Batch(PiLiangSP piLiangSP)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                string baseUrl = ConfigurationManager.AppSettings["RemoteURL"].ToString();
                RowEntity param = new RowEntity();
                param.Add("aAction", piLiangSP.aAction);
                param.Add("aCCRoles", piLiangSP.aCCRoles);
                param.Add("actionNote", piLiangSP.actionNote);
                param.Add("aDataTypeCurrent", piLiangSP.aDataTypeCurrent);
                param.Add("aNodeIds", piLiangSP.aNodeIds);
                param.Add("aUserCode", piLiangSP.aUserCode);
                param.Add("targetType", piLiangSP.targetType);
                result.Data = CommHepler.HttpPost<RowEntity>(baseUrl + "MobileApproveTaskByNodeId_Batch", param);
                result.Code = 200;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询审批数据分组信息
        /// </summary>
        /// <param name="shenPiFZ">审批分组请求实体</param>
        /// <returns></returns>
        public ResultInfo<RowEntity> MobileGetTaskNodeGroup(ShenPiFZ shenPiFZ)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                string baseUrl = ConfigurationManager.AppSettings["RemoteURL"].ToString();
                RowEntity param = new RowEntity();
                param.Add("aBeg_date", shenPiFZ.aBeg_date);
                param.Add("aDataType", shenPiFZ.aDataType);
                param.Add("aEnd_date", shenPiFZ.aEnd_date);
                param.Add("aTargetType", shenPiFZ.aTargetType);
                param.Add("aUserCode", shenPiFZ.aUserCode);
                result.Data = CommHepler.HttpPost<RowEntity>(baseUrl + "MobileGetTaskNodeGroup", param);
                result.Code = 200;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
    }
}