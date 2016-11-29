using System.Collections.Generic;
using Moq;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.Test.Mocks {
    public class MockPlayersFactory : IPlayersFactory {
        private readonly Mock<IPlayersFactory> _mock;

        public MockPlayersFactory() {
            _mock = new Mock<IPlayersFactory>();
        }

        public IEnumerable<IPlayer> Create(GameSettings gameSettings, IIntelligence ai) {
            return _mock.Object.Create(gameSettings, ai);
        }

        public MockPlayersFactory CreateStubbedToReturn(IEnumerable<IPlayer> patterns) {
            _mock.Setup(m => m.Create(It.IsAny<GameSettings>(), It.IsAny<IIntelligence>())).Returns(patterns);
            return this;
        }

        public void VerifyCreatedCalled(GameSettings gameSettings, IIntelligence ai, int times = 1) {
            _mock.Verify(m => m.Create(gameSettings, ai), Times.Exactly(times));
        }

        public void VerifyCreatedNotCalled() {
            _mock.Verify(m => m.Create(It.IsAny<GameSettings>(), It.IsAny<IIntelligence>()), Times.Never);
        }
    }
}