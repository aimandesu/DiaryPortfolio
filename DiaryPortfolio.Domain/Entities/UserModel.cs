using Microsoft.AspNetCore.Identity;

namespace DiaryPortfolio.Domain.Entities;

public class UserModel : IdentityUser<Guid> //UserName, Password is already in IdentityUser
{
    // public override string? UserName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int? Age { get; set; }
    public string Title { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    //FK
    public Guid? LocationId { get; set; }
    public LocationModel? Location { get; set; }

    public Guid? ResumeId { get; set; }
    public ResumeModel? Resume { get; set; }

    public Guid? MediaId { get; set; }
    public MediaModel? ProfileMedia { get; set; }

    //EF
    public List<SpaceModel> SpaceModels { get; set; } = [];

}