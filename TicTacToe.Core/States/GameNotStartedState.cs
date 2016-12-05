using TicTacToe.Core.AI;

namespace TicTacToe.Core.States {
    public class GameNotStartedState : IGameState {
        private readonly GameSettings _settings;
        private readonly IGameInitializer _gameInitializer;
        private readonly IIntelligenceContextFactory _contextFactory;
        private readonly IInputOutput _io;

        public GameNotStartedState(GameSettings settings, IGameInitializer gameInitializer, IIntelligenceContextFactory contextFactory, IInputOutput io) {
            _settings = settings;
            _gameInitializer = gameInitializer;
            _contextFactory = contextFactory;
            _io = io;
        }

        public IGameState Handle() {
            return new GameStartedState(_settings, _gameInitializer, _contextFactory, _io);
        }
    }
}