using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Robots.Helper
{
    /// <summary>
    /// Class ServerConfigModel.
    /// </summary>
    public class ServerConfigModel
    {

        /// <summary>
        /// ID
        /// </summary>		
        private decimal _id;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public decimal ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// SERVICENAME
        /// </summary>		
        private string _serviceName;

        /// <summary>
        /// Gets or sets the servicename.
        /// </summary>
        /// <value>The servicename.</value>
        public string SERVICENAME
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }

        /// <summary>
        /// SERVICEIP
        /// </summary>		
        private string _serviceIp;

        /// <summary>
        /// Gets or sets the serviceip.
        /// </summary>
        /// <value>The serviceip.</value>
        public string SERVICEIP
        {
            get { return _serviceIp; }
            set { _serviceIp = value; }
        }

        /// <summary>
        /// LASTTIME
        /// </summary>		
        private DateTime _lastTime;

        /// <summary>
        /// Gets or sets the lasttime.
        /// </summary>
        /// <value>The lasttime.</value>
        public DateTime LASTTIME
        {
            get { return _lastTime; }
            set { _lastTime = value; }
        }

        /// <summary>
        /// Gets or sets the sysdatetime.
        /// </summary>
        /// <value>The sysdatetime.</value>
        public DateTime SYSDATETIME { get; set; }

        /// <summary>
        /// Gets or sets the serviceipper.
        /// </summary>
        /// <value>The serviceipper.</value>
        public string SERVICEIPPER { get; set; }

        /// <summary>
        /// Gets or sets the passminute.
        /// </summary>
        /// <value>The passminute.</value>
        public int PASSMINUTE { get; set; }

        /// <summary>
        /// Gets or sets the second.
        /// </summary>
        /// <value>The second.</value>
        public double SECOND { get; set; }

    }
}
