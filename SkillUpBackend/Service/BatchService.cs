using SkillUpBackend.Mapper;
using SkillUpBackend.Model;
using SkillUpBackend.Repository;
using SkillUpBackend.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace SkillUpBackend.Service
{
    public class BatchService : IBatchService
    {
        private readonly IBatchRepository _batchRepository;
        private readonly IUserService _userService;
        private readonly BatchMapper _batchMapper;
        public BatchService(IBatchRepository batchRepository, BatchMapper batchMapper, IUserService userService)
        {
            _batchRepository = batchRepository;
            _batchMapper = batchMapper;
            _userService = userService;
        }

        public async Task<int> CreateBatch(BatchCreateOrEdit batchCreateOrEdit)
        {
            Batch batch = await _batchMapper.BatchCreateOrEditToBatch(batchCreateOrEdit);
            return await _batchRepository.CreateBatch(batch);
        }

        public async Task DeleteBatch(int id)
        {
            var batch = await _batchRepository.GetBatchById(id);
            batch.IsActive = false;
            batch.UpdatedOn = DateTime.UtcNow;
            batch.UpdatedBy = "Admin";
            await _batchRepository.UpdateBatch(batch);
        }
        public async Task UpdateBatch(int id, BatchCreateOrEdit batchCreateOrEdit)
        {
            var batch = await _batchRepository.GetBatchById(id);
            batch.Name = batchCreateOrEdit.Name;
            batch.UpdatedBy = "Admin";
            batch.UpdatedOn = DateTime.Now;
            await _batchRepository.UpdateBatch(batch);
        }

        public async Task<BatchViewModel> GetBatchByIdWithUsers(int id)
        {
            return await _batchRepository.GetBatchByIdWithUsers(id);
        }

        public async Task<IEnumerable<Batch>> GetBatches()
        {
            return await _batchRepository.GetBatches();
        }

        public async Task<Batch> GetBatchById(int id)
        {
            return await _batchRepository.GetBatchById(id);
        }

        public async Task AddUserToBatch(BatchCreateOrEdit batchCreateOrEdit, int id)
        {
            var batchUsers = await _batchRepository.GetBatchByIdWithUsers(id);
            batchCreateOrEdit.Id = id;
            batchCreateOrEdit.InsertedBy = "admin";
            foreach(var userId in batchCreateOrEdit.UserId)
            {
                _userService.GetUserById(userId);
            }
            batchCreateOrEdit.UserId = await CheckUserDeactive(batchCreateOrEdit.UserId, batchUsers.UserCreateOrEdits, id);
            
            await _batchRepository.AddUserToBatch(batchCreateOrEdit);
        }

        public async Task RemoveUserFromBatch(BatchCreateOrEdit batchCreateOrEdit, int id)
        {
            var batchUsers = _batchRepository.GetBatchByIdWithUsers(id);
            batchCreateOrEdit.Id = id;
            batchCreateOrEdit.UpdatedBy = "admin";
            foreach (var userId in batchCreateOrEdit.UserId)
            {
                _userService.GetUserById(userId);
            }
            await _batchRepository.RemoveUserFromBatch(batchCreateOrEdit);
        }
        private async Task<ICollection<int>> CheckUserDeactive(ICollection<int> userId, ICollection<UserCreateOrEdit> userCreateOrEdits, int batchId)
        {
            var excitingUser = new Collection<int>();
            foreach(var user in userCreateOrEdits)
            {
                if (userId.Contains((int)user.Id))
                {
                    userId.Remove((int)user.Id);
                    excitingUser.Add((int)user.Id);
                }
            }
            if (excitingUser.Any())
            {
                await _batchRepository.ActiveUser(excitingUser, batchId);
            }
            return userId;
        }
    }
}