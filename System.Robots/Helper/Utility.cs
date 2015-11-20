using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Robots.Helper
{
    public class Utility
    {
        /// 把datatable转换为modeList
        /// 调用此函数请保证一下几点：
        /// 1.model的内不包含子model。
        /// 2.model有无参构造函数
        /// 3.model的属性名和datatable的列名一致（不区分大小写）
        /// </summary>
        /// <typeparam name="T">model的类型</typeparam>
        /// <param name="dt">datatable</param>
        /// <returns></returns>
        public static List<T> SetValueFromDB<T>(DataTable dt) where T : new()
        {
            List<T> lstReturn = new List<T>();
            if (dt != null && dt.Rows.Count > 0)
            {

                DataColumnCollection dcc = dt.Columns;
                foreach (DataRow dr in dt.Rows)
                {
                    lstReturn.Add(ConvertDataRowToModel<T>(dr, dcc));
                }
                return lstReturn;
            }
            return lstReturn;
        }

        /// <summary>
        /// 把datarow转换为model
        /// 调用此函数请保证一下几点：
        /// 1.model的内不包含子model。
        /// 2.model有无参构造函数
        /// 3.model的属性名和datatable的列名一致（不区分大小写）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="dcc"></param>
        /// <returns></returns>
        public static T ConvertDataRowToModel<T>(DataRow dr, DataColumnCollection dcc) where T : new()
        {
            if (dr == null || dcc == null)
            {
                return default(T);
            }
            T t = new T();
            PropertyInfo[] pis = t.GetType().GetProperties();
            PropertyInfo pi = null;
            foreach (DataColumn dc in dcc)
            {
                pi = pis.FirstOrDefault(p => p.Name.ToLower().Equals(dc.ColumnName.ToLower()));
                if (pi != null
                    && dr[dc] != null
                    && dr[dc] != DBNull.Value
                    && pi.CanWrite)
                {
                    Type type = pi.PropertyType;
                    if (type.Name.ToLower().Contains("nullable"))
                    {
                        type = Nullable.GetUnderlyingType(type);
                    }
                    if (type.IsEnum)
                    {
                        if (dr[dc] != null && dr[dc] != DBNull.Value)
                        {
                            pi.SetValue(t, Enum.Parse(type, Convert.ToInt32(dr[dc]).ToString()), null);
                        }
                    }
                    else
                    {
                        pi.SetValue(t, Convert.ChangeType(dr[dc], type), null);
                    }
                }
            }
            return t;
        }


        /// <summary>
        /// 转换model
        /// 调用此函数请保证一下几点：
        /// 1.model的内不包含子model。
        /// 2.model有无参构造函数
        /// 3.两个model的属性名一致（不区分大小写）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="dcc"></param>
        /// <returns></returns>
        public static Y CopyTToY<T, Y>(T t) where Y : new()
        {
            Y y = new Y();
            PropertyInfo[] tpis = t.GetType().GetProperties();
            PropertyInfo[] ypis = y.GetType().GetProperties();
            foreach (PropertyInfo tpi in tpis)
            {
                var ypi = ypis.FirstOrDefault(p => p.Name.ToLower().Equals(tpi.Name.ToLower()));
                if (ypi != null
                    && tpi.GetValue(t, null) != null
                    && ypi.CanWrite)
                {
                    Type type = ypi.PropertyType;
                    if (type.Name.ToLower().Contains("nullable"))
                    {
                        type = Nullable.GetUnderlyingType(type);
                    }
                    if (type.IsEnum)
                    {
                        if (ypi != null && tpi.GetValue(t, null) != null)
                        {
                            ypi.SetValue(y, Enum.Parse(type, Convert.ToInt32(tpi.GetValue(t, null)).ToString()), null);
                        }
                    }
                    else
                    {
                        ypi.SetValue(y, Convert.ChangeType(tpi.GetValue(t, null), type), null);
                    }
                }
            }
            return y;
        }

    }
}
