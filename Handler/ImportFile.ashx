<%@ WebHandler Language="C#" Class="ImportFile" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using NetUtilityLib;
using System.Text;
using System.Collections.Generic;
using CommHelper;

public class ImportFile : IHttpHandler , System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
       
        string res = UploadExcelData(context);

        context.Response.Write(res);
    }
    /// <summary>  
    /// Excel导入到数据库  
    /// </summary>  
    /// <param name="context"></param>  
    public string UploadExcelData(HttpContext context)
    {
        string res = "";
        var flist = context.Request.Files;
        for (int i = 0; i < flist.Count; i++)
        {

            var c = flist[i];
            string IsXls = System.IO.Path.GetExtension(c.FileName).ToString().ToLower();
            if (IsXls != ".xls" && IsXls != ".xlsx")
            {
                return "格式不正确！";
            }
            string savePath = "upload/" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + IsXls;
            c.SaveAs(context.Server.MapPath(savePath));//将文件保存到服务器，因为火狐浏览器获取不到客户端的路径。  

                //导入需求
            if (context.Request.QueryString["type"].ToString() == "1")
            {
                res = importExportFile.TestExcelRead(context.Server.MapPath(savePath), null,0);
            }
            //导入案例
            else {

                string demandID = context.Request.QueryString["demandID"].ToString();
                int ID = int.Parse(context.Request.QueryString["ID"].ToString());

                res = importExportFile.TestExcelRead(context.Server.MapPath(savePath), demandID,ID);
            }


        }
        return res;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}