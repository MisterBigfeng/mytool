<%@ WebHandler Language="C#" Class="UserMsg" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using CommHelper;
using System.Text;

public class UserMsg : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        
        context.Response.ContentType = "text/plain";

        string getMsg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";
        
        try
        {
            Dictionary<String, Object> dicParameter = comm.GetParameter(context);

            string userName = dicParameter["userName"].ToString();
            string pwd = dicParameter["pwd"].ToString();
            string type = dicParameter["type"].ToString();
            

            if (string.IsNullOrEmpty(type))
            {
                context.Response.Write(string.Format(getMsg, true, "登录失败！"));

                return;
            }
            
            //登录
            if (type == "1")
            {
                getMsg = Login1.selectUser(userName, pwd);
                
                //context.Session["userName"] = userName;

                HttpCookie cookies = new HttpCookie("userName");
                cookies["userName"] = userName;
                context.Response.AppendCookie(cookies);

                context.Response.Write(getMsg);
                
            }
                
            //退出
            if(type=="-1")
            {
                context.Session["userName"] = "";

                context.Response.Write(string.Format(getMsg, true, "已退出"));
                
            }
            

        }
        catch (Exception ex)
        {
            context.Response.Write(string.Format(getMsg, false, ex.Message.ToString()));
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}