namespace authentication.Models.Dto;

public class EndpointRequestCountInfo
{
    public string TypeName { get; set; }
    public string EndpointName { get; set; }
    public int TotalRequests { get; set; }
}