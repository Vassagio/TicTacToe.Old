using TicTacToe.Core.AI;
using TicTacToe.Core.States;

namespace TicTacToe.Core {
    public class Application : IApplication {
        private readonly IGameInitializer _gameInitializer;
        private readonly IIntelligenceContextFactory _aiContextFactory;

        public Application(IGameInitializer gameInitializer, IIntelligenceContextFactory aiContextFactory) {
            _gameInitializer = gameInitializer;
            _aiContextFactory = aiContextFactory;
        }

        public void Run(GameSettings settings) {
            IGameState gameState = new GameNotStartedState(settings, _gameInitializer, _aiContextFactory);
            do {
                gameState = gameState.Handle();
            } while (!(gameState is GameEndedState));
        }
    }
}