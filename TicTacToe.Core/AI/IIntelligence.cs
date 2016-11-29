using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public interface IIntelligence {
        BoardCoordinate DetermineBest(IIntelligenceContext context);
    }
}