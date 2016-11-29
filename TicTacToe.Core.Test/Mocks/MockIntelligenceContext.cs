using Moq;
using TicTacToe.Core.AI;

namespace TicTacToe.Core.Test.Mocks {
    public class MockIntelligenceContext : IIntelligenceContext {
        private readonly Mock<IIntelligenceContext> _mock;

        public MockIntelligenceContext() {
            _mock = new Mock<IIntelligenceContext>();
        }
    }
}
