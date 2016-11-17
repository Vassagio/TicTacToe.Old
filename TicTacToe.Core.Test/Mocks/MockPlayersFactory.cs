using System.Collections.Generic;
using Moq;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.Test.Mocks {
    public class MockPlayersFactory : IPlayersFactory {
        private readonly Mock<IPlayersFactory> _mock;

        public MockPlayersFactory() {
            _mock = new Mock<IPlayersFactory>();
        }

        public IEnumerable<IPlayer> Create(GameSettings gameSettings) {
            return _mock.Object.Create(gameSettings);
        }

        public MockPlayersFactory CreateStubbedToReturn(IEnumerable<IPlayer> patterns) {
            _mock.Setup(m => m.Create(It.IsAny<GameSettings>())).Returns(patterns);
            return this;
        }

        public void VerifyCreatedCalled(GameSettings gameSettings, int times = 1) {
            _mock.Verify(m => m.Create(gameSettings), Times.Exactly(times));
        }

        public void VerifyCreatedNotCalled() {
            _mock.Verify(m => m.Create(It.IsAny<GameSettings>()), Times.Never);
        }
    }
}
