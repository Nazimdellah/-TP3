using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JuliePro.Models
{
    public class TrainerRecordViewModel {

        public TrainerRecordViewModel(Trainer trainer, IEnumerable<RecordViewModel> records)
        {
            Trainer = trainer;
            Records = records;
        }

        public Trainer Trainer { get; set; }
        public IEnumerable<RecordViewModel> Records { get; set; }

    }


    public class TrainersDisciplinesViewModel {


        public TrainersDisciplinesViewModel(List<Trainer> trainers, List<Discipline> disciplines)
        {
            var listItems = trainers.Select(x => new SelectListItem() { Text = x.FullName, Value = x.Id.ToString() }).ToList();
            this.Trainers = new SelectList(listItems, "Value", "Text");
            this.Disciplines = new SelectList(disciplines, "Id", "Name");
        }

        [Display(Name = "Trainers")] public SelectList Trainers { get; set; }
        [Display(Name = "Disciplines")] public SelectList Disciplines { get; set; }


    }

    public class RecordViewModel
    {
        public RecordViewModel()
        {

        }

        public RecordViewModel(Record model, List<Trainer> trainers, List<Discipline> disciplines)
        {
            this.Id = model.Id;
            this.Date = model.Date;
            this.Amount = model.Amount;
            this.Unit = model.Unit;
            this.DisciplineName = model.Discipline?.Name ?? "N/A";
            this.TrainerName = model.Trainer?.FullName ?? "N/A";
            this.AvailableOptions = new TrainersDisciplinesViewModel(trainers, disciplines);
        }

        [Display(Name="Id")] public int Id { get; set; } = 0;
        [Display(Name="Date")] public DateTime Date { get; set; }
        [Display(Name = "Amount")] public decimal Amount { get;  set; }
        [Display(Name = "Unit")] public string Unit { get;  set; }
        [Display(Name = "Discipline")] public string? DisciplineName { get; private set; }
        [Display(Name = "Trainer")] public string? TrainerName { get; private set; }

        [Display(Name="AmountWithUnit")]  public string? AmountWithUnit { get { return this.Amount.ToString("F2") + " " + this.Unit;  } }

        [Display(Name="SelectedTrainerId")] public int SelectedTrainerId { get; set; }        
        [Display(Name="SelectedDisciplineId")] public int SelectedDisciplineId { get; set; }

        public TrainersDisciplinesViewModel AvailableOptions { get; set; }


        public Record ToModel() { 
        
            var result = new Record();
            result.Id = Id;
            result.Date = Date;
            result.Amount = Amount;
            result.Unit = Unit;
            result.Discipline_Id = SelectedDisciplineId;
            result.Trainer_Id = SelectedTrainerId;
            return result;
        }
    }
}
