namespace CityTransport.Models
{
    public class LineUser
    {
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int LineId { get; set; }

        public virtual Line Line { get; set; }
    }
}
