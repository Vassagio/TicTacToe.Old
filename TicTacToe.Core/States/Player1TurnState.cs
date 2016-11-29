using TicTacToe.Core.AI;

namespace TicTacToe.Core.States {
    public class Player1TurnState : PlayerTurnState {
        public Player1TurnState(IGame game, IIntelligenceContextFactory contextFactory, IIntelligenceContext context) : base(game, contextFactory, context) {}

        protected override IGameState GetNextPlayerState() {
            var newContext = ContextFactory.Create(Game);
            return new Player2TurnState(Game, ContextFactory, newContext);
        }
    }
}