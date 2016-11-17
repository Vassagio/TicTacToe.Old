using Moq;

namespace TicTacToe.Core.Test.Mocks {
    public class MockToken : IToken {
        private readonly Mock<IToken> _mock;
        
        public MockToken() {
            _mock = new Mock<IToken>();
        }

        public char Symbol { get; set; }
    }
}
