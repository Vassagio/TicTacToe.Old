using System.Collections.Generic;
using Moq;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.Test.Mocks {
    public class MockGame : IGame {
        public IBoard Board { get; set; }
        public IEnumerable<IPlayer> Players { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        private readonly Mock<IGame> _mock;

        public MockGame() {
            _mock = new Mock<IGame>();
        }

        public void SwitchPlayer() {
            _mock.Object.SwitchPlayer();
        }

        public bool IsOver() {
            return _mock.Object.IsOver();
        }

        public void MakeMove(IIntelligenceContext context) {
            _mock.Object.MakeMove(context);
        }

        public MockGame IsOverStubbedToReturn(bool isOver) {
            _mock.Setup(m => m.IsOver()).Returns(isOver);
            return this;
        }

        public void VerifySwitchPlayerCalled(int times = 1) {
            _mock.Verify(m => m.SwitchPlayer(), Times.Exactly(times));
        }

        public void VerifySwitchPlayerNotCalled() {
            _mock.Verify(m => m.SwitchPlayer(), Times.Never);
        }

        public void VerifyIsOverCalled(int times = 1) {
            _mock.Verify(m => m.IsOver(), Times.Exactly(times));
        }

        public void VerifyIsOverNotCalled() {
            _mock.Verify(m => m.IsOver(), Times.Never);
        }

        public void VerifyMakeMoveCalled(IIntelligenceContext context, int times = 1) {
            _mock.Verify(m => m.MakeMove(context), Times.Exactly(times));
        }

        public void VerifyMakeMoveNotCalled() {
            _mock.Verify(m => m.MakeMove(It.IsAny<IIntelligenceContext>()), Times.Never);
        }
    }
}