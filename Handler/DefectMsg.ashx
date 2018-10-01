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
                string defectName = dicParameter["defectName"].ToString();
                string defectNo = dicParameter["defectNo"].ToString();
                string demandID = dicParameter["demandID"].ToString();
                int pageIndex = int.Parse(dicParameter["pageIndex"].ToString());
                int pageSize = int.Parse(dicParameter["pageSize"].ToString());

                DefectManager.DefectRequest dr = new DefectManager.DefectRequest();
                dr.defectName = defectName;
                dr.defectNo = defectNo;
                dr.demandID = demandID;
                dr.pageIndex = pageIndex;
                dr.pageSize = pageSize;

                //获取需求信息
                getMsg = DefectManager.getDefectList(dr, context.Request.Cookies["userName"].ToString());

                context.Response.Write(getMsg);
            }
            else if (type == "2")
            {

                string status = dicParameter["status"].ToString();
                string defectNo = dicParameter["defectNo"].ToString();
                string defectName = dicParameter["defectName"].ToString();
                string defectGrade = dicParameter["defectGrade"].ToString();
                string demandID = dicParameter["demandID"].ToString();
                string defectType = dicParameter["defectType"].ToString();
                string caseID = dicParameter["caseID"].ToString();
                string defectDescription = dicParameter["defectDescription"].ToString();
                string finishDatetime = dicParameter["finishDatetime"].ToString();
                string accessory = dicParameter["accessory"].ToString();
                string remark = dicParameter["remark"].ToString();
                string functionModule = dicParameter["functionModule"].ToString();
                string functionPoint = dicParameter["functionPoint"].ToString();
                string yzcd = dicParameter["yzcd"].ToString();
                string devOP = dicParameter["devOP"].ToString();
                string testOP = dicParameter["testOP"].ToString();
                string defectSource = dicParameter["defectSource"].ToString();
                string defectFX = dicParameter["defectFX"].ToString();
                string findDatetime = dicParameter["findDatetime"].ToString();


                DefectManager.DefectRequest defect = new DefectManager.DefectRequest();

                defect.status = status;
                defect.defectNo = defectNo;
                defect.defectName = defectName;
                defect.defectGrade = defectGrade;
                defect.demandID = demandID;
                defect.defectType = defectType;
                defect.caseID = caseID;
                defect.defectDescription = defectDescription;
                defect.finishDatetime = finishDatetime;
                defect.accessory = accessory;
                defect.remark = remark;
                defect.functionModule = functionModule;
                defect.functionPoint = functionPoint;
                defect.yzcd = yzcd;
                defect.devOP = devOP;
                defect.testOP = testOP;
                defect.defectSource = defectSource;
                defect.defectFX = defectFX;
                defect.findDatetime = findDatetime;

                getMsg = DefectManager.saveDefectInfo(defect);
                context.Response.Write(getMsg);
            }
            else if (type == "3")
            {
                int ID = int.Parse(dicParameter["ID"].ToString());
                string defectNo = dicParameter["defectNo"].ToString();
                string defectName = dicParameter["defectName"].ToString();
                getMsg = DefectManager.deleteDefectInfo(ID, defectNo, defectName);
                context.Response.Write(getMsg);
            }
            else if (type == "4")
            {
                int ID = int.Parse(dicParameter["ID"].ToString());
                string defectNo = dicParameter["defectNo"].ToString();
                string defectName = dicParameter["defectName"].ToString();
                getMsg = DefectManager.getDefectDetails(ID, defectNo, defectName);
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