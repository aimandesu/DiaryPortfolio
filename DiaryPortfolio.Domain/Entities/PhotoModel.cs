namespace DiaryPortfolio.Domain.Entities;

public class PhotoModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Url { get; set; } = string.Empty;
    public string Mime { get; set; } = string.Empty;
    public double Width { get; set; } 
    public double Height { get; set; }
    public double Size { get; set; }
    
}