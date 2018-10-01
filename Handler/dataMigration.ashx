<%@ WebHandler Language="C#" Class="dataMigration" %>

using System;
using System.Web;

public class dataMigration : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"erroeMsg\":\"{1}\"}}";
        try
        {
            dataMigration1.dataMigrationMsg();
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}