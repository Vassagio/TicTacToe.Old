using System.Collections.Generic;
using Moq;

namespace TicTacToe.Core.Test.Mocks {
    public class MockPatternFactory : IPatternFactory {

        private readonly Mock<IPatternFactory> _mock;

        public MockPatternFactory() {
            _mock = new Mock<IPatternFactory>();
        }
        public IEnumerable<string> Create(int size) {
            return _mock.Object.Create(size);
        }

        public MockPatternFactory CreateStubbedToReturn(IEnumerable<string> patterns) {
            _mock.Setup(m => m.Create(It.IsAny<int>())).Returns(patterns);
            return this;
        }

        public void VerifyCreatedCalled(int size, int times = 1) {
            _mock.Verify(m => m.Create(size), Times.Exactly(times));
        }

        public void VerifyCreatedNotCalled() {
            _mock.Verify(m => m.Create(It.IsAny<int>()), Times.Never);
        }
    }
}
