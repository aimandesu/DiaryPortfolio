using Microsoft.AspNetCore.Identity;

namespace DiaryPortfolio.Domain.Entities;

public class UserModel : IdentityUser<Guid> //UserName, Password is already in IdentityUser
{
    // public override string? UserName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    
    //EF
    public List<SpaceModel> SpaceModels { get; set; } = [];

}