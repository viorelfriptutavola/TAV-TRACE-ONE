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
    public class MaterialCustomerLogRepository
    {
        private readonly string _connectionString;

        public MaterialCustomerLogRepository(
            string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveLog(
            MaterialCustomer item,
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
                CQ.dbo.TraceOne_MaterialCustomers
                (
                    Action,
                    RqstId,
                    Plant,
                    Matcode,
                    CustomerCode,
                    CustomerSubcode,
                    SoldDate,
                    DeliveryCode,
                    Brand,
                    OrderNumber,

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
                    @Plant,
                    @Matcode,
                    @CustomerCode,
                    @CustomerSubcode,
                    @SoldDate,
                    @DeliveryCode,
                    @Brand,
                    @OrderNumber,

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
                    item.Plant,
                    item.Matcode,
                    item.CustomerCode,
                    item.CustomerSubcode,
                    item.SoldDate,
                    item.DeliveryCode,
                    item.Brand,
                    item.OrderNumber,

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
