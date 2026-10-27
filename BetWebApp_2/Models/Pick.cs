using System.ComponentModel.DataAnnotations;
namespace BetWebApp_2.Models
{
    public class Pick
    {
        public int Id { get; set; }
        [Required]
        public string PickDescription { get; set; } = "";
        public DateTime Date {  get; set; }
        public Prediction FinalPrediction { get; set; } = Prediction.Pending;
        public bool IsDecided => FinalPrediction != Prediction.Pending;
    }
}
