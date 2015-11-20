using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Robots.Helper
{
    /// <summary>
    /// Class ServerConfigBll.
    /// </summary>
    public partial class ServerConfigBll
    {
        private readonly ServerConfigDal dal = new ServerConfigDal();

        public ServerConfigBll()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(decimal ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ServerConfigModel model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ServerConfigModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateLastTime(ServerConfigModel model)
        {
            return dal.UpdateLastTime(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateLastIp(ServerConfigModel model)
        {
            return dal.UpdateLastIp(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(decimal ID)
        {

            return dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ServerConfigModel GetModel(decimal ID)
        {

            return dal.GetModel(ID);
        }
        
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ServerConfigModel GetModel(ServerConfigModel model)
        {

            return dal.GetModel(model);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ServerConfigModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ServerConfigModel> DataTableToList(DataTable dt)
        {
            List<ServerConfigModel> modelList = new List<ServerConfigModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ServerConfigModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ServerConfigModel();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = decimal.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["LASTTIME"].ToString() != "")
                    {
                        model.LASTTIME = DateTime.Parse(dt.Rows[n]["LASTTIME"].ToString());
                    }


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion
    }
}
