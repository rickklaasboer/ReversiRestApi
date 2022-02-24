namespace ReversiRestApi.Requests.Game
{
    public class DoTurnRequest
    {
        public string PlayerToken { get; set; }
        
        public string GameToken { get; set; }
        
        public (int row, int column) Position { get; set; }
    }
}