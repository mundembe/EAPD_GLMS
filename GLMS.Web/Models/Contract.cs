using System.ComponentModel.DataAnnotations;

namespace GLMS.Web.Models
{
    public class Contract
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ContractStatus Status { get; set; }

        public string ServiceLevel { get; set; }

        public string? SignedAgreementPath { get; set; }

        public ICollection<ServiceRequest>? ServiceRequests { get; set; }
    }
}