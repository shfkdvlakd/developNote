// Program.cs
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace ExcelToJsonExample
{
    class Program
    {
        static void Main()
        {
            // 1) 엑셀 파일 경로와 OLE DB 연결 문자열 구성
            string filePath = @"C:\path\to\your\file.xlsx";
            // Provider=Microsoft.ACE.OLEDB.12.0 : .xlsx driver
            // HDR=YES : 1행을 컬럼 이름으로 사용.
            // IMEX=1 : Import/Export 모드로 동작, 모두 문자열로 읽어서 가져오기.
            string excelConn = $@"
                Provider=Microsoft.ACE.OLEDB.12.0; 
                Data Source={filePath};
                Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1"";";

            // 2) 엑셀 → DataTable
            var table = new DataTable();
            using (var conn = new OleDbConnection(excelConn))
            {
                conn.Open();
                string sheet = "Sheet1$";
                string query = $"SELECT * FROM [{sheet}]";
                using (var adapter = new OleDbDataAdapter(query, conn))
                {
                    adapter.Fill(table);
                }
            }

            // 3) DataTable → JSON 문자열
            string json = DataTableToJSON(table);

            // 4) SQL Server에 JSON 넘겨주기
            //    예: 프로시저 [dbo].[UNIV_MAJOR_UPDATE] 의 @in_JsonValue 파라미터로 사용
            string sqlConn = @"Server=YOUR_SQL_SERVER;Database=YOUR_DB;User Id=...;Password=...;";
            using (var conn = new SqlConnection(sqlConn))
            using (var cmd  = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.UNIV_MAJOR_UPDATE"; // 호출할 프로시저 이름

                // SqlDbType.NVarChar, -1 은 NVARCHAR(MAX) 와 동일
                cmd.Parameters.Add(new SqlParameter("@in_JsonValue", SqlDbType.NVarChar, -1) {
                    Value = json
                });

                // 예: 출력 파라미터가 있으면 추가로 정의 ex) return : status,code,msg...
                // cmd.Parameters.Add(new SqlParameter("@out_ErrCode", SqlDbType.VarChar, 50) {
                //     Direction = ParameterDirection.Output
                // });

                cmd.ExecuteNonQuery();

                // 예: 출력 파라미터 읽기
                // var errCode = cmd.Parameters["@out_ErrCode"].Value;
            }

            Console.WriteLine("완료: JSON 데이터 전송 성공");
        }

        /// <summary>
        /// DataTable을 JSON 문자열로 직렬화
        /// </summary>
        private static string DataTableToJSON(DataTable dt)
        {
            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = int.MaxValue
            };

            var list = new List<Dictionary<string, object>>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>(dt.Columns.Count);
                // column 값이 비어 있으면 null, 있으면 값 넣기.
                foreach (DataColumn col in dt.Columns)
                {
                    row[col.ColumnName] = dr[col] is DBNull ? null : dr[col];
                }
                list.Add(row);
            }

            return serializer.Serialize(list);
        }
    }
}
