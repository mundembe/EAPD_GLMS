namespace GLMS.Web.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }

        public int ContractId { get; set; }
        public Contract? Contract { get; set; }

        public string Description { get; set; }

        public decimal CostUSD { get; set; }
        public decimal CostZAR { get; set; }

        public RequestStatus Status { get; set; }
    }
}