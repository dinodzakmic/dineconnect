using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineConnectXF.Model
{
    public class Supplier
    {
        public string SupplierName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber1 { get; set; }
        public int DefaultCreditDays { get; set; }
        public string OrderPlacedThrough { get; set; }
        public string Email { get; set; }
        public string FaxNumber { get; set; }
        public string Website { get; set; }
        public bool IsActive { get; set; }
        public string TallySupplierName { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public bool IsDeleted { get; set; }
        public string DeleterUserId { get; set; }
        public string DeletionTime { get; set; }
        public string LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public string CreationTime { get; set; }
        public int CreatorUserId { get; set; }
        public int Id { get; set; }
        public override string ToString()
        {
            return SupplierName;
        }
    }

    public class SupplierResult
    {
        public List<Supplier> Items { get; set; }
    }

    public class Suppliers
    {
        public bool Success { get; set; }
        public SupplierResult Result { get; set; }
        public Error Error { get; set; }
        public bool UnAuthorizedRequest { get; set; }
    }
}
