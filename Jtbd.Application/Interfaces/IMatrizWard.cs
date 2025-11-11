using Jtbd.Domain.Entities;
using Jtbd.Domain.ViewModel;

namespace Jtbd.Application.Interfaces
{
    public interface IMatrizWard
    {
        Task<List<FinalClusterGroup>> GetMatrizWardAsync(int proyectId, int numberOfClusters);
        Task<List<StoriesClusters>> GetStoriesClustersAsync(int proyectId);
        Task<bool> CreateStorieClustereAsync(int proyectId, int storieId, int clustereId);
        Task<bool> DeleteStorieClustereAsync(int proyectId);
    }
}
