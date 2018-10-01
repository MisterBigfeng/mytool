<%@ WebHandler Language="C#" Class="ExportFile" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;
using CommHelper;

public class ExportFile : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        
        context.Response.ContentType = "text/plain";

        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"erroeMsg\":\"{1}\"}}";

        try
        {
            string arrayData = context.Request.Params["arrayData"].ToString();
            
            int len = 0;
            if (arrayData != "") {
                len = arrayData.Split('|').Length;
            }
            
            string str = "";

            string sqlStr = "getDemandMsg";

            string type = "3";

            if (type == "3")
            {
                #region 需要导出需求ID

                for (int i = 0; i < len; i++)
                {
                    if (i == len - 1)
                    {
                        str = str + "ID=" + arrayData.Split('|')[i] + "";
                    }
                    else
                    {
                        str = str + "ID=" + arrayData.Split('|')[i] + " or ";
                    }
                }

                sqlStr = @" SELECT [sysName] 系统名称
                      ,[functionModule] 功能模块
                      ,[functionPoint] 功能点
                      ,[caseCode] 案例编号
                      ,[caseName] 案例名称
                      ,[precondition] 前置条件
                      ,[caseType] 案例类型
                      ,[caseDescription] 案例描述
                      ,[caseNature] 案例性质
                      ,[expectedResult] 预期结果
                      ,[priority] 优先级
                      ,[caseEditOP] 案例编写人
                      ,[actualResult] 实际结果
                      ,[caseOP] 案例执行人
                      ,[caseRemarks] 备注
                      ,convert(varchar,[insertDatetime],23) 编写时间
                  FROM [AgileTestDemo].[dbo].[caseTable] where " + str;
                
                #endregion
            }


            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(comm.conStr);
            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqlStr, conn);
            
            if (type == "1")
            {
                cmd.Parameters.AddWithValue("ID", 0);
                cmd.Parameters.AddWithValue("type", 1);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
            }
            
            System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            adap.Fill(ds);

            var table = ds.Tables[0];

            ExcelRender.RenderToExcel(table, context, "测试案例"+DateTime.Now.ToString("yyyyMMddHHmmss")+".xls");

            returnData = string.Format(msg, true, "导出成功");
        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, false, ex.Message.ToString());
        }

        context.Response.Write(returnData);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }
   
}