using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Robots.Helper
{
    /// <summary>
    /// Class CheckServerService.
    /// </summary>
    public class CheckServerService
    {
        /// <summary>
        /// Checks the server.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="second">The second.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CheckServer(string serviceName, double second)
        {
            try
            {
                int passMinute = 5;
                int.TryParse(AppConfigHelper.PassMinute, out passMinute);
                string serviceIp = NetUtil.GetHostIp();
                second = Math.Ceiling(second);

                var serverConfigBll = new ServerConfigBll();
                var severConfigModel = serverConfigBll.GetModel(new ServerConfigModel() { SERVICENAME = serviceName });
                //不存在记录，插入记录返回false
                if (severConfigModel == null)
                {
                    ServerConfigModel model = new ServerConfigModel();
                    model.SERVICENAME = serviceName;
                    model.SERVICEIP = serviceIp;
                    model.PASSMINUTE = passMinute;
                    model.SECOND = second;
                    var bak = serverConfigBll.Add(model);
                    TextLog.Append("无服务配制,插入记录,返回F");
                    return false;
                }
                else
                {
                    if (severConfigModel.PASSMINUTE > 0)
                    {
                        passMinute = severConfigModel.PASSMINUTE;
                    }
                    else
                    {
                        severConfigModel.PASSMINUTE = passMinute;
                    }
                    //存在记录IP相同
                    if (severConfigModel.SERVICEIP == serviceIp)
                    {
                        //存在记录IP相同，5分种内更新，返回true;
                        if (severConfigModel.SYSDATETIME < severConfigModel.LASTTIME.AddMinutes(passMinute).AddSeconds(severConfigModel.SECOND))
                        {
                            severConfigModel.SECOND = second;
                            var bak = serverConfigBll.UpdateLastTime(severConfigModel);
                            TextLog.Append(string.Format("IP相同,{0}分内,有权,返回T", passMinute));
                            return bak;
                        }
                        else
                        {

                            severConfigModel.SECOND = second;
                            var bak = serverConfigBll.UpdateLastTime(severConfigModel);
                            //存在记录IP相同，5分种后主服务器收回权限，返回更新true;
                            TextLog.Append(string.Format("IP相同,{0}分后,获权,返回F", passMinute));
                            return false;

                        }
                    }
                    else
                    {
                        //IP不同，5分种后返回true
                        if (severConfigModel.SYSDATETIME > severConfigModel.LASTTIME.AddMinutes(passMinute).AddSeconds(severConfigModel.SECOND))
                        {
                            TextLog.Append(string.Format("IP不同,{0}分种后,获权,返回F", passMinute));
                            severConfigModel.SERVICEIPPER = severConfigModel.SERVICEIP;
                            severConfigModel.SERVICEIP = serviceIp;
                            severConfigModel.SECOND = second;
                            var bak = serverConfigBll.UpdateLastIp(severConfigModel);
                            return false;
                        }
                        else
                        {

                            //IP不同，5分种内返回false
                            TextLog.Append(string.Format("IP不同,{0}分种内,无权,返回F", passMinute));
                            return false;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                TextLog.Append(String.Format("检查服务配制失败: " + ex.Message));
                return false;
            }
        }
    }
}
