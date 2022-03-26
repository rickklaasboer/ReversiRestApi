using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ReversiRestApi
{
    public class Game : IGame
    {
        private const int Scope = 8;

        private readonly int[,] _direction = new int[8, 2]
        {
            { 0, 1 }, // right
            { 0, -1 }, // left
            { 1, 0 }, // down
            { -1, 0 }, // up
            { 1, 1 }, // to bottom right
            { 1, -1 }, // to bottom left
            { -1, 1 }, // to top right
            { -1, -1 } // to top left
        };

        public Game()
        {
            Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            Token = Token.Replace("/", "q");
            Token = Token.Replace("+", "r");

            Board = new Color[Scope, Scope];
            Board[3, 3] = Color.White;
            Board[4, 4] = Color.White;
            Board[3, 4] = Color.Black;
            Board[4, 3] = Color.Black;

            PlayerTurn = Color.None;
        }
        
        [Key]
        public string Token { get; set; }

        // public int ID { get; set; }
        public string Description { get; set; }
        
        public string Player1Token { get; set; }
        public string Player2Token { get; set; }

        public Color[,] Board { get; set; }

        public Color PlayerTurn { get; set; }

        [NotMapped]
        public bool IsFinished => Finished();

        [NotMapped]
        public Color Winner => WinningColor();

        public void Skip()
        {
            // Check that no move is possible for the player who wants to pass before switching turns.
            if (CanDoValidMove(PlayerTurn))
            {
                throw new Exception("Passen mag niet, er is nog een zet mogelijk");
            }

            ToggleTurn();
        }


        public bool Finished()
        {
            // True if none of the players can make a move
            return !(CanDoValidMove(Color.White) && CanDoValidMove(Color.Black));
        }

        public Color WinningColor()
        {
            if (!Finished())
            {
                return Color.None;
            }

            var white = 0;
            var black = 0;
            for (var row = 0; row < Scope; row++)
            for (var col = 0; col < Scope; col++)
                if (Board[row, col] == Color.White)
                    white++;
                else if (Board[row, col] == Color.Black)
                    black++;

            if (white > black)
                return Color.White;
            if (black > white)
                return Color.Black;
            return Color.None;
        }

        public bool MoveIsPossible(int row, int col)
        {
            if (!PositionWithinBoard(row, col))
                throw new Exception($"Zet ({row},{col}) ligt buiten het bord!");
            return MoveIsPossible(row, col, PlayerTurn);
        }

        public void MakeMove(int row, int col)
        {
            if (!MoveIsPossible(row, col))
            {
                throw new Exception($"Zet ({row},{col}) is niet mogelijk!");
            }

            bool didTurn = false;

            for (var i = 0; i < 8; i++)
            {
                var result = FlipOpponentBricksInDirectionIfTrapped(
                    row,
                    col,
                    PlayerTurn,
                    _direction[i, 0],
                    _direction[i, 1]
                );

                didTurn = didTurn ? didTurn : result;
            }

            Board[row, col] = PlayerTurn;
            ToggleTurn();
        }

        private static Color GetOpponentColor(Color color)
        {
            if (color == Color.White)
                return Color.Black;
            if (color == Color.Black)
                return Color.White;
            return Color.None;
        }

        private bool CanDoValidMove(Color color)
        {
            if (color == Color.None)
            {
                throw new Exception("Kleur mag niet gelijk aan Geen zijn!");
            }

            // Check if move is possible for color
            for (var row = 0; row < Scope; row++)
            {
                for (var col = 0; col < Scope; col++)
                {
                    if (MoveIsPossible(row, col, color))
                        return true;
                }
            }

            return false;
        }

        private bool MoveIsPossible(int row, int col, Color color)
        {
            // True if move is possible in any direction
            for (var i = 0; i < 8; i++)
            {
                if (StonesToEmbedInDirection(
                        row,
                        col,
                        color,
                        _direction[i, 0],
                        _direction[i, 1])
                   )
                {
                    return true;
                }
            }

            return false;
        }

        private void ToggleTurn()
        {
            if (PlayerTurn == Color.White)
                PlayerTurn = Color.Black;
            else
                PlayerTurn = Color.White;
        }

        private static bool PositionWithinBoard(int row, int col)
        {
            return row is >= 0 and < Scope && col is >= 0 and < Scope;
        }

        private bool PutOnBoardAndFree(int row, int col)
        {
            // True if placed on board, and field is still free
            return PositionWithinBoard(row, col) && Board[row, col] == Color.None;
        }

        private bool StonesToEmbedInDirection(int row, int col,
            Color colorSetter,
            int rowDirection, int colDirection)
        {
            int tempRow, tempCol;
            var kleurTegenstander = GetOpponentColor(colorSetter);
            if (!PutOnBoardAndFree(row, col))
                return false;

            // Put row and column on the index for the first box next to the move.
            tempRow = row + rowDirection;
            tempCol = col + colDirection;

            var numberOfAdjacentStonesFromOpponent = 0;

            // As long as Board[row,column] is not outside the board boundaries, and you are in the next box
            // always hits the opponent's color, look one more square.
            // Board[row, column] is ultimately outside the board boundaries, or no longer has the
            // the color of the opponent.
            // Note: part after && will only be executed if condition before that is true.
            while (PositionWithinBoard(tempRow, tempCol) && Board[tempRow, tempCol] == kleurTegenstander)
            {
                tempRow += rowDirection;
                tempCol += colDirection;
                numberOfAdjacentStonesFromOpponent++;
            }

            // Now you see how you ended with the above loop. Alone
            // if all three conditions below are true, there are in the
            // specified direction to embed stones.
            return PositionWithinBoard(tempRow, tempCol) &&
                   Board[tempRow, tempCol] == colorSetter &&
                   numberOfAdjacentStonesFromOpponent > 0;
        }

        private bool FlipOpponentBricksInDirectionIfTrapped(
            int row,
            int col,
            Color colorSetter,
            int rowDirection,
            int colDirection
        )
        {
            int tempRow, tempCol;
            var opponentColor = GetOpponentColor(colorSetter);
            var stonedTurned = false;

            if (StonesToEmbedInDirection(row, col, colorSetter, rowDirection, colDirection))
            {
                tempRow = row + rowDirection;
                tempCol = col + colDirection;

                // N.b.: je weet zeker dat je niet buiten het bord belandt,
                // omdat de stenen van de tegenstander ingesloten zijn door
                // een steen van degene die de zet doet.
                while (Board[tempRow, tempCol] == opponentColor)
                {
                    Board[tempRow, tempCol] = colorSetter;
                    tempRow += rowDirection;
                    tempCol += colDirection;
                }

                stonedTurned = true;
            }

            return stonedTurned;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}