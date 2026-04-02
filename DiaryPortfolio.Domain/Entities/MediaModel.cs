using DiaryPortfolio.Domain.Enum;

namespace DiaryPortfolio.Domain.Entities;

public class MediaModel
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid MediaStatusSelectionId { get; set; }
    public Guid MediaTypeSelectionId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    //FK, EF 
    // Media Table
    public Guid SpaceId { get; set; }
    public SpaceModel? SpaceModel { get; set; }

    //Location Table
    public Guid? LocationId { get; set; }
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
    public Guid? CollectionId { get; set; }
    public CollectionModel? CollectionModel { get; set; }
    public SelectionModel? SelectionMediaStatusModel { get; set; }
    public SelectionModel? SelectionMediaTypeModel { get; set; }

}