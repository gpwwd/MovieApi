namespace Application.Dtos.ExternalApiResponses;

public class RootObject
{
    public List<Doc> Docs { get; set; }
    public int Total { get; set; }
    public int Limit { get; set; }
    public int Page { get; set; }
    public int Pages { get; set; }
}
