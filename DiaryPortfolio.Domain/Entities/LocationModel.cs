namespace DiaryPortfolio.Domain.Entities;

public class LocationModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Latitude { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
}