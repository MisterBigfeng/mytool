using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CommHelper;

/// <summary>
/// Features 的摘要说明
/// </summary>
public class Features
{
	public Features()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    #region 获取需求列表

    public static string getFeaturesList()
    {

        string returnData = string.Empty;

        string msg = "{{\"code\":\"{0}\",\"msg\":\"{1}\"}}";

        DataSet ds = new DataSet();

        try
        {
            string sqlStr = @"SELECT TOP 10 *  FROM dbo.Features WITH(NOLOCK);";

            int sqlType = 1; // 1:sql语句，2：存储过程

            ds = comm.Query(sqlStr, sqlType,null);

            if (ds.Tables.Count > 0) {
                if (ds.Tables[0].Rows.Count > 0) {
                    returnData = comm.DataTableToJson("dataList", ds.Tables[0]);
                }
            }
            
        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, 0, ex.Message.ToString());
        }

        return returnData;

    }

    #endregion

    public static string getFeaturesCaseList(string featureID) {

        string returnData = string.Empty;

        string msg = "{{\"code\":\"{0}\",\"msg\":\"{1}\"}}";

        DataSet ds = new DataSet();

        try
        {
            string sqlStr = @"SELECT TC_ID,Name,Description,Summary FROM dbo.TestCase WITH(NOLOCK ) WHERE FeatureId='"+ featureID+"'";

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
            returnData = string.Format(msg, 0, ex.Message.ToString());
        }

        return returnData;

    }

    public static string saveDemandInfo(FeaturesRequest features)
    {

        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";
        string sqlStr = @"INSERT INTO [AgileTestDemo].[dbo].[demandTable]
            (
            [demandID]
           ,[demandName]
           ,[devOP]
           ,[sitTestTime]
           ,[testOP]
           ,[sitComplatetime]
           ,[bankOP]
           ,[uatTestDatetime]
           ,[uatTestOP]
           ,[uatFinishDatetime]
           ,[actualComplateTime]
           ,[submitConfirmTime]
           ,[status]
           ,[estmateComplateTime]
           ,[estmateProductionTime]
           ,[delayLength]
           ,[delayReason]
           ,[uatTestIsComplate]
           ,[remark]
           ,[Totaltestcase]
           ,[Totaltestcasepass]
           ,[Todaytestcasepass]
           ,[Totalbug]
           ,[Notrepairedbug]
           ,[sysName])
     VALUES
           (
            '" + features.demandID+@"'
           ,'"+features.demandName+@"'
           ,'"+features.devOP+@"'
           ,'"+features.sitTestTime+@"'
           ,'"+features.testOP+@"'
           ,'"+features.sitComplatetime+@"'
           ,'"+features.bankOP+@"'
           ,'"+features.uatTestDatetime+@"'
           ,'"+features.uatTestOP+@"'
           ,'"+features.uatFinishDatetime+@"'
           ,'"+features.actualComplateTime+@"'
           ,'"+features.submitConfirmTime+@"'
           ,'"+features.status+@"'
           ,'"+features.estmateComplateTime+@"'
           ,'"+features.estmateProductionTime+@"'
           ,null
           ,null
           ,null
           ,'"+features.remark+ @"'

           ,'" + features.Totaltestcase + @"'
           ,'" + features.Totaltestcasepass + @"'
           ,'" + features.Todaytestcasepass + @"'
           ,'" + features.Totalbug + @"'
           ,'" + features.Notrepairedbug + @"'
           ,'个人网银')";


        int sqlType = 1; // 1:sql语句，2：存储过程

        int res = comm.Execute(sqlStr, sqlType);

        return string.Format(msg, true, "添加成功");

    }

    public  static string  deleteDemandInfo(int ID, string demandID, string demandName){

        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";

        int sqlType = 1; // 1:sql语句，2：存储过程

        string sqlStr = @"delete from demandTable where ID=" + ID + " and demandID='" + demandID + "'and demandName='" + demandName + @"';
                            delete from caseTable where demandID='" + demandID + "^" + demandName + "';";

        int res = comm.Execute(sqlStr, sqlType);

        return string.Format(msg, true, "删除成功");
    }
    public static string getDemandDetails(int ID, string demandID, string demandName)
    {

        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";

        DataSet ds = new DataSet();

        int sqlType = 1; // 1:sql语句，2：存储过程
        try
        {
            string sqlStr = @"select * from demandTable  where ID=" + ID + " and demandID='" + demandID + "'and demandName='" + demandName  + "'";

            ds = comm.Query(sqlStr, sqlType, null);

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
            returnData = string.Format(msg, 0, ex.Message.ToString());
        }


        return returnData;
    }

    public class FeaturesRequest
    {
        public string demandID { get; set; }
        public string actualComplateTime{get;set;}
        public string bankOP{get;set;}
        public string demandName{get;set;}
        public string demandType{get;set;}
        public string devOP{get;set;}
        public string estmateComplateTime{get;set;}
        public string estmateProductionTime{get;set;}
        public string kjb{get;set;}
        public string remark{get;set;}
        public string 	sitComplatetime{get;set;}
        public string 	sitTestTime{get;set;}
        public string 	status{get;set;}
        public string 	submitConfirmTime{get;set;}
        public string 	testOP{get;set;}
        public string 	uatFinishDatetime{get;set;}
        public string 	uatTestDatetime{get;set;}
        public string uatTestOP { get; set; }

        public string Totaltestcase { get; set; }
        public string Totaltestcasepass { get; set; }
        public string Todaytestcasepass { get; set; }
        public string Totalbug { get; set; }
        public string Notrepairedbug { get; set; }
    }


}