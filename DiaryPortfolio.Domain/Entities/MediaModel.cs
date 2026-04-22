using DiaryPortfolio.Domain.Enum;
using DiaryPortfolio.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiaryPortfolio.Domain.Entities;

public class MediaModel : IUserOwner, IUserOwnerQuery
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

    //Photos
    public List<MediaPhotoModel> MediaPhotos { get; set; } = [];
    public List<MediaVideoModel> MediaVideos { get; set; } = [];

    //EF
    public Guid? CollectionId { get; set; }
    public CollectionModel? CollectionModel { get; set; }
    public SelectionModel? SelectionMediaStatusModel { get; set; }
    public SelectionModel? SelectionMediaTypeModel { get; set; }

    public Guid OwnerId => SpaceModel?.DiaryProfile?.UserId ?? Guid.Empty;

    public static IQueryable<object> WithOwnerIncludes(DbContext context)
        => context.Set<MediaModel>()
            .Include(m => m.SpaceModel)
                .ThenInclude(s => s!.DiaryProfile);
}