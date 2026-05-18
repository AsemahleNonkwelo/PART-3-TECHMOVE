using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema; 

namespace TechMovePOE.Models;

public class Contract
{
    public int ContractId { get; set; }

    public int ClientId { get; set; }

    public Client? Client { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string ServiceLevel { get; set; } = string.Empty;

    public string? AgreementFilePath { get; set; }

    [NotMapped]
public IFormFile? AgreementFile { get; set; }

    public ICollection<ServiceRequest>? ServiceRequests { get; set; }
}
