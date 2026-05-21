using System;
using System.Configuration;

namespace Config
{
    public static class AppSettings
    {
        public static string ClientId => Get("TRACEONE_CLIENT_ID");
        //public static string ClientSecret => Get("TRACEONE_CLIENT_SECRET");
        public static string ClientSecret =>
          Environment.GetEnvironmentVariable("TRACEONE_CLIENT_SECRET");

        public static string ConnectionString =>
            Environment.GetEnvironmentVariable("SQL_WEBAPP_DB");

        public static string Tenant => Get("TRACEONE_TENANT");

        public static string TokenUrl => Get("TRACEONE_TOKEN_URL");
        public static string ApiBase => Get("TRACEONE_API_BASE");

        public static string EpCustomers => Get("EP_CUSTOMERS");
        public static string EpContacts => Get("EP_CONTACTS");
        public static string EpMaterialCustomers => Get("EP_MATCUSTOMERS");
        public static string EpCustomerSpectypes => Get("EP_CUSTOMER_SPECTYPES");
        public static string EpExportMaterials => Get("EP_EXPORT_MATERIALS");
        private static string Get(string key)
        {
            var v = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(v))
                throw new ConfigurationErrorsException($"Chiave mancante in App.config: {key}");
            return v;
        }
    }
}
