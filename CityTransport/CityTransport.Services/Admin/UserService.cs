namespace CityTransport.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.CustomerCard;
    using CityTransport.Common.Models.Admin.InputModels.User;
    using CityTransport.Common.Models.Admin.ViewModels.User;
    using CityTransport.Data;
    using CityTransport.Models;
    using CityTransport.Models.Enums;
    using CityTransport.Services.Admin.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly BaseModel BaseModel;

        public UserService(CityTransprtDbContext Db,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
            : base(Db)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
            this.BaseModel = new BaseModel();
        }

        public async Task<ICollection<UserViewModel>> UsersAsync()
        {
            var users = this.Db.Users.Include(x => x.CustomerCard).ToList();
            var model = new List<UserViewModel>();
            
            foreach (var user in users)
            {
                var roles = await this.UserManager.GetRolesAsync(user);

                var userModel = new UserViewModel
                {
                    Id = user.Id,
                    RegisteredOn = user.RegisteredOn,
                    Username = user.UserName,
                };

                if (roles.Any(x => x == SeedDataConstants.AdminRole))
                {
                    userModel.IsAdmin = true;
                }

                if (roles.Any(r => r == SeedDataConstants.UserRole))
                {
                    userModel.IsUser = true;
                }

                if (user.CustomerCard != null)
                {
                    userModel.CustomerCardNumber = user.CustomerCard.CustomerCardNumber;
                }
                model.Add(userModel);
            }

            return model;
        }

        public async Task<string> CreateUserAsync(CreateUserInputModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await this.UserManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return null;
            }

            var userCreated = this.UserManager.Users.FirstOrDefault(x => x.UserName == user.UserName);
            await this.UserManager.AddToRoleAsync(userCreated, SeedDataConstants.UserRole);

            return userCreated.Id;

        }

        public async Task<UserDetailsViewModel> Details(string id, string tab)
        {
            var user = this.UserManager.Users
                .Where(x => x.Id == id)
                .Include(x => x.CustomerCard)
                .FirstOrDefault();

            if (user == null)
            {
                return null;
            }
            var subscriptionCards = this.Db.SubscriptionCards.Select(x => x.TypeCard.ToString()).Distinct().ToList();
            var cardTypes = Enum.GetValues(typeof(TypeCard)).Cast<TypeCard>()
                .Select(x => new SelectListItem
                {
                    Text = Enum.GetName(typeof(TypeCard), x),
                    Value = x.ToString()
                }).ToList();
            cardTypes = cardTypes.Where(x => subscriptionCards.Contains(x.Text)).ToList();

            var allRoles = this.RoleManager.Roles
             .Select(x => new SelectListItem
             {
                 Text = x.Name,
                 Value = x.Name
             })
             .ToList();

            var userRoles = await this.UserManager.GetRolesAsync(user);

            var userEditModel = new EditUserInputModel
            {
                Email = user.Email,
                FisrtName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName
            };
           
            var model = new UserDetailsViewModel
            {
                Id = user.Id,
                UserEdit = userEditModel,
                Roles = allRoles,
                UserRoles = userRoles,
                CardTypes = cardTypes,
            };

            if (user.CustomerCard != null)
            {
                var updateCustomerCard = new EditCustomerCardInputModel
                {
                    Id = user.CustomerCardId.Value,
                    CountTrips = user.CustomerCard.CountTrips,
                    CustomerCardNumber = user.CustomerCard.CustomerCardNumber,
                    TypeCard = user.CustomerCard.Type.ToString(),
                    ValidateFrom = user.CustomerCard.ValidFrom.Value,
                    ValidateTo = user.CustomerCard.ValidTo.Value
                };

                model.EditCustomerCard = updateCustomerCard;
            }
            if (tab == null)
            {
                model.Tab = "0";
            }
            else
            {
                model.Tab = tab;
            }

            return model;
        }

        public async Task<BaseModel> EditUser(string id, EditUserInputModel model)
        {
            var user =  await this.UserManager.FindByIdAsync(id);

            if (user == null)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidUserId;

            }
            else
            {
                user.Email = model.Email;
                user.FirstName = model.FisrtName;
                user.LastName = model.LastName;
                user.UserName = model.Username;

                var result = await this.UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    this.BaseModel.HasError = false;
                    this.BaseModel.Message = MessageConstants.EditUser;
                }
                else
                {
                    this.BaseModel.HasError = true;
                    this.BaseModel.Message = MessageConstants.NoEditUser;
                }
            }

            return this.BaseModel;
        }

        public async Task<BaseModel> ChangePasswordAsync(string id, ChangePasswordInputModel model)
        {
            var user = await this.UserManager.FindByIdAsync(id);

            if (user == null)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidUserId;

                return this.BaseModel;
            }

            var token = await this.UserManager.GeneratePasswordResetTokenAsync(user);
            var result = await this.UserManager.ResetPasswordAsync(user, token, model.Password);

            if (result.Succeeded)
            {
                this.BaseModel.HasError = false;
                this.BaseModel.Message = MessageConstants.ChangePassword;
            }
            else
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.NoChangePassword;
            }

            return this.BaseModel;
        }

        public async Task<BaseModel> AddToRoleAsync(string id, string role)
        {
            var user = await this.UserManager.FindByIdAsync(id);
            var roleExist = await this.RoleManager.RoleExistsAsync(role);

            if (user == null)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidUserId;
                return BaseModel;

            }

            if (!roleExist)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.RoleDoesntExist;
                return BaseModel;
            }

            var result = await this.UserManager.AddToRoleAsync(user, role);
            if (result.Succeeded)
            {
                this.BaseModel.HasError = false;
                this.BaseModel.Message = MessageConstants.AddUserToRole;
            }
            else
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.NoAddUserToRole;
            }

            return this.BaseModel;
        }

        public async Task<BaseModel> RemoveFromRoleAsync(string id, string role)
        {
            //TODO Visual only roles who user is it
            var user = await this.UserManager.FindByIdAsync(id);
            var roleExist = await this.RoleManager.RoleExistsAsync(role);

            if (user == null)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidUserId;
                return BaseModel;
            }

            if (!roleExist)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.RoleDoesntExist;
                return BaseModel;
            }

           var result = await this.UserManager.RemoveFromRoleAsync(user, role);
            if (result.Succeeded)
            {
                this.BaseModel.HasError = false;
                this.BaseModel.Message = MessageConstants.RemoveUserFromRole;
            }
            else
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.NoRemoveUserFromRole;
            }

            return this.BaseModel;
        }
    }
}
