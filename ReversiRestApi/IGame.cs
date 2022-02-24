namespace ReversiRestApi
{
    public enum Color
    {
        None,
        White,
        Black
    }

    public interface IGame
    {
        int ID { get; set; }
        string Description { get; set; }

        //het unieke token van het spel
        string Token { get; set; }
        string Player1Token { get; set; }
        string Player2Token { get; set; }

        Color[,] Board { get; set; }
        Color PlayerTurn { get; set; }
        void Skip();
        bool Finished();

        //welke kleur het meest voorkomend op het speelbord
        Color ConsideringColor();

        //controle of op een bepaalde positie een zet mogelijk is
        bool MoveIsPossible(int row, int col);
        void MakeMove(int row, int col);
    }
}