using System;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.States {
    public class GameStartedState: IGameState {
        private readonly GameSettings _settings;
        private readonly IGameInitializer _gameInitializer;
        private readonly IIntelligenceContextFactory _contextFactory;

        public GameStartedState(GameSettings settings, IGameInitializer gameInitializer, IIntelligenceContextFactory contextFactory) {
            _settings = settings;
            _gameInitializer = gameInitializer;
            _contextFactory = contextFactory;
        }

        public IGameState Handle() {
            var game = _gameInitializer.Create(_settings);
            var context = _contextFactory.Create(game);
            switch (_settings.PlayerStartType) {
                case PlayerStartType.FirstPlayerFirst:
                    return new Player1TurnState(game, _contextFactory, context);
                case PlayerStartType.LastPlayerFirst:
                    return new Player2TurnState(game, _contextFactory, context);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
