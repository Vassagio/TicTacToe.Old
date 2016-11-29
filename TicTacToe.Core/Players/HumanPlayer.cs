using TicTacToe.Core.AI;

namespace TicTacToe.Core.Players {
    public class HumanPlayer : Player {
        public HumanPlayer(PlayerSettings settings) : base(settings) {}
        public override BoardCoordinate GetBestMove(IIntelligenceContext context) {
            throw new System.NotImplementedException();
        }
    }
}