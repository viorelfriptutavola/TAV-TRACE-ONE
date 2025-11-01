using System.Collections.Generic;

namespace Models
{
    public sealed class CustomersRoot
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }

    public sealed class Customer
    {
        public string Code { get; set; }
        public string Subcode { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public string DefLanguage { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public int? Active { get; set; }
        public List<CustomerContact> Contacts { get; set; }
    }

    public sealed class CustomerContact
    {
        public int? ContactId { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; } // "REGULATORY"
    }
}
