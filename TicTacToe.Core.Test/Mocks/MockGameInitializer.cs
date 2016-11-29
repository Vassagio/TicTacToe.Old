using Moq;

namespace TicTacToe.Core.Test.Mocks {
    public class MockGameInitializer : IGameInitializer {

        private readonly Mock<IGameInitializer> _mock;

        public MockGameInitializer() {
            _mock = new Mock<IGameInitializer>();
        }
        public IGame Create(GameSettings gameSettings) {
            return _mock.Object.Create(gameSettings);
        }

        public MockGameInitializer CreateStubbedToReturn(IGame game) {
            _mock.Setup(m => m.Create(It.IsAny<GameSettings>())).Returns(game);
            return this;
        }

        public void VerifyCreateCalled(GameSettings gameSettings, int times = 1) {
            _mock.Verify(m => m.Create(gameSettings), Times.Exactly(times));
        }

        public void VerifyCreateNotCalled() {
            _mock.Verify(m => m.Create(It.IsAny<GameSettings>()), Times.Never);
        }
    }
}
