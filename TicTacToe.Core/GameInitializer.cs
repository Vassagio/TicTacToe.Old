using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core {
    public class GameInitializer : IGameInitializer {
        private readonly IPlayersFactory _playersFactory;
        private readonly IPatternFactory _patternFactory;
        private readonly IIntelligenceFactory _aiFactory;

        public GameInitializer(IPlayersFactory playersFactory, IPatternFactory patternFactory, IIntelligenceFactory aiFactory) {
            _playersFactory = playersFactory;
            _patternFactory = patternFactory;
            _aiFactory = aiFactory;
        }

        public IGame Create(GameSettings gameSettings) {
            var board = new Board(gameSettings.BoardSize, _patternFactory);
            var players = _playersFactory.Create(gameSettings);
            var ai = _aiFactory.Create(gameSettings, players);
            var startingPlayer = GetStartingPlayer(gameSettings.PlayerStartType, players);

            return new Game(board, players, ai, startingPlayer);
        }

        private IPlayer GetStartingPlayer(PlayerStartType playerStartType, IEnumerable<IPlayer> players) {
            if (!players.Any())
                throw new ArgumentException("invalid players");

            switch (playerStartType) {
                case PlayerStartType.FirstPlayerFirst:
                    return players.First();
                case PlayerStartType.LastPlayerFirst:
                    return players.Last();
                default:
                    throw new ArgumentException("invalid player start type");
            }
        }
    }
}