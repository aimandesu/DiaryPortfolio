using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Helpers.Authentication
{
    public class AuthService(
        UserManager<UserModel> userManager,
        SignInManager<UserModel> signInManager
        ) : IAuthenticationRepository
    {
        private readonly UserManager<UserModel> _userManager = userManager;
        private readonly SignInManager<UserModel> _signInManager = signInManager;

        public async Task<ResultResponse<UserModel>> Login(
            string EmailOrUsername,
            string password)
        {
            var normalizedInput = EmailOrUsername.ToUpperInvariant();

            var identityUser = await _userManager.Users
                .FirstOrDefaultAsync(x =>
                    x.NormalizedUserName == normalizedInput ||
                    x.NormalizedEmail == normalizedInput
                );

            if (identityUser is null)
                return ResultResponse<UserModel>.Failure(
                    new Error(HttpStatusCode.NotFound, "Error: User not found"));

            var result = await _signInManager.CheckPasswordSignInAsync(
                identityUser,
                password,
                true
            );

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    return ResultResponse<UserModel>.Failure(
                        new Error(HttpStatusCode.TooManyRequests, "User Locked Out: Too many attempts")
                    );

                return ResultResponse<UserModel>.Failure(
                    new Error(HttpStatusCode.Unauthorized, "Invalid Credentials: Username or Password incorrect")
                );
            }

            return ResultResponse<UserModel>.Success(identityUser);

        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ResultResponse<UserModel?>> FindOrCreateUserGoogle(
            string email, 
            string name,
            string googleUserId)
        {
            //Case 1: user has signed in with Google before -> just return them
            var existingGoogleUser = await _userManager.FindByLoginAsync(
                "Google", googleUserId);

            if (existingGoogleUser is not null)
            {
                return ResultResponse<UserModel?>.Success(existingGoogleUser);
            }
            
            // Case 2: user already has an account (e.g. signed up with email/password) -> link Google to it
            var existingEmailUser = await  _userManager.FindByEmailAsync(email);

            if (existingEmailUser is not null)
            {
                return await LinkGoogleToExistingUser(
                    existingEmailUser, googleUserId);
            }
            
            // Case 3: brand-new user, Google-only
            return await CreateGoogleOnlyUser(email, name, googleUserId);
        }

        public async Task<UserModel?> SignUp(UserModel user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return user;
            }

            return null;

        }

        public async Task<UserModel?> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<UserModel?> FindByIdAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(UserModel user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(UserModel user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(UserModel user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(UserModel user, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        private async Task<ResultResponse<UserModel?>> LinkGoogleToExistingUser(
            UserModel user,
            string googleUserId)
        {
            if (!user.EmailConfirmed)
            {
                return ResultResponse<UserModel?>.Failure(
                    new Error(
                        HttpStatusCode.Conflict,
                        "Error: An account with this email already exists but is not verified. " +
                        "Please log in with your password to verify and link Google.")
                    );
            }
            
            var loginInfo = new UserLoginInfo(
                "Google", 
                googleUserId, 
                "Google");
            
            var loginResult = await _userManager.AddLoginAsync(
                user, loginInfo);

            if (!loginResult.Succeeded)
            {
                return ResultResponse<UserModel?>.Failure(
                    new Error(
                        HttpStatusCode.Unauthorized,
                        "Error: Binding Google Account Failed")
                    );
            }

            return ResultResponse<UserModel?>.Success(user);
            
        }

        private async Task<ResultResponse<UserModel?>> CreateGoogleOnlyUser(
            string email,
            string name,
            string googleUserId)
        {
            var portfolioProfile = new PortfolioProfileModel();
            var diaryProfile =  new DiaryProfileModel();
            
            var user = new UserModel
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                //need map for diaryPortfolio and PortfolioProfile
                PortfolioProfile = portfolioProfile,
                DiaryProfile = diaryProfile,
            };
                
            var createdResult = await _userManager.CreateAsync(user);

            if (!createdResult.Succeeded)
            {
                return ResultResponse<UserModel?>.Failure(
                    new Error(
                        HttpStatusCode.Unauthorized,
                        "Error: Creating User Failed")
                );
            }
            
            var loginInfo = new UserLoginInfo(
                "Google", 
                googleUserId, 
                "Google");

            var loginResult = await _userManager.AddLoginAsync(
                user, loginInfo);

            if (!loginResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                
                return ResultResponse<UserModel?>.Failure(
                    new Error(
                        HttpStatusCode.Unauthorized,
                        "Error: Binding Google Account Failed")
                ); 
            }
            
            return ResultResponse<UserModel?>.Success(user);
        }
        
    }
}
