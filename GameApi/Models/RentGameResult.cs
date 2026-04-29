namespace GamesCrudApi.Models
{
    public class RentGameResult
    {
        public bool Success { get; set; }

        public string Message { get; set; } = "";

        public Game? Game { get; set; }
    }
}
