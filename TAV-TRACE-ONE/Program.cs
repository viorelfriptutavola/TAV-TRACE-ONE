using System;
using System.Net;
using Config;
using Http;
using Json;
using Models;
using Samples;

namespace TraceOneConsole47
{
    class Program
    {
        static void Main(string[] args)
        {
            // TLS 1.2 esplicito su .NET 4.7
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                // 1) OAuth2 client_credentials
                var auth = new AuthClient(
                    AppSettings.TokenUrl,
                    AppSettings.ClientId,
                    AppSettings.ClientSecret
                );
                var token = auth.GetAccessTokenAsync().GetAwaiter().GetResult();
                Console.WriteLine("Token acquisito.");

                // 2) API client
                var api = new TraceOneClient(
                    AppSettings.ApiBase,
                    AppSettings.Tenant,
                    token
                );

                // 3) Dati d'esempio
                ContactRoot contacts = Sample.BuildContacts();
                CustomersRoot customers = Sample.BuildCustomers();
                CustomerSpectypesRoot spectypes = Sample.BuildCustomerSpectypes();
                MaterialCustomersRoot matcusts = Sample.BuildMaterialCustomers();

                // Debug JSON
                Console.WriteLine("--- CONTACTS JSON ---");
                Console.WriteLine(JsonCfg.ToJson(contacts)); Console.WriteLine();

                Console.WriteLine("--- CUSTOMERS JSON ---");
                Console.WriteLine(JsonCfg.ToJson(customers)); Console.WriteLine();

                Console.WriteLine("--- CUSTOMER SPECTYPES JSON ---");
                Console.WriteLine(JsonCfg.ToJson(spectypes)); Console.WriteLine();

                Console.WriteLine("--- MATERIAL CUSTOMERS JSON ---");
                Console.WriteLine(JsonCfg.ToJson(matcusts)); Console.WriteLine();

                // 4) Invii
                //Console.WriteLine("Invio Contacts...");
                //Console.WriteLine(api.SendContactsAsync(contacts).GetAwaiter().GetResult()); Console.WriteLine();

                //Console.WriteLine("Invio Customers...");
                //Console.WriteLine(api.SendCustomersAsync(customers).GetAwaiter().GetResult()); Console.WriteLine();

                Console.WriteLine("Invio Customer SpecTypes...");
                Console.WriteLine(api.SendCustomerSpectypesAsync(spectypes).GetAwaiter().GetResult()); Console.WriteLine();

                //Console.WriteLine("Invio Material Customers...");
                //Console.WriteLine(api.SendMaterialCustomersAsync(matcusts).GetAwaiter().GetResult()); Console.WriteLine();
                //500


                Console.WriteLine("Completato.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("ERRORE: " + ex);
            }
        }
    }
}
