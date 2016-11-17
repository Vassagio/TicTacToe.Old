using System.Collections.Generic;
using Moq;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.Test.Mocks {
    public class MockIntelligenceFactory : IIntelligenceFactory {
        private readonly Mock<IIntelligenceFactory> _mock;

        public MockIntelligenceFactory() {
            _mock = new Mock<IIntelligenceFactory>();
        }

        public IIntelligence Create(GameSettings gameSettings, IEnumerable<IPlayer> players) {
            return _mock.Object.Create(gameSettings, players);
        }

        public MockIntelligenceFactory CreateStubbedToReturn(IIntelligence ai) {
            _mock.Setup(m => m.Create(It.IsAny<GameSettings>(), It.IsAny<IEnumerable<IPlayer>>())).Returns(ai);
            return this;
        }

        public void VerifyCreatedCalled(GameSettings gameSettings, IEnumerable<IPlayer> players, int times = 1) {
            _mock.Verify(m => m.Create(gameSettings, players), Times.Exactly(times));
        }

        public void VerifyCreatedNotCalled() {
            _mock.Verify(m => m.Create(It.IsAny<GameSettings>(), It.IsAny<IEnumerable<IPlayer>>()), Times.Never);
        }
    }
}