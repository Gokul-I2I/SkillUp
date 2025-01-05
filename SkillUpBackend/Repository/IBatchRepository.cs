using Microsoft.AspNetCore.Mvc;
using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;
using System.Collections.ObjectModel;

namespace SkillUpBackend.Repository
{
    public interface IBatchRepository
    {
        Task<int> CreateBatch(Batch batch);
        Task<Batch> GetBatchById(int id);
        Task<List<Batch>> GetBatches();
        Task UpdateBatch(Batch batch);
        Task<BatchViewModel> GetBatchByIdWithUsers(int id);
        Task AddUserToBatch(BatchCreateOrEdit batchCreateOrEdit);
        Task RemoveUserFromBatch(BatchCreateOrEdit batchCreateOrEdit);
        Task ActiveUser(Collection<int> excitingUser, int batchId);
    }
}
