using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace System.Robots.Helper
{
    /// <summary>
    /// 日志记录类
    /// </summary>
    public class LogHelper<T>
    {
        /// <summary>
        /// Logger 实例
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(T));
        private static readonly ILog LoggerMail = LogManager.GetLogger("MailLog");



        /// <summary>
        /// 写入普通信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void Info(string message, bool eamil = false)
        {
            RecordLog(Logger.Info, message);
            if (eamil)
            {
                var ip = NetUtil.GetHostIp();
                var mailLog = message + " Ip:" + ip;
                RecordLog(LoggerMail.Info, message);
            }

        }

        /// <summary>
        /// 写入警告信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Warn(string message, Exception ex, bool eamil = false)
        {
            RecordLog(Logger.Warn, message, ex);
            if (eamil)
            {
                var ip = NetUtil.GetHostIp();
                var mailLog = message + " Ip:" + ip;
                RecordLog(LoggerMail.Warn, message);
            }
        }

        /// <summary>
        /// 写入错误信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Error(string message, Exception ex, bool eamil = false)
        {
            RecordLog(Logger.Error, message, ex);
            if (eamil)
            {
                var ip = NetUtil.GetHostIp();
                var mailLog = message + " Ip:" + ip;
                RecordLog(LoggerMail.Error, message, ex);
            }
        }

        /// <summary>
        /// 写入严重错误信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Fatal(string message, Exception ex)
        {
            RecordLog(Logger.Fatal, message, ex);
        }

        /// <summary>
        /// 写入调试信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Debug(string message, Exception ex)
        {
            RecordLog(Logger.Debug, message, ex);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="method">日志记录类型的方法</param>
        /// <param name="message">消息内容</param>
        private static void RecordLog(Action<string> method, string message)
        {
            ThreadPool.QueueUserWorkItem(x => method(message));
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="method">日志记录类型的方法</param>
        /// <param name="message">消息内容</param>
        /// <param name="ex">异常</param>
        private static void RecordLog(Action<string, Exception> method, string message, Exception ex)
        {
            ThreadPool.QueueUserWorkItem(x => method(message, ex));
        }
    }
}
