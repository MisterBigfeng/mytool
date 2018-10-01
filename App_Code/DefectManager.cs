using CommHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// DefectManager 的摘要说明
/// </summary>
public class DefectManager
{
	public DefectManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public class DefectRequest
    {
        public string status { get; set; }
        public string defectNo { get; set; }
        public string defectName { get; set; }
        public string defectGrade { get; set; }
        public string demandID { get; set; }
        public string defectType { get; set; }
        public string caseID { get; set; }
        public string defectDescription { get; set; }
        public string finishDatetime { get; set; }
        public string accessory { get; set; }
        public string remark { get; set; }
        public string functionModule { get; set; }
        public string functionPoint { get; set; }
        public string yzcd { get; set; }
        public string devOP { get; set; }
        public string testOP { get; set; }
        public string defectSource { get; set; }
        public string defectFX { get; set; }
        public string findDatetime { get; set; }
        //public string defectName { get; set; }
        //public string defectNo { get; set; }
        //public string demandID { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }

    public static string getDefectList(DefectRequest dr, string sessionName)
    {

        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";

        DataSet ds = new DataSet();

        try
        {
            string sqlStr = @"getDefectList";

            int sqlType = 2; // 1:sql语句，2：存储过程

            /*
             
	@defectName VARCHAR(500),
	@defectNo varchar(500),
	@demandID VARCHAR(500),
	@pageIndex INT=1,
	@pageSize INT=10,
	@totalRows int OUT,
	@totalPages INT OUT
             
             */

            int totalRows = 0;

            int totalPages = 0;

            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@defectName",dr.defectName),
                new SqlParameter("@defectNo",dr.defectNo),
                new SqlParameter("@demandID",dr.demandID),
                new SqlParameter("@pageIndex",dr.pageIndex),
                new SqlParameter("@pageSize",dr.pageSize),
                new SqlParameter("@totalRows",totalRows),
                new SqlParameter("@totalPages",totalPages)
            };
            ds = comm.Query(sqlStr, sqlType, sp);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    StringBuilder Json = new StringBuilder();
                    Json.Append("{\"dataList\":[");
                    if (dt.Rows.Count > 0)
                    {
                        returnData =comm.DataTableToJson("dataList", ds.Tables[0]);
                    }
                   
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

    public static string saveDefectInfo(DefectRequest defect)
    {
        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";
        string sqlStr = @"INSERT INTO [AgileTestDemo].[dbo].[defectTable]
            (
            [status]
          ,[defectNo]
          ,[defectName]
          ,[defectGrade]
          ,[demandID]
          ,[defectType]
          ,[caseID]
          ,[defectDescription]
          ,[finishDatetime]
          ,[accessory]
          ,[remark]
          ,[functionModule]
          ,[functionPoint]
          ,[yzcd]
          ,[devOP]
          ,[testOP]
          ,[defectSource]
          ,[defectFX]
          ,[findDatetime])
     VALUES
           (
            '" + defect.status             +@"'            
           ,'" + defect.defectNo           +@"'    
           ,'" + defect.defectName         +@"'    
           ,'" + defect.defectGrade        +@"'    
           ,'" + defect.demandID           +@"'    
           ,'" + defect.defectType         +@"'    
           ,'" + defect.caseID             +@"'    
           ,'" + defect.defectDescription  +@"'    
           ,'" + defect.finishDatetime     +@"'    
           ,'" + defect.accessory          +@"'    
           ,'" + defect.remark             +@"'    
           ,'" + defect.functionModule     +@"'    
           ,'" + defect.functionPoint      +@"'    
           ,'" + defect.yzcd               +@"'    
           ,'" + defect.devOP              +@"'    
           ,'" + defect.testOP             +@"'    
           ,'" + defect.defectSource       +@"'    
           ,'" + defect.defectFX           +@"'    
           ,'" + defect.findDatetime + @"'  
)";


        int sqlType = 1; // 1:sql语句，2：存储过程

        int res = comm.Execute(sqlStr, sqlType);

        return string.Format(msg, true, "添加成功");
    }
    public static string getDefectDetails(int ID, string defectNo, string defectName)
    {
        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";

        DataSet ds = new DataSet();

        int sqlType = 1; // 1:sql语句，2：存储过程
        try
        {
            string sqlStr = @"select * from defectTable  where ID=" + ID + " and defectNo='" + defectNo + "'and defectName='" + defectName + "'";

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

    public static string deleteDefectInfo(int ID, string defectNo, string defectName)
    {
        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"errorMsg\":\"{1}\"}}";

        int sqlType = 1; // 1:sql语句，2：存储过程

        string sqlStr = @"delete from defectTable where ID=" + ID + " and defectNo='" + defectNo + "'and defectName='" + defectName + @"';";

        int res = comm.Execute(sqlStr, sqlType);

        return string.Format(msg, true, "删除成功");
        
    }


}