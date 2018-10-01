using CommHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommHelper;

/// <summary>
/// Login1 的摘要说明
/// </summary>
public class Login1
{
	public Login1()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static string selectUser(string userName, string pwd)
    {
        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";

        try
        {
            string sqlStr = "SELECT * FROM [AgileTestDemo].[dbo].[UserMessage] where userName='" + userName + "'and pwd='" + pwd + "'";
            //localhost
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(comm.conStr);

            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sqlStr;
            cmd.Connection = conn;
            System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            adap.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string limitId = ds.Tables[0].Rows[0][3].ToString();

                returnData = string.Format(msg, true, "登录成功");
            }
            else
            {
                returnData = string.Format(msg,false, "登录失败");
            }

        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, false, ex.Message.ToString());
        }

        return returnData;

    }

    public string insertUser(string userName, string pwd, string limit)
    {
        string msg = "0";
        try
        {

            int result;  //接收sql返回的结果
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(comm.conStr))
            {
                string createTime = DateTime.Now.ToString();
                //插入sql语句
                string sqlStr = "insert into [AgileTestDemo].[dbo].[UserMessage]([userName] ,[pwd],[limit],[createTime])values('" + userName + "','" + pwd + "','" + limit + "','" + createTime + "');";
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqlStr, con))
                {
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }
            if (result > 0)
            {
                msg = "1";//创建成功
            }
            else
            {
                msg = "0";//创建失败
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message.ToString();
        }

        return msg;
    }

    public string returnLimit(string userName, string pwd)
    {

        string msg = "0";
        try
        {

            string sqlStr = "SELECT * FROM [AgileTestDemo].[dbo].[UserMessage] where userName='" + userName + "'and pwd='" + pwd + "'";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(comm.conStr);
            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sqlStr;
            cmd.Connection = conn;
            System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            adap.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string limitId = ds.Tables[0].Rows[0][3].ToString();
                msg = limitId;//用户存在
            }
            else
            {
                msg = "0";//用户不存在
            }

        }
        catch (Exception ex)
        {
            msg = ex.Message.ToString();
        }
        return msg;

    }
}