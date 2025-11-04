using Jtbd.Domain.ViewModel;

namespace Jtbd.Application.Interfaces
{
    public interface IMatrizWard
    {
        Task<List<FinalClusterGroup>> GetMatrizWardAsync(int proyectId, int numberOfClusters);
    }
}
