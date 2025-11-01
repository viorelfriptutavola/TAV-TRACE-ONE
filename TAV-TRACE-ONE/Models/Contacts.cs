using System.Collections.Generic;

namespace Models
{
    public sealed class ContactRoot
    {
        public List<Contact> Contacts { get; set; } = new List<Contact>();
    }

    public sealed class Contact
    {
        public string Action { get; set; }
        public string RqstId { get; set; }
        public string PartnerType { get; set; }    // "CUSTOMER"
        public string PartnerCode { get; set; }
        public string PartnerSubcode { get; set; } // "NONE"
        public string ContactKey { get; set; }     // chiave univoca (es. email)
        public string Phone { get; set; }
        public string Email { get; set; }
        public string JobPosition { get; set; }
        public string Type { get; set; }           // "REGULATORY"
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
