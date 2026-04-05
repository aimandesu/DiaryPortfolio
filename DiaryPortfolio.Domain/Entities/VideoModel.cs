namespace DiaryPortfolio.Domain.Entities;

public class VideoModel
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public int Duration  { get; set; }
    public string Mime { get; set; } = string.Empty;
    public double Size { get; set; }
}