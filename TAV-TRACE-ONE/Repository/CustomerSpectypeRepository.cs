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
    public class CustomerSpectypeRepository
    {
        private readonly string _connectionString;

        public CustomerSpectypeRepository(
            string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<CustomerSpectypeDto>
            GetSpectypesToSend()
        {
            using (var conn =
                new SqlConnection(_connectionString))
            {
                conn.Open();

                var sql = @"
                              SELECT [CodCliente]
                                  ,[cod_spectype]
                                  ,[Lingue]
                                  ,[LastSourceUpdate]
                                  ,[SyncAction]
                              FROM [CQ].[dbo].[TraceOne_Spectype_DaInviare]
                ";

                return conn
                    .Query<CustomerSpectypeDto>(sql)
                    .ToList();
            }
        }
    }

    public class CustomerSpectypeDto
    {
        public string CodCliente { get; set; }

        public string Cod_Spectype { get; set; }

        public string Lingue { get; set; }

        public string SyncAction { get; set; }
    }

    public class CustomerSpectypeMapper
    {
        public CustomerSpectype Map(CustomerSpectypeDto dto)
        {
            return new CustomerSpectype
            {
                Action = dto.SyncAction,

                RqstId = Guid.NewGuid()
                    .ToString(),

                CustomerCode = dto.CodCliente,

                CustomerSubcode = "NONE",

                SpectypeCode = dto.Cod_Spectype,

                SpecTypeCustom = 0,

                Languages = dto.Lingue
            };
        }
    }
}
