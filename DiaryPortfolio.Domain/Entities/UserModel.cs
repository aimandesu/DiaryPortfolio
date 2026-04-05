using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DiaryPortfolio.Domain.Entities;

public class UserModel : IdentityUser<Guid> //UserName, Password is already in IdentityUser
{
    // public override string? UserName { get; set; } = string.Empty;
    public PortfolioProfile? PortfolioProfile { get; set; }
    public DiaryProfile? DiaryProfile { get; set; }

}