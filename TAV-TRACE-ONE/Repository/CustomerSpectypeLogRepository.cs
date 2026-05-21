using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAV_TRACE_ONE.Repository
{
    public class CustomerSpectypeLogRepository
    {
        private readonly string _connectionString;

        public CustomerSpectypeLogRepository(
            string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveLog(
            CustomerSpectype item,
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

                INSERT INTO
                CQ.dbo.TraceOne_CustomerSpectypes
                (
                    Action,
                    RqstId,
                    CustomerCode,
                    CustomerSubcode,
                    SpectypeCode,
                    SpecTypeCustom,
                    Languages,

                    dt_invio,
                    des_esito,
                    http_status,
                    request_body,
                    response_body
                )
                VALUES
                (
                    @Action,
                    @RqstId,
                    @CustomerCode,
                    @CustomerSubcode,
                    @SpectypeCode,
                    @SpecTypeCustom,
                    @Languages,

                    GETDATE(),
                    @Esito,
                    @HttpStatus,
                    @RequestBody,
                    @ResponseBody
                )
                ";

                conn.Execute(sql, new
                {
                    item.Action,
                    item.RqstId,
                    item.CustomerCode,
                    item.CustomerSubcode,
                    item.SpectypeCode,
                    item.SpecTypeCustom,
                    item.Languages,

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
