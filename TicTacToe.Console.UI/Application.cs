using System.Linq;
using TicTacToe.Core;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Console.UI {
    public class Application : IApplication {
        private readonly IGameInitializer _gameInitializer;
        private readonly IIntelligenceContextFactory _aiContextFactory;

        public Application(IGameInitializer gameInitializer, IIntelligenceContextFactory aiContextFactory) {
            _gameInitializer = gameInitializer;
            _aiContextFactory = aiContextFactory;
        }

        public void Run(GameSettings settings) {
            var game = _gameInitializer.Create(settings);
            var context = _aiContextFactory.Create(game);

            //do {
            //    var move = game.AI.DetermineBest(context);
            //} while (game.Board.GetWinner(game.Players) is Nobody && game.Board.GetOpenSpaces().Any());
        }
    }
}