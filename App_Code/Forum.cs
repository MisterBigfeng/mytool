using CommHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Forum 的摘要说明
/// </summary>
public class Forum
{
	public Forum()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static string returnData = string.Empty;
    public static string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";

    /// <summary>
    /// 获取论坛列表
    /// </summary>
    /// <param name="postName"></param>
    /// <param name="postGroupName"></param>
    /// <param name="OP"></param>
    /// <returns></returns>
    public static string getForumList(string postName, string postGroupName,string OP,int pageSize,int pageIndex)
    {

        DataSet ds = new DataSet();

        try
        {
            string sqlStr = @"getForumList";

            int sqlType = 2; // 1:sql语句，2：存储过程

            int totalRows = 0;
            int totalPages = 0;

            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@postGroupName",postGroupName),
                new SqlParameter("@postName",postName),
                new SqlParameter("@pageIndex",pageIndex),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@totalRows",totalRows),
                new SqlParameter("@totalPages",totalPages)
            };

            ds = comm.Query(sqlStr, sqlType, sp);
            returnData = getJson.ToJson(ds,OP);

        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, ex.Message.ToString(), "获取失败");
            // throw;
        }

        return returnData;

    }

    /// <summary>
    /// 获取论坛详情
    /// </summary>
    /// <param name="id"></param>
    /// <param name="OP"></param>
    /// <returns></returns>
    public static string getForumDetails(int id,string OP)
    {

        DataSet ds = new DataSet();

        try
        {
            string sqlStr = @"getForumDetails";

            int sqlType = 2; // 1:sql语句，2：存储过程

            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@id",id)
            };
            ds = comm.Query(sqlStr, sqlType, sp);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    returnData = getJson.ToJson(ds,OP);
                }
            }

        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, ex.Message.ToString(), "获取失败");
            // throw;
        }

        return returnData;

    }

    /// <summary>
    /// 添加论坛信息
    /// </summary>
    /// <param name="postName"></param>
    /// <param name="postGroupName"></param>
    /// <param name="postContent"></param>
    /// <param name="OP"></param>
    /// <returns></returns>
    public static string addPostInfo(string postName,string postGroupName,string postContent,string OP) {

        try
        {
            string str = "addPostInfo";
            int sqlType = 2;
            SqlParameter[] sp = new SqlParameter[] { 
                new SqlParameter("@postName",postName),
                new SqlParameter("@postGroupName",postGroupName),
                new SqlParameter("@postContent",postContent),
                new SqlParameter("@OP",OP)
            };

            var res = comm.Execute(str, sqlType, sp);

            returnData = string.Format(msg, res, "添加成功");

        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, ex.Message.ToString(), "添加失败");
            // throw;
        }

        return returnData;
    }

    /// <summary>
    /// 保存回帖
    /// </summary>
    /// <param name="startPostID"></param>
    /// <param name="replayContent"></param>
    /// <param name="OP"></param>
    /// <returns></returns>
    public static string saveReplayInfo(string startPostID, string replayContent, string OP)
    {

        try
        {
            string str = "saveReplayInfo";//存储过程名
            int sqlType = 2;//1:sql语句   2：存储过程
            SqlParameter[] sp = new SqlParameter[] { 
                new SqlParameter("@startPostID",startPostID),
                new SqlParameter("@replayContent",replayContent),
                new SqlParameter("@OP",OP)
            };

            var res = comm.Execute(str, sqlType, sp);

            returnData = string.Format(msg, res, "保存成功");

        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, ex.Message.ToString(), "保存失败");
           // throw;
        }

        return returnData;
    }

    /// <summary>
    /// 删除论坛信息
    /// </summary>
    /// <param name="startPostID"></param>
    /// <param name="OP"></param>
    /// <returns></returns>
    public static string deletePostInfo(int startPostID,string OP) {
        try
        {
            string str = "delete from StartPost where ID="+startPostID;//删除sql语句
            int sqlType = 2;//1:sql语句   2：存储过程

            var res = comm.Execute(str,sqlType,null);

        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, ex.Message.ToString(), "删除论坛记录操作异常");
            // throw;
        }
        return returnData;
    }

    public static string editPostInfo(int startPostID, string postName, string postGroupName, string postContent, string OP) {
        try
        {
            string str = "update StartPost set startPostName='" + postName + "',spartPostContent='" + postContent + "',postGroupName='" + postGroupName + "' where ID=" + startPostID;//删除sql语句
            int sqlType = 2;//1:sql语句   2：存储过程

            var res = comm.Execute(str, sqlType, null);
        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, ex.Message.ToString(), "编辑论坛操作异常");
        }
        return returnData;
    }

}