using System.Collections.Generic;

namespace Models
{
    public sealed class MaterialCustomersRoot
    {
        public List<MaterialCustomer> MaterialCustomers { get; set; } = new List<MaterialCustomer>();
    }

    public sealed class MaterialCustomer
    {
        public string Action { get; set; }       // "Create"|"Update"|"Delete"
        public string RqstId { get; set; }       // guid/string
        public string Plant { get; set; }        // "MAIN"
        public string Matcode { get; set; }      // "102262"
        public string CustomerCode { get; set; } // "296221"
        public string CustomerSubcode { get; set; } // "NONE"
        public string SoldDate { get; set; }     // "yyyy-MM-dd"
        public string DeliveryCode { get; set; } // "500001"
        public string Brand { get; set; }        // "TAVOLA"
        public string OrderNumber { get; set; }  // opzionale
    }
}
