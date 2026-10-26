using Microsoft.AspNetCore.Authentication;

namespace BetWebApp_2.Models
{
    public class MyPick
    {
        public int Id { get; set; }
        public int PickId { get; set; }
        public Pick Pick { get; set; } = null!;
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;

        public Prediction MyPrediction { get; set; }
        public Prediction? FinalPrediction { get; set; }

        public DateTime Date { get; set; }
    }
}
