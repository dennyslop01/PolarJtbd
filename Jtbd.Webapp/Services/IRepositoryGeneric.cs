namespace Jtbd.Webapp.Services
{
    public interface IRepositoryGeneric
    {
        Task<T> GetAsync<T>(string url);

        Task<T> GetByIdAsync<T>(string url, int id);

        Task<object> PostAsync<T>(string url, T model);

        Task<object> DeleteAsync(string url);

        Task<object> PutAsync<T>(string url, T model);
    }
}
