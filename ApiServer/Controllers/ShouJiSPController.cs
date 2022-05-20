using System;
using System.Web.Http;
using ApiServer.DTO;
using ApiServer.Models;
using ApiServer.Server.Interface;
using Microsoft.Practices.Unity;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace ApiServer.Controllers
{
    public class ShouJiSPController : ApiController
    {
        [Dependency]
        protected IShouJiSPManager ShouJispServer { get; set; }
       
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/login")]
        public ResultInfo<RowEntity> Login(string ausercode, string apwd, string amac)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                result = ShouJispServer.Login(ausercode, apwd, amac);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 分页查询审批数据列表信息
        /// </summary>
        /// <param name="shenPiSJ"></param>
        /// <returns></returns>
        [HttpPost, Route("api/gettasknodelstbypage")]
        public ResultInfo<RowEntity> GetTaskNodeLstByPage(ShenPiSJ shenPiSJ)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                result = ShouJispServer.GetTaskNodeLstByPage(shenPiSJ);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询单笔审批单审批流程图
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/mobilesingletasknodetree")]
        public ResultInfo<RowEntity> MobileSingleTaskNodeTree(string ausercode,string ataskid)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                result = ShouJispServer.MobileSingleTaskNodeTree(ausercode, ataskid);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询审批日志
        /// </summary>
        /// <param name="ausercode"></param>
        /// <param name="ataskid"></param>
        /// <returns></returns>
        [HttpGet, Route("api/mobilesingletracelog")]
        public ResultInfo<RowEntity> MobileSingleTraceLog(string ausercode,string ataskid)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                result = ShouJispServer.MobileSingleTraceLog(ausercode, ataskid);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询限额检查结果
        /// </summary>
        /// <param name="ausercode"></param>
        /// <param name="ataskid"></param>
        /// <param name="aisincludeconfirmed"></param>
        /// <param name="aisincludeordered"></param>
        /// <returns></returns>
        [HttpGet, Route("api/mobilesinglelimitresult")]
        public ResultInfo<RowEntity> MobileSingleLimitresult(string ausercode, string ataskid,string aisincludeconfirmed,string aisincludeordered)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                result = ShouJispServer.MobileSingleLimitresult(ausercode, ataskid,aisincludeconfirmed,aisincludeordered);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询交易指标
        /// </summary>
        /// <param name="ausercode"></param>
        /// <param name="ataskid"></param>
        /// <returns></returns>
        [HttpGet, Route("api/mobilesingletradetarget")]
        public ResultInfo<RowEntity> MobileSingleTradeTarget(string ausercode, string ataskid)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                result = ShouJispServer.MobileSingleTradeTarget(ausercode, ataskid);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询单笔交易详情
        /// </summary>
        /// <param name="ausercode"></param>
        /// <param name="ataskid"></param>
        /// <returns></returns>
        [HttpGet, Route("api/mobilesingletradedetailnew")]
        public ResultInfo<RowEntity> MobileSingleTradeDetailNew(string ausercode, string asysordid)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                result = ShouJispServer.MobileSingleTradeDetailNew(ausercode, asysordid);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 批量审批前检查
        /// </summary>
        /// <param name="ausercode"></param>
        /// <param name="ataskid"></param>
        /// <returns></returns>
        [HttpGet, Route("api/mobilecheckbeforeactionbynodeid_batch")]
        public ResultInfo<RowEntity> MobileCheckBeforeActionByNodeId_Batch(string anodeids,string targettype,string ausercode,string adatatypecurrent)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                result = ShouJispServer.MobileCheckBeforeActionByNodeId_Batch(anodeids, targettype,ausercode, adatatypecurrent);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 批量审批操作
        /// </summary>
        /// <param name="piLiangSP"></param>
        /// <returns></returns>
        [HttpPost, Route("api/mobileapprovetaskbynodeid_batch")]
        public ResultInfo<RowEntity> MobileApproveTaskByNodeId_Batch(PiLiangSP piLiangSP)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                result = ShouJispServer.MobileApproveTaskByNodeId_Batch(piLiangSP);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询审批数据分组信息
        /// </summary>
        /// <param name="shenPiFZ"></param>
        /// <returns></returns>
        [HttpPost, Route("api/mobilegettasknodegroup")]
        public ResultInfo<RowEntity> MobileGetTaskNodeGroup(ShenPiFZ shenPiFZ)
        {
            ResultInfo<RowEntity> result = new ResultInfo<RowEntity>();
            try
            {
                result = ShouJispServer.MobileGetTaskNodeGroup(shenPiFZ);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Error = ex.Message;
            }
            return result;
        }

    }
}