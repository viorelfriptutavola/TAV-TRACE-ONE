using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAV_TRACE_ONE.Models;

namespace TAV_TRACE_ONE.Repository
{
    public class TraceOneMaterialRepository
    {
        private readonly string _connectionString;

        public TraceOneMaterialRepository(
            string connectionString)
        {
            _connectionString =
                connectionString;
        }

        public void SaveMaterials(
            MaterialExportResponse response)
        {
            using (var conn =
                new SqlConnection(
                    _connectionString))
            {
                conn.Open();

                foreach (var material
                    in response.Materials)
                {
                    SaveMaterial(
                        conn,
                        material
                    );

                    SaveScalarAttributes(
                        conn,
                        material
                    );

                    SaveTableAttributes(
                        conn,
                        material
                    );
                }
            }
        }

        private void SaveMaterial(
            SqlConnection conn,
            MaterialDto material)
        {
            var sql = @"

            INSERT INTO
            CQ.dbo.TraceOne_Materials
            (
                Plant,
                Code,
                Prefix,
                Description,
                Type,
                JsonData,
                dt_import
            )
            VALUES
            (
                @Plant,
                @Code,
                @Prefix,
                @Description,
                @Type,
                @JsonData,
                GETDATE()
            )

            ";

            conn.Execute(sql, new
            {
                material.Plant,

                material.Code,

                material.Prefix,

                material.Description,

                material.Type,

                JsonData =
                    JsonConvert.SerializeObject(
                        material
                    )
            });
        }

        private void SaveScalarAttributes(
            SqlConnection conn,
            MaterialDto material)
        {
            if (
                material.Attributes?
                    .ScalarAttributes == null
            )
            {
                return;
            }

            var sql = @"

            INSERT INTO
            CQ.dbo.TraceOne_MaterialScalarAttributes
            (
                MaterialCode,
                AttributeName,
                AttributeValue,
                dt_import
            )
            VALUES
            (
                @MaterialCode,
                @AttributeName,
                @AttributeValue,
                GETDATE()
            )

            ";

            foreach (var attr
                in material.Attributes
                    .ScalarAttributes)
            {
                conn.Execute(sql, new
                {
                    MaterialCode =
                        material.Code,

                    AttributeName =
                        attr.Name,

                    AttributeValue =
                        attr.Value
                });
            }
        }

        private void SaveTableAttributes(
            SqlConnection conn,
            MaterialDto material)
        {
            if (
                material.Attributes?
                    .TableAttributes == null
            )
            {
                return;
            }

            var sql = @"

            INSERT INTO
            CQ.dbo.TraceOne_MaterialTableAttributes
            (
                MaterialCode,
                TableName,
                RowNumber,
                ColumnName,
                ColumnValue,
                dt_import
            )
            VALUES
            (
                @MaterialCode,
                @TableName,
                @RowNumber,
                @ColumnName,
                @ColumnValue,
                GETDATE()
            )

            ";

            foreach (var table
                in material.Attributes
                    .TableAttributes)
            {
                foreach (var row
                    in table.Rows)
                {
                    for (
                        int i = 0;
                        i < row.Cells.Count;
                        i++
                    )
                    {
                        string columnName =
                            table.Columns[i].Name;

                        string columnValue =
                            row.Cells[i].Value;

                        conn.Execute(sql, new
                        {
                            MaterialCode =
                                material.Code,

                            TableName =
                                table.Name,

                            RowNumber =
                                row.RowNumber,

                            ColumnName =
                                columnName,

                            ColumnValue =
                                columnValue
                        });
                    }
                }
            }
        }
    }
}
