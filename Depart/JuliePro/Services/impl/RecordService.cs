using JuliePro.Data;
using JuliePro.Models;
using Microsoft.EntityFrameworkCore;

namespace JuliePro.Services.impl
{
    public class RecordService : ServiceBaseEF<Record>, IRecordService
    {
        public RecordService(JulieProDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<RecordViewModel> CreateAsync(RecordViewModel entity)
        {
            var model = entity.ToModel();
            await base.CreateAsync(model);
            entity.Id = model.Id;
            return entity;
        }

        public override async Task DeleteAsync(int id)
        {
           await base.DeleteAsync(id);
        }

        public async Task EditAsync(RecordViewModel entity)
        {
            var model = entity.ToModel();
            await base.EditAsync(model);
        }

        public new async Task<IReadOnlyList<RecordViewModel>> GetAllAsync()
        {
            var all = await base.GetAllAsync();
            var disciplines = await this._dbContext.Disciplines.ToListAsync();
            var trainers = await this._dbContext.Trainers.ToListAsync();
            return all.Select(x => new RecordViewModel(x, trainers, disciplines)).ToList();
        }

        public async Task<TrainerRecordViewModel> GetAllByTrainerIdAsync(int trainerId)
        {
            var all =  await this._dbContext.Records.Where(x=>x.Trainer_Id==trainerId).ToListAsync();
            var disciplines = await this._dbContext.Disciplines.ToListAsync();
            var trainers = await this._dbContext.Trainers.ToListAsync();
            var records = all.Select(x => new RecordViewModel(x, trainers, disciplines)).ToList();
            var result = new TrainerRecordViewModel(trainers.FirstOrDefault(x=>x.Id==trainerId),records);
            return result;
        }

        public new async Task<IPaginatedList<RecordViewModel>> GetAllPaginatedAsync(int pageIndex, int pageSize)
        {
            
            var all = await base.GetAllPaginatedAsync(pageIndex,pageSize);
            var disciplines = await this._dbContext.Disciplines.ToListAsync();
            var trainers = await this._dbContext.Trainers.ToListAsync();
            var vms = all.Select(x => new RecordViewModel(x, trainers, disciplines)).ToList();
            return new PaginatedList<RecordViewModel>(all.PageIndex, all.PageSize, all.TotalCount, all.TotalPages, vms);
        }

        public new async Task<RecordViewModel> GetByIdAsync(int id)
        {
            var model = await base.GetByIdAsync(id);
            var disciplines = await this._dbContext.Disciplines.ToListAsync();
            var trainers = await this._dbContext.Trainers.ToListAsync();
            return new RecordViewModel(model,  trainers,disciplines);
        }

        public async Task PopulateTrainersDisciplinesAsync(RecordViewModel model)
        {
            var disciplines = await this._dbContext.Disciplines.ToListAsync();
            var trainers = await this._dbContext.Trainers.ToListAsync();
            model.AvailableOptions = new TrainersDisciplinesViewModel(trainers, disciplines);
        }
    }
}
