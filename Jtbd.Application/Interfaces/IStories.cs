using Jtbd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Application.Interfaces
{
    public interface IStories
    {
        Task<IEnumerable<Stories>> GetAllAsync();
        Task<Stories> GetByIdAsync(int id);
        Task<IEnumerable<Stories>> GetByProjectIdAsync(int id);
        Task<IEnumerable<Stories>> GetByInterIdAsync(int id);
        Task<bool> CreateAsync(CreateStorie stories);
        Task<bool> UpdateAsync(CreateStorie stories);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<StoriesPush>> GetPushesByStorieIdAsync(int id);
        Task<IEnumerable<StoriesPull>> GetPullsByStorieIdAsync(int id);
        Task<IEnumerable<StoriesHabbit>> GetHabitsByStorieIdAsync(int id);
        Task<IEnumerable<StoriesAnxiety>> GetAxieByStorieIdAsync(int id);

        Task<bool> CreateStoriePushAsync(CreateStoriesPush nuevo);
        Task<bool> CreateStoriePullAsync(CreateStoriesPull nuevo);
        Task<bool> CreateStorieHabitAsync(CreateStoriesHabbit nuevo);
        Task<bool> CreateStorieAnxieAsync(CreateStoriesAnxiety nuevo);
        Task<bool> DeleteStorieEntityAsync(int opcion, int idStorie, int idEntidad);
        Task<IEnumerable<StoriesPush>> GetPushesByProjectIdAsync(int id);
        Task<bool> UpdatePushesGroupEntityAsync(int idPush, int? idGroup);
        Task<IEnumerable<StoriesPull>> GetPullsByProjectIdAsync(int id);
        Task<bool> UpdatePullsGroupEntityAsync(int idPull, int? idGroup);
        Task<IEnumerable<StoriesGroupsPushes>> GetStorieGroupPushesByProjectIdAsync(int id);
        Task<IEnumerable<StoriesGroupsPulls>> GetStorieGroupPullsByProjectIdAsync(int id);
        Task<bool> UpdateStorieValorAsync(int idstorie, int idgroup, int tipo, int valor);
    }
}
