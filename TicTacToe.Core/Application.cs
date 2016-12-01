using TicTacToe.Core.AI;
using TicTacToe.Core.States;

namespace TicTacToe.Core {
    public class Application : IApplication {
        private readonly IGameInitializer _gameInitializer;
        private readonly IIntelligenceContextFactory _aiContextFactory;
        private readonly IInputOutput _io;

        public Application(IGameInitializer gameInitializer, IIntelligenceContextFactory aiContextFactory, IInputOutput io) {
            _gameInitializer = gameInitializer;
            _aiContextFactory = aiContextFactory;
            _io = io;
        }

        public void Run(GameSettings settings) {
            IGameState gameState = new GameNotStartedState(settings, _gameInitializer, _aiContextFactory, _io);
            do {
                gameState = gameState.Handle();
            } while (!(gameState is GameEndedState));
        }
    }
}