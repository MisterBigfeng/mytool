<%@ WebHandler Language="C#" Class="TestCaseMsg" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using CommHelper;

public class TestCaseMsg : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        string getMsg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";

        try
        {
            //判断session用户是否存在
            //if (context.Session["userName"] == null || context.Session["userName"].ToString() == "")
            //{
            //    string resStr = string.Format(getMsg, "-1", "请重新登录");
            //    context.Response.Write(resStr);
            //    return;
            //}

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

            //获取当前需求下的案例信息
            else if (type == "2")
            {

                string demandID = dicParameter["demandID"].ToString();
                string ID = dicParameter["ID"].ToString();

                getMsg = TestCase.getFeaturesCaseList(demandID,ID);

                context.Response.Write(getMsg);

            }
            //获取案例详情
            else if (type == "3")
            {

                string caseID = dicParameter["caseID"].ToString();

                getMsg = TestCase.getCaseDetails(caseID);

                context.Response.Write(getMsg);

            }
            //添加案例
            else if (type == "4")
            {

                string caseID = dicParameter["caseID"].ToString();
                string demandID = dicParameter["demandID"].ToString();
                string demandName = dicParameter["demandName"].ToString();
                string caseCode = dicParameter["caseCode"].ToString();
                string caseName = dicParameter["caseName"].ToString();
                string functionModule = dicParameter["functionModule"].ToString();
                string functionPoint = dicParameter["functionPoint"].ToString();
                string caseNature = dicParameter["caseNature"].ToString();
                string caseType = dicParameter["caseType"].ToString();
                string caseDescription = dicParameter["caseDescription"].ToString();
                string expectedResult = dicParameter["expectedResult"].ToString();
                string caseOP = context.Request.Cookies["userName"].Value.Split('=')[1].ToString();
                string actualResult = dicParameter["actualResult"].ToString();
                string caseRemarks = dicParameter["caseRemarks"].ToString();
                string casePic = dicParameter["casePic"].ToString();
                string operateType = dicParameter["operateType"].ToString();
                string precondition = dicParameter["precondition"].ToString();
                string priority = dicParameter["priority"].ToString();
                TestCase.TestCaseRequest tc = new TestCase.TestCaseRequest();

                tc.ID = caseID;
                tc.demandID = demandID;
                tc.demandName = demandName;
                tc.caseCode = caseCode;
                tc.caseName = caseName;
                tc.functionModule=functionModule;
                tc.functionPoint =functionPoint;
                tc.caseNature = caseNature;
                tc.caseType = caseType;
                tc.caseDescription = caseDescription;
                tc.expectedResult=expectedResult;
                tc.caseOP = caseOP;
                tc.actualResult =actualResult;
                tc.caseRemarks = caseRemarks;
                tc.precondition = precondition;
                tc.priority = priority;
                if (caseRemarks == "0")
                {
                    tc.caseRemarks = "";
                }
                if (casePic == "0")
                {
                    tc.casePic = "";
                }
                
                if (operateType == "add")
                {
                    getMsg = TestCase.addCaseInfo(tc);
                    context.Response.Write(getMsg);
                }
                else if (operateType == "edit")
                {
                    getMsg = TestCase.editCaseInfo(tc);
                    context.Response.Write(getMsg);
                }
            }
            //删除案例
            else if (type == "5")
            {

                string caseID = dicParameter["caseID"].ToString();

                getMsg = TestCase.deleteCaseInfo(caseID);

                context.Response.Write(getMsg);

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