using Moq;
using TicTacToe.Core.AI;

namespace TicTacToe.Core.Test.Mocks {
    public class MockIntelligenceContextFactory : IIntelligenceContextFactory {
        private readonly Mock<IIntelligenceContextFactory> _mock;

        public MockIntelligenceContextFactory() {
            _mock = new Mock<IIntelligenceContextFactory>();
        }

        public IIntelligenceContext Create(IGame game) {
            return _mock.Object.Create(game);
        }

        public MockIntelligenceContextFactory CreateStubbedToReturn(IIntelligenceContext aiContext) {
            _mock.Setup(m => m.Create(It.IsAny<IGame>())).Returns(aiContext);
            return this;
        }

        public void VerifyCreatedCalled(IGame game, int times = 1) {
            _mock.Verify(m => m.Create(game), Times.Exactly(times));
        }

        public void VerifyCreatedNotCalled() {
            _mock.Verify(m => m.Create(It.IsAny<IGame>()), Times.Never);
        }
    }
}