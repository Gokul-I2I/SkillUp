using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;

namespace SkillUpBackend.Mapper
{
    public class BatchMapper
    {
        
        public async Task<Batch> BatchCreateOrEditToBatch(BatchCreateOrEdit batchCreateOrEdit)
        {
            return new Batch()
            {
                Name = batchCreateOrEdit.Name,
                InsertedBy = "admin",
                InsertedOn = DateTime.Now
            };
        }
        //public async Task<BatchViewModel> BatchToBatchViewModel(Batch batch)
        //{
        //    return new BatchViewModel()
        //    {
        //        BatchId = batch.Id,
        //        BatchName = batch.Name,
        //        UserCreateOrEdits = batch.BatchUsers?.Select(bu => new UserCreateOrEdit
        //        {
        //            Id = bu.User.Id,
        //            FirstName = bu.User.FirstName,
        //            LastName = bu.User.LastName,
        //            Email = bu.User.Email,
        //            Qualification = bu.User.Qualification,
        //            DateOfBirth = bu.User.DateOfBirth,
        //            Role = bu.User.Role.Name
        //        }).ToList()
        //    };
        //}
    }
}
