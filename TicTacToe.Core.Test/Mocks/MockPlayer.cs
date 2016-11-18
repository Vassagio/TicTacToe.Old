using Moq;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.Test.Mocks {
    public class MockPlayer : IPlayer {
        public string Name { get; set; }
        public char Symbol { get; set; }
        private readonly Mock<IPlayer> _mock;

        public MockPlayer() {
            _mock = new Mock<IPlayer>();
        }

        public void ChoosePosition(IBoard board, int position) {
            _mock.Object.ChoosePosition(board, position);
        }

        public bool HasWon(IBoard board) {
            return _mock.Object.HasWon(board);
        }

        public MockPlayer HasWonStubbedToReturn(bool hasWon) {
            _mock.Setup(m => m.HasWon(It.IsAny<IBoard>())).Returns(hasWon);
            return this;
        }    

        public void VerifyChoosePositionCalled(IBoard board, int position, int times = 1) {
            _mock.Verify(m => m.ChoosePosition(board, position), Times.Exactly(times));
        }

        public void VerifyChoosePositionNotCalled() {
            _mock.Verify(m => m.ChoosePosition(It.IsAny<IBoard>(), It.IsAny<int>()), Times.Never);
        }

        public void VerifyHasWonCalled(IBoard board, int times = 1) {
            _mock.Verify(m => m.HasWon(board), Times.Exactly(times));
        }

        public void VerifyHasWonNotCalled() {
            _mock.Verify(m => m.HasWon(It.IsAny<IBoard>()), Times.Never);
        }
    }
}