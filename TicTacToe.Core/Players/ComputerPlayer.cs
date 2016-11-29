using TicTacToe.Core.AI;

namespace TicTacToe.Core.Players {
    public class ComputerPlayer : Player {
        public ComputerPlayer(PlayerSettings settings) : base(settings) {}
        public override BoardCoordinate GetBestMove(IIntelligenceContext context) {
            return AI.DetermineBest(context);
        }
    }
}