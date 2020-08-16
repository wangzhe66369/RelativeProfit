using CalculateStock.Common.Log;
using CalculateStock.Web.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace CalculateStock.Web.Common
{
    public class ErrorHandleAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            ErrorMessage msg = new ErrorMessage(filterContext.Exception, "页面");
            msg.ShowException = MvcException.IsExceptionEnabled();

            var log = new LogHelper();

            log.LogError("被系统过滤捕获的异常" + filterContext.Exception);

            //设置为true阻止golbal里面的错误执行
            filterContext.ExceptionHandled = true;
            if (filterContext.HttpContext.Request.IsAjaxRequest())
                filterContext.Result = new JsonResult() { Data = new ResultEntity() { result = false, message = "服务器异常,联系管理员或查看错误日志！" } };
            else
                filterContext.Result = new ViewResult() { ViewName = "ISE", ViewData = new ViewDataDictionary<ErrorMessage>(msg) };
        }
    }

    /// <summary>
    /// 异常信息显示
    /// </summary>
    public class MvcException
    {
        /// <summary>
        /// 是否已经获取的允许显示异常
        /// </summary>
        private static bool HasGetExceptionEnabled = false;

        private static bool isExceptionEnabled;

        /// <summary>
        /// 是否显示异常信息
        /// </summary>
        /// <returns>是否显示异常信息</returns>
        public static bool IsExceptionEnabled()
        {
            if (!HasGetExceptionEnabled)
            {
                isExceptionEnabled = GetExceptionEnabled();
                HasGetExceptionEnabled = true;
            }
            return isExceptionEnabled;
        }

        /// <summary>
        /// 根据Web.config AppSettings节点下的ExceptionEnabled值来决定是否显示异常信息
        /// </summary>
        /// <returns></returns>
        private static bool GetExceptionEnabled()
        {
            bool result;
            if (!Boolean.TryParse(ConfigurationManager.AppSettings["ExceptionEnabled"], out result))
            {
                return false;
            }
            return result;
        }
    }
}