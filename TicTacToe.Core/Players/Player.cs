using System.Linq;
using System.Text.RegularExpressions;
using TicTacToe.Core.AI;

namespace TicTacToe.Core.Players {
    public abstract class Player : IPlayer {
        public string Name { get; }
        public char Symbol { get; }
        protected readonly IIntelligence AI;
        protected Player() {}

        protected Player(PlayerSettings settings) {
            Name = settings.Name;
            Symbol = settings.Symbol;
            AI = settings.Intelligence;
        }

        public void ChoosePosition(IBoard board, int position) {
            var coordinate = board.ToCoordinate(position);
            board.SetCoordinate(this, coordinate);
        }

        public bool HasWon(IBoard board) {
            var currentPattern = board.GetCurrentPattern(this);
            foreach (var winningPattern in board.WinningPatterns) {
                var r = new Regex(winningPattern);
                var m = r.Match(currentPattern);
                if (m.Success)
                    return true;
            }

            return false;
        }

        public BoardCoordinate GetBestMove(IIntelligenceContext context) {
            return AI.DetermineBest(context);
        }
        public IIntelligence GetIntelligence() {
            return AI;
        }

        public override string ToString() {
            return $"{Name}: {Symbol}";
        }
    }
}