using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAV_TRACE_ONE.Repository
{
    using Dapper;
    using global::Models;
    using Models;
    using System;
    using System.Data.SqlClient;

    namespace Repository
    {
        public class CustomerLogRepository
        {
            private readonly string _connectionString;

            public CustomerLogRepository(
                string connectionString)
            {
                _connectionString = connectionString;
            }

            public void SaveLog(
                Customer customer,
                bool success,
                int httpStatus,
                string requestJson,
                string responseJson,
                string error)
            {
                using (var conn =
                    new SqlConnection(_connectionString))
                {
                    conn.Open();

                    var sql = @"
                INSERT INTO CQ.dbo.TraceOne_Customers
                (
                    Code,
                    Subcode,
                    Name,
                    Street,
                    Postcode,
                    City,
                    StateProvince,
                    Country,
                    DefLanguage,
                    Email,
                    Type,
                    Active,
                    dt_invio,
                    des_esito,
                    http_status,
                    request_body,
                    response_body
                )
                VALUES
                (
                    @Code,
                    @Subcode,
                    @Name,
                    @Street,
                    @Postcode,
                    @City,
                    @StateProvince,
                    @Country,
                    @DefLanguage,
                    @Email,
                    @Type,
                    @Active,
                    GETDATE(),
                    @Esito,
                    @HttpStatus,
                    @RequestBody,
                    @ResponseBody
                )
            ";

                    conn.Execute(sql, new
                    {
                        customer.Code,
                        customer.Subcode,
                        customer.Name,
                        customer.Street,
                        customer.Postcode,
                        customer.City,
                        customer.StateProvince,
                        customer.Country,
                        customer.DefLanguage,
                        customer.Email,
                        customer.Type,
                        customer.Active,

                        Esito = success
                            ? "OK"
                            : error,

                        HttpStatus = httpStatus,

                        RequestBody = requestJson,

                        ResponseBody = responseJson
                    });
                }
            }
        }
    }
}
