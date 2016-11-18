using FluentAssertions;
using TicTacToe.Core;
using TicTacToe.Core.AI;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Console.UI.Tests {
    public class ApplicationTest {
        [Fact]
        public void Initializes_A_New_Application() {
            var application = BuildApplication();

            application.Should().NotBeNull();
            application.Should().BeOfType<Application>();
            application.Should().BeAssignableTo<IApplication>();
        }

        [Fact]
        public void Creates_A_New_Game() {
            var game = new MockGame();
            var gameSettings = new GameSettings();
            var gameInitializer = new MockGameInitializer().CreateStubbedToReturn(game);
            var aiContextFactory = new MockIntelligenceContextFactory();
            var application = BuildApplication(gameInitializer, aiContextFactory);

            application.Run(gameSettings);

            gameInitializer.VerifyCreateCalled(gameSettings);
            aiContextFactory.VerifyCreatedCalled(game);
        }

        private static Application BuildApplication(IGameInitializer gameInitializer = null, IIntelligenceContextFactory aiContextFactory = null) {
            gameInitializer = gameInitializer ?? new MockGameInitializer();
            aiContextFactory = aiContextFactory ?? new MockIntelligenceContextFactory();
            return new Application(gameInitializer, aiContextFactory);
        }
    }
}
