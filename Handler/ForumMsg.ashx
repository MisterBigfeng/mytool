<%@ WebHandler Language="C#" Class="ForumMsg" %>

using System;
using System.Web;
using System.Collections.Generic;
using CommHelper;

public class ForumMsg : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string returnData = string.Empty;

        Dictionary<String, Object> dicParameter = comm.GetParameter(context);

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";
        try
        {

            if (context.Request.Cookies["userName"] == null || context.Request.Cookies["userName"].Value.ToString() == "")
            {
                string resStr = string.Format(msg, "-1", "请重新登录");
                context.Response.Write(resStr);
                return;
            }


            string type = dicParameter["type"].ToString();
        
            string OP = context.Request.Cookies["userName"].Value.Split('=')[1].ToString();
            
            //获取论坛列表
            if (type == "1")
            {
                string postName = dicParameter["postName"].ToString();
                string postGroupName = dicParameter["postGroupName"].ToString();
                int pageSize = int.Parse(dicParameter["pageSize"].ToString());
                int pageIndex = int.Parse(dicParameter["pageIndex"].ToString());

                returnData = Forum.getForumList(postName, postGroupName, OP, pageSize, pageIndex);
            }
            //获取论坛详情
            else if(type == "2")
            {
                int id = int.Parse(dicParameter["id"].ToString());
               
                returnData = Forum.getForumDetails(id, OP);
            }
            //添加帖子
            else if (type == "3")
            {
                string postName = dicParameter["postName"].ToString();
                string postGroupName = dicParameter["postGroupName"].ToString();
                string postContent = dicParameter["postContent"].ToString();
            

                returnData = Forum.addPostInfo(postName,postGroupName,postContent,OP);
            }
            //保存回帖
            else if (type == "4")
            {
                string startPostID = dicParameter["startPostID"].ToString();
                string replayContent = dicParameter["replayContent"].ToString();
                returnData = Forum.saveReplayInfo(startPostID, replayContent, OP);
            }
            //编辑
            else if (type == "6")
            {
                int startPostID = int.Parse(dicParameter["startPostID"].ToString());
                string postName = dicParameter["postName"].ToString();
                string postGroupName = dicParameter["postGroupName"].ToString();
                string postContent = dicParameter["postContent"].ToString();

                returnData = Forum.editPostInfo(startPostID,postName, postGroupName, postContent, OP);

            }
            //删除
            else if (type == "7")
            {
                int startPostID = int.Parse(dicParameter["startPostID"].ToString());
                returnData = Forum.deletePostInfo(startPostID, OP);

            }
        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, ex.Message.ToString(), "测试论坛执行异常");
            //throw;
        }
        context.Response.Write(returnData);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}