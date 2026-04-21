namespace DiaryPortfolio.Domain.Entities;

public class LocationModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public Guid? PostalCodeId { get; set; }
    public PostalCodeModel? PostalCode { get; set; }
}