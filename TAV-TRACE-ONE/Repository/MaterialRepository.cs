using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAV_TRACE_ONE.Repository
{
    public class MaterialRepository
    {
        private readonly string _connectionString;

        public MaterialRepository(
            string connectionString)
        {
            _connectionString =
                connectionString;
        }

        public List<MaterialExportItemDto>
            GetMaterialsToExport()
        {
            using (var conn =
                new SqlConnection(
                    _connectionString))
            {
                conn.Open();

                var sql = @"

                SELECT DISTINCT
                    Plant,
                    Matcode AS Code
                FROM CQ.dbo.TraceOne_MatCust_DaInviare

                WHERE
                    Matcode IS NOT NULL
                    AND Matcode <> ''

                ";

                return conn
                    .Query<
                        MaterialExportItemDto>(
                            sql
                        )
                    .ToList();
            }
        }
    }

    public class MaterialExportItemDto
    {
        public string Plant { get; set; }

        public string Code { get; set; }
    }
}
