using System;
using System.Collections.Generic;
using Moq;
using TicTacToe.Core.AI;
using TicTacToe.Core.AI.Human;

namespace TicTacToe.Core.Test.Mocks {
    public class MockHumanIntelligence : IHumanIntelligence {
        private readonly Mock<IHumanIntelligence> _mock;

        public MockHumanIntelligence() {
            _mock = new Mock<IHumanIntelligence>();
        }

        public BoardCoordinate DetermineBest(IIntelligenceContext context) {
            return _mock.Object.DetermineBest(context);
        }

        public MockHumanIntelligence DetermineBestStubbedToReturn(BoardCoordinate coordinate) {
            _mock.Setup(m => m.DetermineBest(It.IsAny<IIntelligenceContext>())).Returns(coordinate);
            return this;
        }

        public MockHumanIntelligence DetermineBestStubbedToReturn(params BoardCoordinate[] coordinates) {
            var queue = new Queue<BoardCoordinate>(coordinates);
            _mock.Setup(m => m.DetermineBest(It.IsAny<IIntelligenceContext>())).Returns(queue.Dequeue());
            return this;
        }

        public void VerifyDetermineBestCalled(IIntelligenceContext context, int times = 1) {
            _mock.Verify(m => m.DetermineBest(context), Times.Exactly(times));
        }

        public void VerifyDetermineBestNotCalled() {
            _mock.Verify(m => m.DetermineBest(It.IsAny<IIntelligenceContext>()), Times.Never);
        }
    }
}
