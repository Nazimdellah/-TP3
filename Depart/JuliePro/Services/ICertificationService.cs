using JuliePro.Models;

namespace JuliePro.Services
{
    public interface ICertificationService : IServiceBaseAsync<Certification>
    {
        public Task<TrainerCertificationViewModel> GetAllByTrainerAsync(int trainerId);
    }
}
