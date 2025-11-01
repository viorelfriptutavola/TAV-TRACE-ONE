using System.Collections.Generic;

namespace Models
{
    public sealed class CustomerSpectypesRoot
    {
        public List<CustomerSpectype> CustomerSpectypes { get; set; } = new List<CustomerSpectype>();
    }

    public sealed class CustomerSpectype
    {
        public string Action { get; set; }           // "Create"|"Update"|"Delete"
        public string RqstId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerSubcode { get; set; }  // "NONE"
        public string SpectypeCode { get; set; }
        public int? SpecTypeCustom { get; set; }   // 0|1
        public string Languages { get; set; }        // "it,fr"
    }
}
