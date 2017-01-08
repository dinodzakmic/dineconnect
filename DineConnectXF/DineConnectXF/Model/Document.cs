using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineConnectXF.Model
{
    public class DocumentResult
    {
        public int TotalCount { get; set; }
        public List<UploadDocument> Items { get; set; }
    }

    public class Documents
    {
        public bool Success { get; set; }
        public DocumentResult Result { get; set; }
        public Error Error { get; set; }
        public bool UnAuthorizedRequest { get; set; }
    }

    public class UploadDocument
    {
        public int TenantId { get; set; }
        public int LocationId { get; set; }
        public string Url { get; set; }
        public int DocumentStatus { get; set; }
    }
    public class SearchDocument
    {
        public int TenantId { get; set; }
        public int Location { get; set; }
        public int Supplier { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public SearchDocument(int tenantId, int locationId, int suppId, string startDate, string endDate)
        {
            this.TenantId = tenantId;
            Location = locationId;
            Supplier = suppId;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}
