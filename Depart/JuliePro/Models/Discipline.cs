using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuliePro.Models
{
    public class Discipline
    {
        [Display(Name="Id")] public int Id { get; set; }
        [Display(Name="Name")] public string Name { get; set; }
        [Display(Name="Description")] public string Description { get; set; }
        [ForeignKey("Parent")]
        [Display(Name="Parent_Id")] public int? Parent_Id { get; set; }
        [Display(Name="Parent")] public virtual Discipline Parent { get; set; }
        [Display(Name="Children")] public virtual IList<Discipline> Children { get; set; }
        [Display(Name="TrainerPersonalRecords")] public virtual ICollection<Record> TrainerPersonalRecords { get; set; }
        [Display(Name="Trainers")] public virtual ICollection<Trainer> Trainers { get; set; }

    }



}