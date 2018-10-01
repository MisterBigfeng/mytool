<%@ WebHandler Language="C#" Class="DemandMsg" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using CommHelper;

public class DemandMsg : IHttpHandler , System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string getMsg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";


        try
        {
            if (context.Request.Cookies["userName"] == null || context.Request.Cookies["userName"].ToString()=="")
            {
                string resStr = string.Format(getMsg, "-1", "请重新登录");
                context.Response.Write(resStr);
                return;
            }
            
            Dictionary<String, Object> dicParameter = comm.GetParameter(context);

            string type = dicParameter["type"].ToString();
            if (type == "1")
            {
                string demandID = dicParameter["demandID"].ToString();
                string demandName = dicParameter["demandName"].ToString();
                int pageIndex = int.Parse(dicParameter["pageIndex"].ToString());
                int pageSize = int.Parse(dicParameter["pageSize"].ToString());

                TestCase.DemandRequest dr = new TestCase.DemandRequest();
                dr.demandID = demandID;
                dr.demandName = demandName;
                dr.pageIndex = pageIndex;
                dr.pageSize = pageSize;

                //获取需求信息
                getMsg = TestCase.getDemandList(dr, context.Request.Cookies["userName"].ToString());

                context.Response.Write(getMsg);
            }
            else if (type == "2")
            {
                string actualComplateTime = dicParameter["actualComplateTime"].ToString();
                string bankOP = dicParameter["bankOP"].ToString();
                string demandID = dicParameter["demandID"].ToString();
                string demandName = dicParameter["demandName"].ToString();
                string demandType = dicParameter["demandType"].ToString();
                string devOP = dicParameter["devOP"].ToString();
                string estmateComplateTime = dicParameter["estmateComplateTime"].ToString();
                string estmateProductionTime = dicParameter["estmateProductionTime"].ToString();
                string kjb = dicParameter["kjb"].ToString();
                string remark = dicParameter["remark"].ToString();
                string sitComplatetime = dicParameter["sitComplatetime"].ToString();
                string sitTestTime = dicParameter["sitTestTime"].ToString();
                string status = dicParameter["status"].ToString();
                string submitConfirmTime = dicParameter["submitConfirmTime"].ToString();
                string testOP = dicParameter["testOP"].ToString();
                string uatFinishDatetime = dicParameter["uatFinishDatetime"].ToString();
                string uatTestDatetime = dicParameter["uatTestDatetime"].ToString();
                string uatTestOP = context.Request.Cookies["userName"].ToString();

                string Totaltestcase = dicParameter["Totaltestcase"].ToString();
                string Totaltestcasepass = dicParameter["Totaltestcasepass"].ToString();
                string Todaytestcasepass = dicParameter["Todaytestcasepass"].ToString();
                string Totalbug = dicParameter["Totalbug"].ToString();
                string Notrepairedbug = dicParameter["Notrepairedbug"].ToString();
                

                Features.FeaturesRequest features = new Features.FeaturesRequest();

                features.actualComplateTime =actualComplateTime;
                features.bankOP =bankOP;
                features.demandID =demandID;
                features.demandName =demandName;
                features.demandType =demandType;
                features.devOP =devOP;
                features.estmateComplateTime =estmateComplateTime;
                features.estmateProductionTime =estmateProductionTime;
                features.kjb =kjb;
                features.remark =remark;
                features.sitComplatetime =sitComplatetime;
                features.sitTestTime =sitTestTime;
                features.status =status;
                features.submitConfirmTime =submitConfirmTime;
                features.testOP =testOP;
                features.uatFinishDatetime =uatFinishDatetime;
                features.uatTestDatetime =uatTestDatetime;
                features.uatTestOP = context.Request.Cookies["userName"].ToString();

                features.Totaltestcase = Totaltestcase;
                features.Totaltestcasepass = Totaltestcasepass;
                features.Todaytestcasepass = Todaytestcasepass;
                features.Totalbug = Totalbug;
                features.Notrepairedbug = Notrepairedbug;
                
                getMsg = Features.saveDemandInfo(features);
                context.Response.Write(getMsg);
            }
            else if (type == "3")
            {
                int ID = int.Parse(dicParameter["ID"].ToString());
                string demandID = dicParameter["demandID"].ToString();
                string demandName = dicParameter["demandName"].ToString();
                getMsg = Features.deleteDemandInfo(ID, demandID, demandName);
                context.Response.Write(getMsg);
            }
            else if (type == "4")
            {
                int ID = int.Parse(dicParameter["ID"].ToString());
                string demandID = dicParameter["demandID"].ToString();
                string demandName = dicParameter["demandName"].ToString();
                getMsg = Features.getDemandDetails(ID, demandID, demandName);
                context.Response.Write(getMsg);
            }
            //context.Response.Write(getMsg);
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