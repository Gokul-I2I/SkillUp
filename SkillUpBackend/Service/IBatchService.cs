using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Service
{
    public interface IBatchService
    {
        Task<int> CreateBatch(BatchCreateOrEdit batchCreateOrEdit);
        Task DeleteBatch(int id);
        Task<Batch> GetBatchById(int id);
        Task<List<Batch>> GetBatches();
        Task UpdateBatch(int id, BatchCreateOrEdit batchCreateOrEdit);
        Task<BatchViewModel> GetBatchByIdWithUsers(int id);
        Task AddUserToBatch(BatchCreateOrEdit batchCreateOrEdit, int id);
        Task RemoveUserFromBatch(BatchCreateOrEdit batchCreateOrEdit, int id);
        Task ActiveBatch(int id);
    }
}
