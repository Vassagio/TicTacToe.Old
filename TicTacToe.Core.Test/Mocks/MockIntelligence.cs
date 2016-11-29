using System;
using System.Collections.Generic;
using Moq;
using TicTacToe.Core.AI;

namespace TicTacToe.Core.Test.Mocks {
    public class MockIntelligence : IIntelligence {
        private readonly Mock<IIntelligence> _mock;

        public MockIntelligence() {
            _mock = new Mock<IIntelligence>();
        }

        public BoardCoordinate DetermineBest(IIntelligenceContext context) {
            return _mock.Object.DetermineBest(context);
        }

        public MockIntelligence DetermineBestStubbedToReturn(BoardCoordinate coordinate) {
            _mock.Setup(m => m.DetermineBest(It.IsAny<IIntelligenceContext>())).Returns(coordinate);
            return this;
        }

        public MockIntelligence DetermineBestStubbedToReturn(params BoardCoordinate[] coordinates) {
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