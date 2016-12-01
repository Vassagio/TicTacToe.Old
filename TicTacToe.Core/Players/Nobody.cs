using TicTacToe.Core.AI;

namespace TicTacToe.Core.Players {
    public class Nobody : IPlayer {
        public string Name => "Nobody";
        public char Symbol => ' ';

        public void ChoosePosition(IBoard board, int position) {}

        public bool HasWon(IBoard board) {
            return false;
        }

        public BoardCoordinate GetBestMove(IIntelligenceContext context) {
            return null;
        }

        public IIntelligence GetIntelligence() {
            return null;
        }
    }
}