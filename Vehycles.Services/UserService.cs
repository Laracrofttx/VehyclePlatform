﻿namespace Vehycles.Services
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Caching.Memory;
	using System.Threading.Tasks;
	using Vehycle.Data.Models;
	using Vehycle.Web.ViewModels.Account;
	using Vehycles.Services.Interfaces;
	using static Vehycle.Common.GeneralApplicationConstants;
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly IMemoryCache memoryCache;

		public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMemoryCache memoryCache)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.memoryCache = memoryCache;
		}

		public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
		{
			try
			{
				var userExist = await this.userManager.FindByEmailAsync(model.EmailAddress);
				if (userExist != null)
				{
					throw new ArgumentException("User with this email already exists.");
				}

				var user = new ApplicationUser
				{
					UserName = model.UserName,
					Email = model.EmailAddress,
					FirstName = model.FirstName,
					LastName = model.LastName,
					Gender = model.Gender,
					PhoneNumber = model.PhoneNumber,
					DateOfBirth = model.DateOfBirth,
					Age = model.Age
				};
				var result = await userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					await this.signInManager.SignInAsync(user, false);
					this.memoryCache.Remove(UsersCacheKey);
				}
				return result;
			}
			catch (Exception)
			{
				return IdentityResult.Failed(new IdentityError
				{
					Description =
					"An error occurred during registration. Please try again later."
				});
			}
		}
		public async Task<LoginRequestModel> LoginAsync(LoginRequestModel model)
		{
			try
			{
				//var user = await userManager.FindByEmailAsync(model.UserName) ?? throw new ArgumentException("There is no such user.");
				var result = await this.signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

				if (!result.Succeeded)
				{
					throw new ArgumentException("There was a error while loggin you in! Please try again later or contact an administrator.");
				}

				return new LoginRequestModel()
				{
					UserName = model.UserName
				};
			}
			catch (Exception ex)
			{
				throw new ArgumentException("An error occurred during login. Please try again later.", ex.Message);
			}
		}

		public async Task LogoutAsync()
		{
			await this.signInManager.SignOutAsync();
		}
	}
}
