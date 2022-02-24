namespace ReversiRestApi.Requests.Game
{
    public class AbandonTurnRequest
    {
        public string PlayerToken { get; set; }
        
        public string GameToken { get; set; }
    }
}