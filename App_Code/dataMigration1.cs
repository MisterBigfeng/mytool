using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CommHelper;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// dataMigration1 的摘要说明
/// </summary>
public class dataMigration1
{
	public dataMigration1()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static string dataMigrationMsg()
    {

        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"erroeMsg\":\"{1}\"}}";

        DataSet ds = new DataSet();

        try
        {
            string sqlStr = @"dataMigration";

            int sqlType = 2; // 1:sql语句，2：存储过程

            ds = comm.Query(sqlStr, sqlType,null);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string insDemandStr = "";

                    importExportFile.PrintData(ds.Tables[0], null, 0);
                }
                //if (ds.Tables[1].Rows.Count > 0)
                //{
                //    //importExportFile.PrintData(ds.Tables[1], "", 0);
                //}
            }

        }
        catch (Exception ex)
        {
            
            throw;
        }

        return "";

    }

    public static string dataMigrationCaseMsg() {
        string returnData = string.Empty;

        string msg = "{{\"success\":\"{0}\",\"erroeMsg\":\"{1}\"}}";

        DataSet ds = new DataSet();

        try
        {
            string sqlStr = @"dataMigrationCase";

            int sqlType = 2; // 1:sql语句，2：存储过程

            ds = comm.Query(sqlStr, sqlType, null);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string insDemandStr = "";

                    importExportFile.PrintData(ds.Tables[0], "", 0);
                }

            }

        }
        catch (Exception ex)
        {

            throw;
        }

        return "";
    }

}