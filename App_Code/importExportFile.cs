using CommHelper;
using NetUtilityLib;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// importExportFile 的摘要说明
/// </summary>
public class importExportFile
{
    public importExportFile()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    static DataTable GenerateData()
    {
        DataTable data = new DataTable();
        for (int i = 0; i < 5; ++i)
        {
            data.Columns.Add("Columns_" + i.ToString(), typeof(string));
        }

        for (int i = 0; i < 10; ++i)
        {
            DataRow row = data.NewRow();
            row["Columns_0"] = "item0_" + i.ToString();
            row["Columns_1"] = "item1_" + i.ToString();
            row["Columns_2"] = "item2_" + i.ToString();
            row["Columns_3"] = "item3_" + i.ToString();
            row["Columns_4"] = "item4_" + i.ToString();
            data.Rows.Add(row);
        }
        return data;
    }

    public static string PrintData(DataTable data, string demandID,int ID)
    {
        if (data == null) return null;

        string res = "";

        try
        {
            StringBuilder sb = new StringBuilder();
            int currentIndex = 1;
            int maxCount = 50;

            using (SqlConnection conn = new SqlConnection(comm.conStr))
            {
                conn.Open();
                if (demandID == null)
                {
                    #region 插入需求数据库
                    foreach (DataRow dr in data.Rows)
                    {
                        demandID = dr[0].ToString().Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace('"', '”');
                        string demandName = dr[1].ToString().Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace('"', '”');
                        string devOP = dr[2].ToString();
                        string sitTestTime = dr[3].ToString();
                        string testOP = dr[4].ToString();
                        string sitComplatetime = dr[5].ToString();
                        string bankOP = dr[6].ToString();
                        string uatTestDatetime = dr[7].ToString();
                        string uatTestOP = dr[8].ToString();
                        string uatFinishDatetime = dr[9].ToString();
                        string actualComplateTime = dr[10].ToString();
                        string submitConfirmTime = dr[11].ToString();
                        string status = dr[12].ToString();
                        string estmateProductionTime = dr[13].ToString();
                        string delayLength = dr[14].ToString();
                        string delayReason = dr[15].ToString();
                        string uatTestIsComplate = dr[16].ToString();
                        string remark = ""; //dr[17].ToString();
                        string sysName = "个人网银";

                        if (currentIndex == 1)
                        {
                            sb.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')", demandID, demandName, devOP, sitTestTime, testOP, sitComplatetime, bankOP, uatTestDatetime, uatTestOP, uatFinishDatetime, actualComplateTime, submitConfirmTime, status, estmateProductionTime, delayLength, delayReason, uatTestIsComplate, remark, sysName);
                        }
                        else
                        {
                            sb.AppendFormat(",('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')", demandID, demandName, devOP, sitTestTime, testOP, sitComplatetime, bankOP, uatTestDatetime, uatTestOP, uatFinishDatetime, actualComplateTime, submitConfirmTime, status, estmateProductionTime, delayLength, delayReason, uatTestIsComplate, remark, sysName);
                        }
                        if (currentIndex == maxCount)
                        {
                            #region 插入数据
                            string insertdDemandStr = @"INSERT  [demandTable]
                                               ([demandID]
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
                                                ,[estmateProductionTime]
                                                ,[delayLength]
                                                ,[delayReason]
                                                ,[uatTestIsComplate]
                                                ,[remark]
                                                ,[sysName])
                                         VALUES " + sb.ToString();

                            SqlCommand cmd = new SqlCommand(insertdDemandStr, conn);
                            int e = cmd.ExecuteNonQuery();
                            if (e > 0) { res = "导入成功"; }
                            #endregion
                            sb.Clear();
                            currentIndex = 0;
                        }
                        currentIndex++;
                    }
                    if (sb.Length > 0)
                    {
                        #region 插入剩余数据
                        string insertdDemandStr = @"INSERT  [demandTable]
                                               ([demandID]
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
                                                ,[estmateProductionTime]
                                                ,[delayLength]
                                                ,[delayReason]
                                                ,[uatTestIsComplate]
                                                ,[remark]
                                                ,[sysName])
                                         VALUES " + sb.ToString();
                        #endregion

                        SqlCommand cmd = new SqlCommand(insertdDemandStr, conn);
                        int e = cmd.ExecuteNonQuery();
                        if (e > 0) { res = "导入成功"; }
                    }
                    #endregion
                }
                else
                {
                    if (ID == 0)
                    {
                        #region 插入案例数据库

                        foreach (DataRow dr in data.Rows)
                        {
                            string sysName = dr[0].ToString();//系统名称
                            string functionModule = dr[1].ToString();//功能模块
                            string functionPoint = dr[2].ToString();//功能点
                            string caseCode = dr[3].ToString();//案例编号
                            string caseName = dr[4].ToString();//案例名称
                            string precondition = dr[5].ToString().Replace(" ", "").Replace("\n", "^");//前置条件
                            string caseType = dr[6].ToString();//案例类型
                            string caseDescription = dr[7].ToString().Replace(" ", "").Replace("\n", "^");//案例描述
                            string caseNature = dr[8].ToString();//案例性质
                            string expectedResult = dr[9].ToString().Replace(" ", "").Replace("\n", "^");//预期结果
                            string priority = dr[10].ToString();//优先级
                            string caseEditOP = dr[11].ToString();//案例编写人
                            string actualResult = dr[12].ToString().Replace(" ", "").Replace("\n", "^");//实际结果
                            string caseOP = dr[13].ToString();//案例执行人
                            string caseRemarks = dr[14].ToString();//备注
                            string demandID1 = dr[15].ToString();//demandID
                            string demandName1 = dr[16].ToString().Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace('"', '”');//demandName
                            string projectID = "1";
                            /*
    系统名称	功能模块	功能点	案例编号	案例名称 前置条件	测试类型	案例描述	案例性质	预期结果  优先级	案例编写人	实际结果	案例执行人 备注	demandID	demandName
                    
                             */

                            if (currentIndex == 1)
                            {
                                sb.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},'{12}','{13}','{14}','{15}','{16}')", sysName, functionModule, functionPoint, caseCode, caseName,precondition, caseType,caseDescription, caseNature, expectedResult,priority, caseEditOP, actualResult, caseOP,caseRemarks, "'" + demandID1 + "^" + demandName1 + "'", projectID);
                            }
                            else
                            {
                                sb.AppendFormat(",('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},'{12}','{13}','{14}','{15}','{16}')", sysName, functionModule, functionPoint, caseCode, caseName, precondition, caseType, caseDescription, caseNature, expectedResult, priority, caseEditOP, actualResult, caseOP, caseRemarks, "'" + demandID1 + "^" + demandName1 + "'", projectID);
                            }
                            if (currentIndex == maxCount)
                            {
                                #region 插入数据
                                string insertCaseStr = @"INSERT  [caseTable]
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
                                               ,[projectID]
                                         VALUES " + sb.ToString();

                                SqlCommand cmd = new SqlCommand(insertCaseStr, conn);
                                int e = cmd.ExecuteNonQuery();
                                if (e > 0) { res = "导入成功"; }

                                #endregion
                                sb.Clear();
                                currentIndex = 0;
                            }
                            currentIndex++;
                        }
                        if (sb.Length > 0)
                        {
                            #region 插入剩余数据
                            string insertCaseStr = @"INSERT  [caseTable]
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
                                               ,[projectID]
                                         VALUES " + sb.ToString();
                            #endregion

                            SqlCommand cmd = new SqlCommand(insertCaseStr, conn);
                            int e = cmd.ExecuteNonQuery();
                            if (e > 0) { res = "导入成功"; }
                        }
                        #endregion
                    }
                    else
                    {
                        //string demandStr1 = "select demandName from demandTable with(nolock) where demandID='"+demandID+"'";

                        string demandStr1 = "select demandName from demandTable with(nolock) where demandID='" + demandID + "' and ID= '" + ID + "' ";
                        DataSet ds1 = comm.Query(demandStr1, 1, null);

                        string demandName1 = ds1.Tables[0].Rows[0][0].ToString().Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace('"', '”');



                        #region 插入案例数据库

                        foreach (DataRow dr in data.Rows)
                        {
                            string sysName = dr[0].ToString();
                            string functionModule = dr[1].ToString();
                            string functionPoint = dr[2].ToString();
                            string caseCode = dr[3].ToString();
                            string caseName = dr[4].ToString();
                            string precondition = dr[5].ToString().Replace(" ", "").Replace("\n", "^");//前置条件
                            string caseType = dr[6].ToString();
                            string caseDescription = dr[7].ToString().Replace(" ", "").Replace("\n", "^");
                            string caseNature = dr[8].ToString();
                            string expectedResult = dr[9].ToString().Replace(" ", "").Replace("\n", "^");

                            string priority = dr[10].ToString();//优先级
                            string caseEditOP = dr[11].ToString();
                            string actualResult = dr[12].ToString().Replace(" ", "").Replace("\n", "^");
                            string caseOP = dr[13].ToString();
                            string caseRemarks = dr[14].ToString();//备注
                            string projectID = "1";


                            if (currentIndex == 1)
                            {
                                sb.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')", sysName, functionModule, functionPoint, caseCode, caseName, precondition, caseType, caseDescription, caseNature, expectedResult, priority, caseEditOP, actualResult, caseOP, caseRemarks, "" + demandID + "^" + demandName1 + "", projectID);
                            }
                            else
                            {
                                sb.AppendFormat(",('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')", sysName, functionModule, functionPoint, caseCode, caseName, precondition, caseType, caseDescription, caseNature, expectedResult, priority, caseEditOP, actualResult, caseOP, caseRemarks, "" + demandID + "^" + demandName1 + "", projectID);
                            }
                            if (currentIndex == maxCount)
                            {
                                #region 插入数据
                                string insertCaseStr = @"INSERT  [caseTable]
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
                                         VALUES " + sb.ToString();

                                SqlCommand cmd = new SqlCommand(insertCaseStr, conn);
                                int e = cmd.ExecuteNonQuery();
                                if (e > 0) { res = "导入成功"; }

                                #endregion
                                sb.Clear();
                                currentIndex = 0;
                            }
                            currentIndex++;
                        }
                        if (sb.Length > 0)
                        {
                            #region 插入剩余数据
                            string insertCaseStr = @"INSERT  [caseTable]
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
                                         VALUES " + sb.ToString();
                            #endregion

                            SqlCommand cmd = new SqlCommand(insertCaseStr, conn);
                            int e = cmd.ExecuteNonQuery();
                            if (e > 0) { res = "导入成功"; }
                        }
                        #endregion
                    }
                }
                conn.Close();

            }
        }
        catch (Exception ex)
        {
            res = ex.Message.ToString();
        }
        return res;
    }

    static void TestExcelWrite(string file)
    {
        try
        {
            using (ExcelHelper excelHelper = new ExcelHelper(file))
            {
                DataTable data = GenerateData();
                int count = excelHelper.DataTableToExcel(data, "MySheet", true);
                if (count > 0)
                    Console.WriteLine("Number of imported data is {0} ", count);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }
    }

   public static string TestExcelRead(string file, string demandID,int ID)
    {
        string res = "";
        DataTable dt = new DataTable();
        try
        {
            using (ExcelHelper excelHelper = new ExcelHelper(file))
            {
                dt = excelHelper.ExcelToDataTable("MySheet", true);
                res = PrintData(dt, demandID,ID);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }
        return res;
    }


    /// <summary>
    /// 导出到Excel
    /// </summary>
    /// <param name="exportToExcel"></param>
    /// <returns></returns>

    public static void exportToExcel(string arrayData, HttpContext context, string type)
    {

        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"erroeMsg\":\"{1}\"}}";

        try
        {
            int len = arrayData.Split('|').Length;

            string str = "";

            string sqlStr = "getDemandMsg";

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

                sqlStr = @" SELECT TOP 1000 [ID]
                      ,[sysName] 系统名称
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
                      ,[insertDatetime] 编写时间
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
            
            ExcelRender.RenderToExcel(table, context, "案例.xls");

        }
        catch (Exception ex)
        {
            returnData = string.Format(msg, false, ex.Message.ToString());
        }

        //return returnData;// +fileName;
    }
    

}