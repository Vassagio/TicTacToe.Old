using TicTacToe.Core.AI;

namespace TicTacToe.Core.States {
    public class Player2TurnState : PlayerTurnState {
        public Player2TurnState(IGame game, IIntelligenceContextFactory contextFactory, IIntelligenceContext context, IInputOutput io) : base(game, contextFactory, context, io) {}

        protected override IGameState GetNextPlayerState() {
            var newContext = ContextFactory.Create(Game);
            return new Player1TurnState(Game, ContextFactory, newContext, IO);
        }
    }
}