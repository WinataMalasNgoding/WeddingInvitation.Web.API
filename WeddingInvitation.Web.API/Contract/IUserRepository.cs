using WeddingInvitation.Web.API.Models;

namespace WeddingInvitation.Web.API.Contract
{
    public interface IUserRepository
    {
        Task<UserGridModel?> GetUserById(long userId);
        Task<List<UserGridModel>> GetUserGridAll();
        Task<UserModel?> InsertUser(UserInsertModel userInput);
        Task<UserModel?> UpdateUser(long userId, UserUpdateModel userInput);
        Task<string?> DeleteUser(long userId);
    }
}
