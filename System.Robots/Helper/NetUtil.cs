using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace System.Robots.Helper
{
    /// <summary>
    /// Class NetUtil.
    /// </summary>
    public class NetUtil
    {
        /// <summary>
        /// Gets the host ip.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetHostIp()
        {
            IPAddress[] arrIpAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in arrIpAddresses)
            {
                if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))
                {
                    return ip.ToString();

                }
            }

            return "0.0.0.0";
        }
    }
}
