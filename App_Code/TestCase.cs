using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CommHelper;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// TestCase 的摘要说明
/// </summary>
public class TestCase
{
	public TestCase()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public class TestCaseRequest {

        public string ID { get;set;}
        public string demandID { get; set; }
        public string demandName { get; set; }
        public string functionModule { get; set; }
        public string functionPoint { get; set; }
        public string caseCode { get; set; }
        public string caseName { get; set; }
        public string precondition { get; set; }
        public string caseType { get; set; }
        public string caseDescription { get; set; }
        public string caseNature { get; set; }
        public string expectedResult { get; set; }
        public string caseEditOP { get; set; }
        public string priority { get; set; }
        public string actualResult { get; set; }
        public string caseOP { get; set; }
        public string caseRemarks { get; set; }
        public string casePic { get; set; }
    }

    public class DemandRequest
    {
        public string demandID { get; set; }
        public string demandName { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }

    #region 获取需求列表

    public static string getDemandList(DemandRequest dr,string sessionName)
    {

        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"erroeMsg\":\"{1}\"}}";

        DataSet ds = new DataSet();

        try
        {
            string sqlStr = @"getDemandList";

            int sqlType = 2; // 1:sql语句，2：存储过程

            /*
             
	            @demandID VARCHAR(500),
	            @demandName VARCHAR(500),
	            @pageIndex INT=1,
	            @pageSize INT=10,
	            @totalRows int OUT,
	            @totalPages INT OUT
             
             */

            int totalRows = 0;

            int totalPages = 0;

            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@demandID",dr.demandID),
                new SqlParameter("@demandName",dr.demandName),
                new SqlParameter("@pageIndex",dr.pageIndex),
                new SqlParameter("@pageSize",dr.pageSize),
                new SqlParameter("@totalRows",totalRows),
                new SqlParameter("@totalPages",totalPages)
            };
            ds = comm.Query(sqlStr, sqlType,sp);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    StringBuilder Json = new StringBuilder();
                    Json.Append("{\"dataList\":[");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Json.Append("{");
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace('"', '”') + "\"");
                                if (j < dt.Columns.Count - 1)
                                {
                                    Json.Append(",");
                                }
                            }
                            Json.Append(",\"caseList\":[]}");
                            if (i < dt.Rows.Count - 1)
                            {
                                Json.Append(",");
                            }
                        }
                    }
                    Json.Append("],\"totalRows\":" + ds.Tables[1].Rows[0][0] + ",\"totalPage\":" + ds.Tables[1].Rows[0][1] + ",\"sessionName\":\"" + sessionName + "\"}");
                    returnData = Json.ToString();// comm.DataTableToJson("dataList", ds.Tables[0]);
                }
                else
                {
                    returnData = string.Format(msg, 0, "暂无数据");
                }
            }
            else
            {
                returnData = string.Format(msg, 0, "暂无数据");
            }

        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, false, ex.Message.ToString());
        }

        return returnData;

    }

    #endregion

    #region 获取需求下的案例
    public static string getFeaturesCaseList(string demandID,string ID)
    {

        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";

        string demandStr1 = "select demandName from demandTable with(nolock) where demandID='" + demandID + "' and ID= '" + ID + "' ";

        DataSet ds2 = comm.Query(demandStr1, 1, null);

        string demandName1 = ds2.Tables[0].Rows[0][0].ToString().Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace('"', '”');

        DataSet ds = new DataSet();

        string demandID1 = demandID;


        try
        {
            string sqlStr = @"SELECT ID,caseCode,caseName FROM dbo.caseTable WITH(NOLOCK ) where demandID='" + demandID + "^" + demandName1 + "' and caseCode is not null and caseCode<>'' order by insertDatetime desc";

            int sqlType = 1; // 1:sql语句，2：存储过程

            ds = comm.Query(sqlStr, sqlType,null);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                   

                    returnData = comm.DataTableToJson("dataList", ds.Tables[0]);
                }
                else
                {
                    returnData = string.Format(msg, 0, "暂无数据");
                }
            }
            else
            {
                returnData = string.Format(msg, 0, "暂无数据");
            }
        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, false, ex.Message.ToString());
        }

        return returnData;

    } 
    #endregion

    #region 获取案例详情
    public static string getCaseDetails(string caseID)
    {

        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";

        DataSet ds = new DataSet();

        try
        {
            string sqlStr = @"SELECT  * FROM dbo.caseTable WITH(NOLOCK ) where ID='" + caseID + "' ";

            int sqlType = 1; // 1:sql语句，2：存储过程

            ds = comm.Query(sqlStr, sqlType,null);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    returnData = comm.DataTableToJson("dataList", ds.Tables[0]);
                }
            }

        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, false, ex.Message.ToString());
        }

        return returnData;

    } 

    #endregion

    #region 添加案例信息

    public static string addCaseInfo(TestCaseRequest tc) {

        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";

        string sqlStr = @"INSERT INTO [AgileTestDemo].[dbo].[caseTable]
                           ([sysName]
                           ,[functionModule]
                           ,[functionPoint]
                           ,[caseCode]
                           ,[caseName]
                           ,[precondition]
                           ,[caseType]
                           ,[caseDescription]
                           ,[caseNature]
                           ,[expectedResult]
                           ,[priority]
                           ,[caseEditOP]
                           ,[actualResult]
                           ,[caseOP]
                           ,[caseRemarks]
                           ,[demandID]
                           ,[projectID])
                     VALUES
                           ('个人网银','" + tc.functionModule + "','" + tc.functionPoint + "','" + tc.caseCode + "' ,'" + tc.caseName + "','" + tc.precondition + "','" + tc.caseType + "' ,'" + tc.caseDescription + "','" + tc.caseNature + "' ,'" + tc.expectedResult + "' ,'" + tc.priority + "','" + tc.caseEditOP + "' ,'" + tc.actualResult + "' ,'" + tc.caseOP + "' ,'" + tc.caseRemarks + "','" + tc.demandID + "^" + tc.demandName + "' ,'个人网银')  ";

        int sqlType = 1; // 1:sql语句，2：存储过程

        int res = comm.Execute(sqlStr, sqlType);

        return string.Format(msg, true, "添加成功");
    }

    #endregion

    #region 更新案例信息

    public static string editCaseInfo(TestCaseRequest tc)
    {
        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";

        string sqlStr = @"UPDATE [AgileTestDemo].[dbo].[caseTable]
                       SET [functionModule] = '" +tc.functionModule
                    +"' ,[functionPoint] = '"+tc.functionPoint
                    +"' ,[caseCode] = '"+tc.caseCode
                    + "' ,[caseName] = '" + tc.caseName
                    + "' ,[precondition] = '" + tc.precondition
                    + "' ,[caseType] = '" + tc.caseType
                    +"' ,[caseDescription] = '"+tc.caseDescription
                    + "' ,[caseNature] = '" + tc.caseNature
                    + "' ,[expectedResult] = '" + tc.expectedResult
                    + "' ,[priority] = '" + tc.priority
                    + "' ,[caseEditOP] = '" + tc.caseEditOP
                    +"' ,[actualResult] = '"+tc.actualResult
                    +" ',[caseOP] = '"+tc.caseOP
                    + "' ,[demandID] = '" + tc.demandID + "^" + tc.demandName
                    + "' ,[caseRemarks] = '" + tc.caseRemarks  
                    + "' WHERE ID='" + tc.ID + "' ";

        int sqlType = 1; // 1:sql语句，2：存储过程

        int res = comm.Execute(sqlStr, sqlType);

        return string.Format(msg, true, "更新成功");
    }

    #endregion

    #region 删除案例

    public static string deleteCaseInfo(string caseID) {

        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";

        try
        {
            string deleteStr = "delete from caseTable where ID='"+caseID+"'";

            int sqlType = 1; // 1:sql语句，2：存储过程

            int res = comm.Execute(deleteStr, sqlType);

            if (res > 0) {
                returnData = string.Format(msg, true, "删除成功"); 
            }

        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, false, ex.Message.ToString());
        }

        return returnData;
    }

    #endregion
}