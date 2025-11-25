using Jtbd.Domain.Entities;

namespace Jtbd.Application.Interfaces
{
    public interface IMatrizWard
    {
        Task<bool> GetMatrizWardAsync(int proyectId, int numberOfClusters);
        Task<List<StoriesClusters>> GetStoriesClustersAsync(int proyectId);
        Task<bool> CreateStorieClustereAsync(int proyectId, int storieId, int clustereId);
        Task<bool> DeleteStorieClustereAsync(int proyectId);

        Task<List<StoriesClustersJobs>> GetClustersJobsAsync(int proyectId);
        Task<List<StoriesClustersJobs>> GetClustersJobsByClusterAsync(int proyectId, int clusterid);
        Task<List<StoriesClustersJobs>> GetClustersJobsByTipoClusterAsync(int proyectId, int clusterid, int tipoid);
        Task<bool> CreateClusterJobsAsync(CreateClustersJobs jobs);
        Task<bool> UpdateClusterJobsAsync(CreateClustersJobs jobs);
        Task<bool> DeleteClusterJobsAsync(int id);

    }
}
