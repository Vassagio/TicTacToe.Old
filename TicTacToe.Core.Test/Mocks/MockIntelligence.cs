using System.Collections.Generic;
using Moq;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.Test.Mocks {
    public class MockIntelligence: IIntelligence {
        private readonly Mock<IIntelligence> _mock;

        public MockIntelligence() {
            _mock = new Mock<IIntelligence>();
        }

        public BoardCoordinate GetBestMove(IBoard board, IEnumerable<IPlayer> players, IPlayer originalPlayer, IPlayer currentPlayer) {
            return _mock.Object.GetBestMove(board, players, originalPlayer, currentPlayer);
        }

        public MockIntelligence CreateStubbedToReturn(BoardCoordinate coordinate) {
            _mock.Setup(m => m.GetBestMove(It.IsAny<IBoard>(), It.IsAny<IEnumerable<IPlayer>>(), It.IsAny<IPlayer>(), It.IsAny<IPlayer>())).Returns(coordinate);
            return this;
        }

        public void VerifyCreatedCalled(IBoard board, IEnumerable<IPlayer> players, IPlayer originalPlayer, IPlayer currentPlayer, int times = 1) {
            _mock.Verify(m => m.GetBestMove(board, players, originalPlayer, currentPlayer), Times.Exactly(times));
        }

        public void VerifyCreatedNotCalled() {
            _mock.Verify(m => m.GetBestMove(It.IsAny<IBoard>(), It.IsAny<IEnumerable<IPlayer>>(), It.IsAny<IPlayer>(), It.IsAny<IPlayer>()), Times.Never);
        }
    }
}
