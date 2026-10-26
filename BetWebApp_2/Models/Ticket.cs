namespace BetWebApp_2.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public List<MyPick> MyPicks { get; set; }= new();
        public DateTime Date { get; set; }
    }
}
