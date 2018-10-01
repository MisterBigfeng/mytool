<%@ WebHandler Language="C#" Class="dataMigrationCase" %>

using System;
using System.Web;

public class dataMigrationCase : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"erroeMsg\":\"{1}\"}}";
        try
        {
            dataMigration1.dataMigrationCaseMsg();
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