using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Robots.Helper
{
    /// <summary>
    /// Class ServerConfigDal.
    /// </summary>
    public partial class ServerConfigDal
    {
        OracleHelper OracleHelper = new OracleHelper(ConfigurationManager.ConnectionStrings["RobotsBasicDbConnection"].ToString());
        public ServerConfigDal()
        { }

        public bool Exists(decimal ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Robots_ServerConfig");
            strSql.Append(" where ");
            strSql.Append(" ID = :ID  ");
            OracleParameter[] parameters = {
					new OracleParameter(":ID", OracleType.Number,4)			};
            parameters[0].Value = ID;

            return OracleHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ServerConfigModel model)
        {
            StringBuilder strSql = new StringBuilder();
            List<OracleParameter> listPara = new List<OracleParameter>();
            strSql.Append("insert into Robots_ServerConfig(");
            strSql.Append("ID,SERVICENAME,SERVICEIP,LASTTIME,PASSMINUTE,SECOND");
            strSql.Append(") values (");
            strSql.Append(":ID,:SERVICENAME,:SERVICEIP,sysdate,:PASSMINUTE,:SECOND");
            strSql.Append(") ");


            listPara.Add(new OracleParameter(":ID", OracleType.Number, 4) { Value = model.ID });
            listPara.Add(new OracleParameter(":SERVICENAME", OracleType.VarChar, 100) { Value = model.SERVICENAME });
            listPara.Add(new OracleParameter(":SERVICEIP", OracleType.VarChar, 100) { Value = model.SERVICEIP });
            listPara.Add(new OracleParameter(":PASSMINUTE", OracleType.Number, 4) { Value = model.PASSMINUTE });

            listPara.Add(new OracleParameter(":SECOND", OracleType.Number) { Value = model.SECOND });


            int rows = OracleHelper.ExecuteSql(strSql.ToString(), listPara.ToArray());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ServerConfigModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Robots_ServerConfig set ");

            strSql.Append(" ID = :ID , ");
            strSql.Append(" SERVICENAME = :SERVICENAME , ");
            strSql.Append(" SERVICEIP = :SERVICEIP , ");
            strSql.Append(" LASTTIME = :LASTTIME,  ");
            strSql.Append(" SECOND = :SECOND,  ");
            strSql.Append(" PASSMINUTE = :PASSMINUTE  ");
            strSql.Append(" where ID=:ID  ");

            OracleParameter[] parameters = {
			            new OracleParameter(":ID", OracleType.Number,4){Value = model.ID} ,            
                        new OracleParameter(":SERVICENAME", OracleType.VarChar,100){Value = model.SERVICENAME} ,            
                        new OracleParameter(":SERVICEIP", OracleType.VarChar,100){Value = model.SERVICEIP} ,            
                        new OracleParameter(":LASTTIME", OracleType.DateTime){Value = model.LASTTIME},
                        new OracleParameter(":PASSMINUTE", OracleType.Number,4){Value = model.PASSMINUTE},
                        new OracleParameter(":SECOND", OracleType.Number){Value = model.SECOND}
              
            };
            int rows = OracleHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateLastTime(ServerConfigModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Robots_ServerConfig set ");

            strSql.Append(" LASTTIME = SYSDATE,PASSMINUTE=:PASSMINUTE,SECOND=:SECOND  ");
            strSql.Append(" where ID=:ID and SERVICENAME=:SERVICENAME and LASTTIME=:LASTTIME and SERVICEIP=:SERVICEIP ");

            OracleParameter[] parameters = {
			            new OracleParameter(":ID", OracleType.Number,4){Value = model.ID},
                        new OracleParameter(":SERVICENAME", OracleType.VarChar,100){Value = model.SERVICENAME} ,            
                        new OracleParameter(":SERVICEIP", OracleType.VarChar,100){Value = model.SERVICEIP} ,            
                        new OracleParameter(":LASTTIME", OracleType.DateTime){Value = model.LASTTIME},
                        new OracleParameter(":PASSMINUTE", OracleType.Number,4){Value = model.PASSMINUTE},
                        new OracleParameter(":SECOND", OracleType.Number){Value = model.SECOND}
              
            };
            int rows = OracleHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateLastIp(ServerConfigModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Robots_ServerConfig set ");

            strSql.Append(" LASTTIME = SYSDATE,SERVICEIP=:SERVICEIP,PASSMINUTE=:PASSMINUTE,SECOND=:SECOND  ");
            strSql.Append(" where ID=:ID and SERVICENAME=:SERVICENAME and LASTTIME=:LASTTIME and SERVICEIP=:SERVICEIPPER ");

            OracleParameter[] parameters = {
			            new OracleParameter(":ID", OracleType.Number,4){Value = model.ID},
                        new OracleParameter(":SERVICENAME", OracleType.VarChar,100){Value = model.SERVICENAME} ,            
                        new OracleParameter(":SERVICEIP", OracleType.VarChar,100){Value = model.SERVICEIP} , 
                        new OracleParameter(":SERVICEIPPER", OracleType.VarChar,100){Value = model.SERVICEIPPER} ,  
                        new OracleParameter(":LASTTIME", OracleType.DateTime){Value = model.LASTTIME},
                        new OracleParameter(":PASSMINUTE", OracleType.Number,4){Value = model.PASSMINUTE},
                        new OracleParameter(":SECOND", OracleType.Number){Value = model.SECOND}
              
            };
            int rows = OracleHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(decimal ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Robots_ServerConfig ");
            strSql.Append(" where ID=:ID ");
            OracleParameter[] parameters = {
					new OracleParameter(":ID", OracleType.Number,4)			};
            parameters[0].Value = ID;


            int rows = OracleHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ServerConfigModel GetModel(decimal ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, SERVICENAME, SERVICEIP, LASTTIME,PASSMINUTE  ");
            strSql.Append("  from Robots_ServerConfig ");
            strSql.Append(" where ID=:ID ");
            OracleParameter[] parameters = {
					new OracleParameter(":ID", OracleType.Number,4)			};
            parameters[0].Value = ID;


            return OracleHelper.QueryEntity<ServerConfigModel>(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ServerConfigModel GetModel(ServerConfigModel model)
        {
            List<OracleParameter> listpara = new List<OracleParameter>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, SERVICENAME, SERVICEIP, LASTTIME,SYSDATE SYSDATETIME,PASSMINUTE,SECOND  ");
            strSql.Append("  from Robots_ServerConfig ");
            strSql.Append(" where 1=1 ");
            if (!string.IsNullOrEmpty(model.SERVICENAME))
            {
                strSql.Append(" and SERVICENAME=:SERVICENAME ");
                listpara.Add(new OracleParameter(":SERVICENAME", OracleType.VarChar, 100) { Value = model.SERVICENAME });

            }
            if (!string.IsNullOrEmpty(model.SERVICEIP))
            {
                strSql.Append(" and SERVICEIP=:SERVICEIP ");
                listpara.Add(new OracleParameter(":SERVICEIP", OracleType.VarChar, 100) { Value = model.SERVICEIP });

            }

            strSql.Append(" order by  id desc ");

            return OracleHelper.QueryEntity<ServerConfigModel>(strSql.ToString(), listpara.ToArray());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Robots_ServerConfig ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return OracleHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM Robots_ServerConfig ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return OracleHelper.Query(strSql.ToString());
        }
    }
}
