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

        public async Task<UserModel?> SignUp(UserModel user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return user;
            }

            return null;

        }
    }
}
