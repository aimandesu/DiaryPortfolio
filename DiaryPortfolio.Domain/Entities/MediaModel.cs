using DiaryPortfolio.Domain.Enum;

namespace DiaryPortfolio.Domain.Entities;

public class MediaModel
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public MediaStatus MediaStatus { get; set; } = MediaStatus.Public;
    public MediaType MediaType { get; set; } = MediaType.Post;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    //FK, EF 
    // Media Table
    public Guid SpaceId { get; set; }
    public SpaceModel? SpaceModel { get; set; }
    
    //Location Table
    public LocationModel? LocationModel { get; set; }
    
    //Condition Table
    public ConditionModel? ConditionModel { get; set; }
    
    //Videos
    public List<VideoModel> VideoModels { get; set; } = [];
    
    //Photos
    public List<PhotoModel> PhotoModels { get; set; } = [];
    
    //TextStyle Table
    public Guid TextId { get; set; }
    public TextModel? TextModel { get; set; }
    
    //EF
    public Guid CollectionId { get; set; }
    public CollectionModel? CollectionModel { get; set; }
    
}