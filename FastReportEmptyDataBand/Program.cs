using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;

public class Program
{
    private static void Main(string[] args)
    {
        string connStr = @"連線字串";//替換連線字串
        using (var cn = new OracleConnection(connStr))
        {
            string queryUser = @"SELECT * FROM SECUUSER WHERE GROUP_CODE = 'LCCC' AND is_checker = 'N'";
            var queryUserResult = cn.Query<dynamic>(queryUser);
            if (queryUserResult.Count() == 0)
            {
                var dr = cn.ExecuteReader(queryUser);
                var dt = new DataTable();
                dt.Load(dr);
                List<string> cols = GetDataTableProperty(dt);
                SimulateUICombineDataTable(cols);
            }
            else
            {
                Console.WriteLine("有資料");
            }
        }
    }
    /// <summary>
    /// 取得每個DataTable Column Name
    /// </summary>
    /// <param name="dt"></param>
    private static List<string> GetDataTableProperty(DataTable dt)
    {
        List<string> cols = new List<string>();
        foreach (var columnName in dt.Columns)
        {
            Console.WriteLine($"DataTable欄位: {columnName}");
            cols.Add(columnName.ToString());
        }
        return cols;
    }
    /// <summary>
    /// 模擬UI將Column組回DataTable
    /// </summary>
    private static void SimulateUICombineDataTable(List<string> cols)
    {
        DataTable dt = new DataTable();
        foreach (string item in cols)
        {
            dt.Columns.Add(item);
        }
    }
}