namespace WeddingInvitation.Web.API.Contract
{
    public interface IHelperRepository
    {
        Task<bool> ValidateIsFieldExists<T>(string tableName, string columnName, T value);
    }
}
