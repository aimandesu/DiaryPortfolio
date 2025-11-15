namespace DiaryPortfolio.Domain.Entities;

public class CollectionModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    //FK
    public List<MediaModel> MediaModels { get; set; } = [];
}