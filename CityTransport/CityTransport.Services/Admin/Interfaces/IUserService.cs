namespace CityTransport.Services.Admin.Interfaces
{
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.User;
    using CityTransport.Common.Models.Admin.ViewModels.User;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<ICollection<UserViewModel>> UsersAsync();

        Task<UserDetailsViewModel> Details(string id, string tab);

        Task<string> CreateUserAsync(CreateUserInputModel model);

        Task<BaseModel> EditUser(string id, EditUserInputModel model);

        Task<BaseModel> ChangePasswordAsync(string id, ChangePasswordInputModel model);

        Task<BaseModel> AddToRoleAsync(string id, string role);

        Task<BaseModel> RemoveFromRoleAsync(string id, string role);

    }
}
