namespace DiaryPortfolio.Domain.Entities;

public class VideoModel
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public int Duration  { get; set; }
    public double Size { get; set; }
    
    //EF
    public Guid MediaId { get; set; }
    public MediaModel? MediaModel { get; set; }
}