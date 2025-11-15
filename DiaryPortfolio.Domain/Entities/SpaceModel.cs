namespace DiaryPortfolio.Domain.Entities;

public class SpaceModel
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }  = DateTime.Now;
    
    // FK to User
    public Guid UserId { get; set; }
    public UserModel? User { get; set; }
    
    //EF 
    public List<MediaModel> MediaModels { get; set; } = [];
}