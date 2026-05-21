using Config;
using Http;
using Json;
using Models;
using Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TAV_TRACE_ONE.Models;
using TAV_TRACE_ONE.Repository;
using TAV_TRACE_ONE.Repository.Repository;

namespace TraceOneConsole47
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12;

            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine(
                        "Parametro mancante."
                    );

                    Console.WriteLine(
                        "customers | spectypes | materials | all"
                    );

                    return;
                }

                string mode =
                    args[0].ToLower();

                string token = GetToken();

                var api =
                    CreateApiClient(token);

                switch (mode)
                {
                    case "customers":

                        RunCustomers(api);

                        break;

                    case "spectypes":

                        RunSpectypes(api);

                        break;

                    case "materials":

                        RunMaterials(api);

                        break;

                    case "all":

                        RunCustomers(api);

                        RunSpectypes(api);

                        RunMaterials(api);

                        break;

                    default:

                        Console.WriteLine(
                            "Parametro non valido."
                        );

                        break;
                }

                Console.WriteLine("Completato.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(
                    "ERRORE:"
                );

                Console.Error.WriteLine(ex);
            }
        }

        private static string GetToken()
        {
            var auth = new AuthClient(
                AppSettings.TokenUrl,
                AppSettings.ClientId,
                AppSettings.ClientSecret
            );

            var token = auth
                .GetAccessTokenAsync()
                .GetAwaiter()
                .GetResult();

            Console.WriteLine("Token acquisito.");

            return token;
        }

        private static TraceOneClient CreateApiClient(string token)
        {
            return new TraceOneClient(
                AppSettings.ApiBase,
                AppSettings.Tenant,
                token
            );
        }

        
        private static void PrintJson<T>(string title,T obj)
        {
            Console.WriteLine(
                $"--- {title} ---"
            );

            Console.WriteLine(
                JsonCfg.ToJson(obj)
            );

            Console.WriteLine();
        }


        //Customers
        private static void RunCustomers(TraceOneClient api)
        {
            CustomersRoot customers =
                BuildCustomersFromDb();

            PrintJson(
                "CUSTOMERS JSON",
                customers
            );

            SendCustomers(
                api,
                customers
            );
        }
        private static CustomersRoot BuildCustomersFromDb()
        {
            var repo = new CustomerSourceRepository(
                AppSettings.ConnectionString
            );

            List<CustomerDto> sourceCustomers =
                repo.GetCustomersToSend();

            Console.WriteLine(
                $"Clienti letti da DB: {sourceCustomers.Count}"
            );

            var mapper = new CustomerMapper();

            List<Customer> customers = sourceCustomers
                .Select(mapper.Map)
                .ToList();

            return new CustomersRoot
            {
                Customers = customers
            };
        }
        private static void SendCustomers(TraceOneClient api,CustomersRoot customers)
        {
            var logRepo =
                new CustomerLogRepository(
                    AppSettings.ConnectionString
                );

            foreach (var customer in customers.Customers)
            {
                SendSingleCustomer(
                    api,
                    logRepo,
                    customer
                );
            }
        }
        private static void SendSingleCustomer(TraceOneClient api,CustomerLogRepository logRepo,Customer customer)
        {
            try
            {
                var root = new CustomersRoot
                {
                    Customers =
                        new List<Customer>
                        {
                    customer
                        }
                };

                string requestJson =
                    JsonCfg.ToJson(root);

                ApiResult result =
                    api.SendCustomersAsync(root)
                        .GetAwaiter()
                        .GetResult();

                int logStatus;

                if (!result.Success)
                {
                    logStatus = result.StatusCode;
                }
                else if (!result.BusinessSuccess)
                {
                    logStatus = 205;
                }
                else
                {
                    logStatus = 200;
                }

                logRepo.SaveLog(
                    customer,

                    result.BusinessSuccess,

                    logStatus,

                    result.RequestBody,

                    result.ResponseBody,

                    result.BusinessError
                );


                Console.WriteLine(
                    $"{customer.Code} OK"
                );
            }
            catch (Exception ex)
            {
                logRepo.SaveLog(
                    customer,
                    false,
                    500,
                    "",
                    "",
                    ex.ToString()
                );

                Console.WriteLine(
                    $"{customer.Code} ERROR"
                );

                Console.WriteLine(ex.Message);
            }
        }


        //Spectypes
        private static void RunSpectypes(TraceOneClient api)
        {
            CustomerSpectypesRoot spectypes =
                BuildCustomerSpectypesFromDb();

            PrintJson(
                "CUSTOMER SPECTYPES JSON",
                spectypes
            );

            SendCustomerSpectypes(
                api,
                spectypes
            );
        }
        private static CustomerSpectypesRoot BuildCustomerSpectypesFromDb()
        {
            var repo =
                new CustomerSpectypeRepository(
                    AppSettings.ConnectionString
                );

            List<CustomerSpectypeDto> source =
                repo.GetSpectypesToSend();

            Console.WriteLine(
                $"Spectypes letti: {source.Count}"
            );

            var mapper =
                new CustomerSpectypeMapper();

            List<CustomerSpectype> items =
                source
                    .Select(mapper.Map)
                    .ToList();

            return new CustomerSpectypesRoot
            {
                CustomerSpectypes = items
            };
        }
        private static void SendCustomerSpectypes(TraceOneClient api,CustomerSpectypesRoot root)
        {
            var logRepo =
                new CustomerSpectypeLogRepository(
                    AppSettings.ConnectionString
                );

            foreach (var item
                in root.CustomerSpectypes)
            {
                SendSingleCustomerSpectype(
                    api,
                    logRepo,
                    item
                );
            }
        }
        private static void SendSingleCustomerSpectype(TraceOneClient api,CustomerSpectypeLogRepository logRepo,CustomerSpectype item)
        {
            try
            {
                var root =
                    new CustomerSpectypesRoot
                    {
                        CustomerSpectypes =
                            new List<CustomerSpectype>
                            {
                        item
                            }
                    };

                string requestJson =
                    JsonCfg.ToJson(root);

                ApiResult result =
                    api.SendCustomerSpectypesAsync(root)
                        .GetAwaiter()
                        .GetResult();

                int logStatus;

                if (!result.Success)
                {
                    logStatus = result.StatusCode;
                }
                else if (!result.BusinessSuccess)
                {
                    logStatus = 205;
                }
                else
                {
                    logStatus = 200;
                }

                logRepo.SaveLog(
                    item,

                    result.BusinessSuccess,

                    logStatus,

                    result.RequestBody,

                    result.ResponseBody,

                    result.BusinessError
                );

                Console.WriteLine(
                    $"{item.CustomerCode} SPECTYPE OK"
                );
            }
            catch (Exception ex)
            {
                logRepo.SaveLog(
                    item,
                    false,
                    500,
                    "",
                    "",
                    ex.ToString()
                );

                Console.WriteLine(
                    $"{item.CustomerCode} SPECTYPE ERROR"
                );

                Console.WriteLine(ex.Message);
            }
        }


        //MaterialCustomers
        private static void RunMaterials(TraceOneClient api)
        {
            MaterialCustomersRoot materials =
                BuildMaterialCustomersFromDb();

            PrintJson(
                "MATERIAL CUSTOMERS JSON",
                materials
            );

            SendMaterialCustomers(
                api,
                materials
            );
        }
        private static MaterialCustomersRoot BuildMaterialCustomersFromDb()
        {
            var repo =
                new MaterialCustomerRepository(
                    AppSettings.ConnectionString
                );

            List<MaterialCustomerDto> source =
                repo.GetMaterialCustomersToSend();

            Console.WriteLine(
                $"MaterialCustomers letti: " +
                source.Count
            );

            var mapper =
                new MaterialCustomerMapper();

            List<MaterialCustomer> items =
                source
                    .Select(mapper.Map)
                    .ToList();

            return new MaterialCustomersRoot
            {
                MaterialCustomers = items
            };
        }
        private static void SendMaterialCustomers(TraceOneClient api,MaterialCustomersRoot root)
        {
            var logRepo =
                new MaterialCustomerLogRepository(
                    AppSettings.ConnectionString
                );

            foreach (var item
                in root.MaterialCustomers)
            {
                SendSingleMaterialCustomer(
                    api,
                    logRepo,
                    item
                );
            }
        }
        private static void SendSingleMaterialCustomer(TraceOneClient api,MaterialCustomerLogRepository logRepo,MaterialCustomer item)
        {
            try
            {
                var root =
                    new MaterialCustomersRoot
                    {
                        MaterialCustomers =
                            new List<MaterialCustomer>
                            {
                        item
                            }
                    };

                string requestJson =
                    JsonCfg.ToJson(root);

                ApiResult result =
                    api.SendMaterialCustomersAsync(root)
                        .GetAwaiter()
                        .GetResult();

                int logStatus;

                if (!result.Success)
                {
                    logStatus = result.StatusCode;
                }
                else if (!result.BusinessSuccess)
                {
                    logStatus = 205;
                }
                else
                {
                    logStatus = 200;
                }

                logRepo.SaveLog(
                    item,

                    result.BusinessSuccess,

                    logStatus,

                    result.RequestBody,

                    result.ResponseBody,

                    result.BusinessError
                );

                Console.WriteLine(
                    $"{item.CustomerCode} MATERIAL OK"
                );
            }
            catch (Exception ex)
            {
                logRepo.SaveLog(
                    item,
                    false,
                    500,
                    "",
                    "",
                    ex.ToString()
                );

                Console.WriteLine(
                    $"{item.CustomerCode} MATERIAL ERROR"
                );

                Console.WriteLine(ex.Message);
            }
        }
    }
    
}
