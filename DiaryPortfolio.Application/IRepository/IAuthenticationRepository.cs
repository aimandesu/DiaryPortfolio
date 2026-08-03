using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository
{
    public interface IAuthenticationRepository
    {
        // Authentication Process
        Task<UserModel?> SignUp(UserModel user, string password);
        Task<ResultResponse<UserModel>> Login(
            string emailOrUsername,
            string password);
        Task Logout();
        Task<ResultResponse<UserModel?>> FindOrCreateUserGoogle(
            string email,
            string name,
            string googleUserId);

        // Email confirmation / password reset
        Task<UserModel?> FindByEmailAsync(string email);
        Task<UserModel?> FindByIdAsync(Guid userId);
        Task<string> GenerateEmailConfirmationTokenAsync(UserModel user);
        Task<IdentityResult> ConfirmEmailAsync(UserModel user, string token);
        Task<string> GeneratePasswordResetTokenAsync(UserModel user);
        Task<IdentityResult> ResetPasswordAsync(UserModel user, string token, string newPassword);
    }
}
