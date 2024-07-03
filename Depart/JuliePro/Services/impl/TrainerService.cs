using JuliePro.Data;
using JuliePro.Models;
using JuliePro.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JuliePro.Services.impl
{
    public class TrainerService : ServiceBaseEF<Trainer>, ITrainerService
    {
        private IImageFileManager fileManager;

        public TrainerService(JulieProDbContext dbContext, IImageFileManager fileManager) : base(dbContext)
        {
            this.fileManager = fileManager;
        }

        public async Task<Trainer> CreateAsync(Trainer model, IFormCollection form)
        {
            model.Photo = await fileManager.UploadImage(form, false, null);

            return await base.CreateAsync(model);
        }

        public async Task EditAsync(Trainer model, IFormCollection form)
        {
            var old = await _dbContext.Trainers.Where(x=>x.Id == model.Id).Select(x=>x.Photo).FirstOrDefaultAsync();
            model.Photo = await fileManager.UploadImage(form, true, old);
            await this.EditAsync(model);
        }

        public async Task<TrainerSearchViewModel> GetAllAsync(TrainerSearchViewModelFilter filter)
        {
            filter.VerifyProperties();//mets à null les éléments qui sont vides. 

            var result = new TrainerSearchViewModel(filter);

            //Compléter le filtre pour qu'il soit fonctionel

            /***********Partie1***************/
            result.Items = await this._dbContext.Trainers
                .Where(i => filter.SearchNameText == null || (i.FirstName + " " + i.LastName).ToLower().Contains(filter.SearchNameText.ToLower()))
                /**Ajouter des Where pour les critères Descipline et Genre**/
              
                .ToPaginatedAsync(result.SelectedPageIndex, result.SelectedPageSize);

            result.AvailablePageSizes = new SelectList(new List<int>() { 9, 12, 18, 21 });
            result.Disciplines = new SelectList(await this._dbContext.Disciplines.ToListAsync(), "Id", "Name");
          
          

            return result;
        }
    }
}
