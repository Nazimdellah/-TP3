using JuliePro.Models;

namespace JuliePro.Services
{
    public interface IRecordService : IServiceBaseAsync<RecordViewModel>
    {
        public Task PopulateTrainersDisciplinesAsync(RecordViewModel model);
        public Task<TrainerRecordViewModel> GetAllByTrainerIdAsync(int trainerId);
    }
}
