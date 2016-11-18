
using TicTacToe.Core;
using Xunit;

namespace TicTacToe.Console.UI.Tests {
    public class ProgramTest {
        [Fact]
        public void Runs_The_Application() {
            var gameSettings = new GameSettings();
            var application = new MockApplication();

            Program.Run(application, gameSettings);

            application.VerifyRunCalled(gameSettings);
        }
    }
}
