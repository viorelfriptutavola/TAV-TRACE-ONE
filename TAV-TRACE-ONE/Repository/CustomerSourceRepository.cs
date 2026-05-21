using Config;
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
    public class CustomerSourceRepository
    {
        private readonly string _connectionString;

        public CustomerSourceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<CustomerDto> GetCustomersToSend()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var sql = @"
                    SELECT [CodCliente]
                          ,[RagioneSociale]
                          ,[Indirizzo]
                          ,[CAP]
                          ,[Citta]
                          ,[Provincia]
                          ,[Nazione]
                          ,[des_mail]
                          ,[Lingua]
                          ,[id_mercato]
                          ,[cod_spectype]
                          ,[DataInserimento]
                          ,[DataModifica]
                      FROM [CQ].[dbo].[TraceOne_Customer_DaInviare]
                    ";

                return conn.Query<CustomerDto>(sql).ToList();
            }
        }
    }

    public class CustomerDto
    {
        public string CodCliente { get; set; }

        public string RagioneSociale { get; set; }

        public string Indirizzo { get; set; }

        public string CAP { get; set; }

        public string Citta { get; set; }

        public string Provincia { get; set; }

        public string Nazione { get; set; }

        public string Des_Mail { get; set; }

        public string Lingua { get; set; }

        public int Id_Mercato { get; set; }

        public string Cod_Spectype { get; set; }
    }

    public class CustomerMapper
    {
        public Customer Map(CustomerDto dto)
        {
            return new Customer
            {
                Code = dto.CodCliente,

                Subcode = "NONE",

                Name = dto.RagioneSociale,

                Street = dto.Indirizzo,

                Postcode = dto.CAP,

                City = dto.Citta,

                StateProvince = dto.Provincia,

                Country = dto.Nazione,

                DefLanguage = dto.Lingua?.ToLower(),

                Email = dto.Des_Mail?.Trim().ToLowerInvariant(),

                Fax = "",

                Phone = "",

                Type = "000",

                Active = 1,

                Contacts = BuildContacts(dto)
            };
        }

        private List<CustomerContact> BuildContacts(
            CustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Des_Mail))
            {
                return new List<CustomerContact>();
            }

            return new List<CustomerContact>
        {
            new CustomerContact
            {
                ContactId = 1,

                Phone = "",

                Title = "",

                FirstName = "Regulatory",

                MiddleName = "",

                LastName = dto.RagioneSociale,

                Email = dto.Des_Mail?.Trim().ToLower(),

                Type = "REGULATORY"
            }
        };
        }
    }


}
