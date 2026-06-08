using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAV_TRACE_ONE.Repository.Repository;

namespace TAV_TRACE_ONE.Repository
{
    public class MaterialCustomerRepository
    {
        private readonly string _connectionString;

        public MaterialCustomerRepository(
            string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<MaterialCustomerDto>
            GetMaterialCustomersToSend()
        {
            using (var conn =
                new SqlConnection(_connectionString))
            {
                conn.Open();

                var sql = @"

                SELECT
                    Action,
                    RqstId,
                    Plant,
                    Matcode,
                    CustomerCode,
                    CustomerSubcode,
                    SoldDate,
                    DeliveryCode,
                    Brand,
                    OrderNumber
                FROM CQ.dbo.TraceOne_MatCust_DaInviare
                where Action='Create' and [art_trace]!=null

                ";

                return conn
                    .Query<MaterialCustomerDto>(sql)
                    .ToList();
            }
        }
    }


    public class MaterialCustomerDto
    {
        public string Action { get; set; }

        public string RqstId { get; set; }

        public string Plant { get; set; }

        public string Matcode { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerSubcode { get; set; }

        public string SoldDate { get; set; }

        public string DeliveryCode { get; set; }

        public string Brand { get; set; }

        public string OrderNumber { get; set; }
    }

    public class MaterialCustomerMapper
    {
        public MaterialCustomer Map(
            MaterialCustomerDto dto)
        {
            return new MaterialCustomer
            {
                Action = dto.Action,

                RqstId = dto.RqstId,

                Plant = dto.Plant,

                Matcode = dto.Matcode,

                CustomerCode = dto.CustomerCode,

                CustomerSubcode =
                    dto.CustomerSubcode,

                SoldDate = dto.SoldDate,

                DeliveryCode =
                    dto.DeliveryCode,

                Brand = dto.Brand,

                OrderNumber =
                    dto.OrderNumber
            };
        }
    }
}
