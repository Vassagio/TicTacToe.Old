using System;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public class EmptyIntelligence : IIntelligence {
        public BoardCoordinate DetermineBest(IIntelligenceContext context) {
            throw new NotImplementedException();
        }
    }
}