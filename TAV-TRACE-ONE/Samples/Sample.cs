using System;
using System.Collections.Generic;
using Models;

namespace Samples
{
    public static class Sample
    {
        public static ContactRoot BuildContacts()
        {
            return new ContactRoot
            {
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        Action = "Create",
                        RqstId = "123",
                        PartnerType = "CUSTOMER",
                        PartnerCode = "296221",
                        PartnerSubcode = "NONE",
                        ContactKey = "viorel.friptu",
                        Phone = "",
                        Email = "viorel.friptu@vartech.it",
                        JobPosition = "Developer",
                        Type = "REGULATORY",
                        Title = "",
                        FirstName = "Viorel",
                        MiddleName = "",
                        LastName = "Friptu"
                    }
                }
            };
        }

        public static CustomersRoot BuildCustomers()
        {
            return new CustomersRoot
            {
                Customers = new List<Customer>
                {
                    new Customer
                    {
                        Code = "296221",
                        Subcode = "NONE",
                        Name = "TAVOLA S.P.A.",
                        Street = "Via Bernardino Verro 35",
                        Postcode = "20141",
                        City = "Milano",
                        StateProvince = "MI",
                        Country = "ITA",
                        DefLanguage = "it",
                        Email = "assistenza@tavola.it",
                        Fax = "",
                        Phone = "",
                        Type = "000",
                        Active = 1,
                        Contacts = new List<CustomerContact>
                        {
                            new CustomerContact
                            {
                                ContactId = 1,
                                Phone = "",
                                Title = "",
                                FirstName = "Test",
                                MiddleName = "",
                                LastName = "",
                                Email = "assistenza@tavola.it",
                                Type = "REGULATORY"
                            }
                        }
                    }
                }
            };
        }

        public static CustomerSpectypesRoot BuildCustomerSpectypes()
        {
            return new CustomerSpectypesRoot
            {
                CustomerSpectypes = new List<CustomerSpectype>
                {
                    new CustomerSpectype
                    {
                        Action = "Create",
                        RqstId = "1",
                        CustomerCode = "296221",
                        CustomerSubcode = "NONE",
                        SpectypeCode = "SC_MSD_EU_SPEC",
                        SpecTypeCustom = 0,
                        Languages = "it,fr"
                    }
                }
            };
        }

        public static MaterialCustomersRoot BuildMaterialCustomers()
        {
            return new MaterialCustomersRoot
            {
                MaterialCustomers = new List<MaterialCustomer>
                {
                    new MaterialCustomer
                    {
                        Action = "Create",
                        RqstId = Guid.NewGuid().ToString(),
                        Plant = "MAIN",
                        Matcode = "188566",
                        CustomerCode = "296221",
                        CustomerSubcode = "NONE",
                        SoldDate = DateTime.Today.ToString("yyyy-MM-dd"),
                        DeliveryCode = "500001",
                        Brand = "TAVOLA",
                        OrderNumber = "400001"
                    }
                }
            };
        }
    }
}
