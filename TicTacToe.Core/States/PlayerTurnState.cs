using TicTacToe.Core.AI;

namespace TicTacToe.Core.States {
    public abstract class PlayerTurnState: IGameState {
        protected readonly IGame Game;
        protected readonly IIntelligenceContextFactory ContextFactory;
        protected readonly IIntelligenceContext Context;

        protected PlayerTurnState(IGame game, IIntelligenceContextFactory contextFactory, IIntelligenceContext context) {
            Game = game;
            ContextFactory = contextFactory;
            Context = context;
        }

        public IGameState Handle() {
            Game.MakeMove(Context);
            Game.SwitchPlayer();            

            return Game.IsOver() ? new GameEndedState() : GetNextPlayerState();
        }

        protected abstract IGameState GetNextPlayerState();
    }
}
