using Moq;
using TicTacToe.Core.AI;

namespace TicTacToe.Core.Test.Mocks {
    public class MockIntelligenceFactory: IIntelligenceFactory {
        private readonly Mock<IIntelligenceFactory> _mock;

        public MockIntelligenceFactory() {
            _mock = new Mock<IIntelligenceFactory>();
        }

        public IIntelligence Create(GameSettings gameSettings) {
            return _mock.Object.Create(gameSettings);
        }

        public MockIntelligenceFactory CreateStubbedToReturn(IIntelligence ai) {
            _mock.Setup(m => m.Create(It.IsAny<GameSettings>())).Returns(ai);
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
