namespace Infrastructure.Data
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
    }
}