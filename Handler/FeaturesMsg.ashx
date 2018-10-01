<%@ WebHandler Language="C#" Class="featuresMsg" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using CommHelper;

public class featuresMsg : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        
        context.Response.ContentType = "text/plain";

        string getMsg = "{{\"code\":\"{0}\",\"msg\":\"{1}\"}}";
        
        try
        {
            Dictionary<String, Object> dicParameter = comm.GetParameter(context);
                
            string type = dicParameter["type"].ToString();
            
            if (type == "1")
            {
                //获取需求信息
                getMsg = Features.getFeaturesList();
                
                context.Response.Write(getMsg);
                
            }

            //获取当前需求下的案例信息
            if (type == "2")
            {
                
                string featureID = dicParameter["featureID"].ToString();

                getMsg = Features.getFeaturesCaseList(featureID);
                
                context.Response.Write(getMsg);
                
            }

        }
        catch (Exception ex)
        {
            context.Response.Write(string.Format(getMsg, 0, ex.Message.ToString()));
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}