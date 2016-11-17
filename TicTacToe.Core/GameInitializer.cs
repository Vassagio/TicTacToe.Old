using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core {
    public class GameInitializer {
        private readonly IPlayersFactory _playersFactory;
        private readonly IPatternFactory _patternFactory;
        private readonly IIntelligenceFactory _aiFactory;

        public GameInitializer(IPlayersFactory playersFactory, IPatternFactory patternFactory, IIntelligenceFactory aiFactory) {
            _playersFactory = playersFactory;
            _patternFactory = patternFactory;
            _aiFactory = aiFactory;
        }

        public Game Create(GameSettings gameSettings) {
            var board = new Board(gameSettings.BoardSize, _patternFactory);
            var players = _playersFactory.Create(gameSettings);
            var ai = _aiFactory.Create(gameSettings);

            return new Game(board, players, ai);
        }
    }
}