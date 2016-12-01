using System;
using Moq;

namespace TicTacToe.Core.Test.Mocks {
    public class MockInputOutput : IInputOutput {
        private readonly Mock<IInputOutput> _mock;

        public MockInputOutput() {
            _mock = new Mock<IInputOutput>();
        }

        public void Write(string text) {
            _mock.Object.Write(text);
        }

        public string Read(string question) {
            return _mock.Object.Read(question);
        }

        public MockInputOutput ReadStubbedToReturn(string text) {
            _mock.Setup(m => m.Read(It.IsAny<string>())).Returns(text);
            return this;
        }

        public void VerifyWriteCalled(string text, int times = 1) {
            _mock.Verify(m => m.Write(text), Times.Exactly(times));
        }

        public void VerifyWriteNotCalled() {
            _mock.Verify(m => m.Write(It.IsAny<string>()), Times.Never);
        }

        public void VerifyReadCalled(string text, int times = 1) {
            _mock.Verify(m => m.Read(text), Times.Exactly(times));
        }

        public void VerifyReadNotCalled() {
            _mock.Verify(m => m.Read(It.IsAny<string>()), Times.Never);
        }
    }
}