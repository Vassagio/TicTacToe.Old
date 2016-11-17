using System;
using Moq;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.Test.Mocks {
    public class MockIntelligence : IIntelligence {
        private readonly Mock<IIntelligence> _mock;

        public MockIntelligence() {
            _mock = new Mock<IIntelligence>();
        }

        public BoardCoordinate DetermineBest(IBoard board, IPlayer minimizePlayer, IPlayer maximizedPlayer) {
            throw new NotImplementedException();
        }
    }
}