using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace CommHelper
{

    /// <summary>
    /// comm 的摘要说明
    /// </summary>
    public class comm
    {
        public comm()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        public static string conStr = "Data Source=FWY-PC\\SQLEXPRESS;Initial Catalog=AgileTestDemo;Integrated Security=True";

        //public static string conStr = "Data Source=.;Initial Catalog=AgileTest;Integrated Security=True";

        #region Query

        //sqlType 1:sql语句，2：存储过程
        public static DataSet Query(string sqlStr, int sqlType, SqlParameter[] sp)
        {

            DataSet ds = new DataSet();
            
            SqlConnection conn = new SqlConnection(conStr);

            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlStr;
                cmd.Connection = conn;
                if (sqlType == 2 && sp!=null)
                {
                    cmd.Parameters.AddRange(sp);
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(ds);

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return ds;

        }


        #endregion

        #region Execute

        public static int Execute(string sqlStr, int sqlType)
        {
            SqlConnection conn = new SqlConnection(conStr);
            int res = 0;
            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlStr;
                cmd.Connection = conn;
                if (sqlType == 2)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }

                res=cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return res;

        }



        public static int Execute(string sqlStr, int sqlType, SqlParameter[] sp)
        {
            SqlConnection conn = new SqlConnection(conStr);
            int res = 0;
            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlStr;
                cmd.Connection = conn;
                if (sqlType == 2 && sp != null)
                {
                    cmd.Parameters.AddRange(sp);
                    cmd.CommandType = CommandType.StoredProcedure;
                }

                res = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return res;

        }
        #endregion

        #region DataTableJson
        public static string DataTableToJson(string jsonName, DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string columnsName = dt.Columns[j].ColumnName;
                        if (columnsName != "startPostTime" && columnsName != "replyTime")
                        {
                            Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace(" ", "").Replace("\n", "^").Replace("\r", "").Replace('"', '”') + "\"");
                        }
                        else
                        {
                            Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            string jsonStr = Json.ToString();
            return jsonStr;
        }

        #endregion

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Dictionary<String, Object> GetParameter(HttpContext context)
        {
            StreamReader reader = new StreamReader(context.Request.InputStream);
            //得到json字符串：strJson={"key3":"xdp-gacl","key4":"白虎神皇"}
            String strJson = HttpUtility.UrlDecode(reader.ReadToEnd());
            JavaScriptSerializer jss = new JavaScriptSerializer();
            //将json字符串反序列化成一个Dictionary对象
            Dictionary<String, Object> dicParameter = jss.Deserialize<Dictionary<String, Object>>(strJson);
            return dicParameter;
        }


    }
}