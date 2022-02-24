namespace ReversiRestApi.Requests.Game
{
    public class CreateGameRequest
    {
        public string Player1Token { get; set; }
        public string Description { get; set; }
    }
}