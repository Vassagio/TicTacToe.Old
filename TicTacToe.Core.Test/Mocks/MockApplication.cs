using Moq;

namespace TicTacToe.Core.Test.Mocks {
    public class MockApplication : IApplication {
        private readonly Mock<IApplication> _mock;

        public MockApplication() {
            _mock = new Mock<IApplication>();
        }

        public void Run(GameSettings gameSettings) {
            _mock.Object.Run(gameSettings);
        }

        public void VerifyRunCalled(GameSettings gameSettings, int times = 1) {
            _mock.Verify(m => m.Run(gameSettings), Times.Exactly(times));
        }

        public void VerifyRunNotCalled() {
            _mock.Verify(m => m.Run(It.IsAny<GameSettings>()), Times.Never);
        }
    }
}