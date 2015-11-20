using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace System.Robots.Helper
{
    /// <summary>
    /// Class AppConfigHelper.
    /// </summary>
    public class AppConfigHelper
    {
        /// <summary>
        /// Tries the get application settings.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public static string TryGetAppSettings(string key)
        {
            try
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                    return ConfigurationManager.AppSettings[key];
                return string.Empty;
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        /// <summary>
        /// Gets the name of the get win service.
        /// </summary>
        /// <value>The name of the get win service.</value>
        public static string GetWinServiceName
        {
            get
            {
                var setDefault = "WinServiceName";
                try
                {
                    var set = ConfigurationManager.AppSettings.Get("WinServiceName");
                    if (string.IsNullOrEmpty(set))
                    {
                        return setDefault;
                    }
                    else
                    {
                        return set;
                    }
                }
                catch (Exception ex)
                {
                    return setDefault;
                }
            }
        }

        /// <summary>
        /// Gets the get win service thread count.
        /// </summary>
        /// <value>The get win service thread count.</value>
        public static string GetWinServiceThreadCount
        {
            get
            {
                var setDefault = "20";
                try
                {
                    var set = ConfigurationManager.AppSettings.Get("WinServiceThreadCount");
                    if (string.IsNullOrEmpty(set))
                    {
                        return setDefault;
                    }
                    else
                    {
                        return set;
                    }
                }
                catch (Exception ex)
                {
                    return setDefault;
                }
            }
        }

        /// <summary>
        /// Gets the get win service automatic switch.
        /// </summary>
        /// <value>The get win service automatic switch.</value>
        public static string GetWinServiceAutoSwitch
        {
            get
            {
                var setDefault = "TRUE";
                try
                {

                    var set = ConfigurationManager.AppSettings.Get("WinServiceAutoSwitch");
                    if (string.IsNullOrEmpty(set))
                    {
                        return setDefault;
                    }
                    else
                    {
                        return set;
                    }
                }
                catch (Exception ex)
                {
                    return setDefault;
                }
            }
        }

        /// <summary>
        /// Gets the pass minute.
        /// </summary>
        /// <value>The pass minute.</value>
        public static string PassMinute
        {
            get
            {
                var setDefault = "5";
                try
                {

                    var set = ConfigurationManager.AppSettings.Get("PassMinute");
                    if (string.IsNullOrEmpty(set))
                    {
                        return setDefault;
                    }
                    else
                    {
                        return set;
                    }
                }
                catch (Exception ex)
                {
                    return setDefault;
                }
            }
        }
        
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIpAddress()
        {
            try
            {
                if (string.IsNullOrEmpty(_ipAddress))
                {
                    var hostName = Dns.GetHostName();
                    var localHost = Dns.GetHostEntry(hostName);
                    var localAddress = localHost.AddressList[0];
                    _ipAddress = localAddress.ToString();
                }
                return _ipAddress;
            }
            catch (Exception)
            {
                return "127.0.0.1";
            }
        }


        /// <summary>
        /// The _ip address
        /// </summary>
        private static string _ipAddress;
    }
}
