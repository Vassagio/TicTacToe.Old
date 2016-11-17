using System.Linq;

namespace TicTacToe.Core.Players {
    public abstract class Player : IPlayer {
        public string Name { get; }
        public char Symbol { get; }

        protected Player() {}

        protected Player(PlayerSettings settings) {
            Name = settings.Name;
            Symbol = settings.Symbol;
        }

        public void ChoosePosition(IBoard board, int position) {
            var token = new Token(this);
            board.AddToken(token, board.ToCoordinate(position));
        }

        public bool HasWon(IBoard board) {
            return board.WinningPatterns.Contains(board.GetCurrentPattern(this));
        }

        public override string ToString() {
            return $"{Name}: {Symbol}";
        }
    }
}