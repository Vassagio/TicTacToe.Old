using TicTacToe.Core.AI;

namespace TicTacToe.Core.States {
    public abstract class PlayerTurnState: IGameState {
        protected readonly IGame Game;
        protected readonly IIntelligenceContextFactory ContextFactory;
        protected readonly IIntelligenceContext Context;
        protected readonly IInputOutput IO;

        protected PlayerTurnState(IGame game, IIntelligenceContextFactory contextFactory, IIntelligenceContext context, IInputOutput io) {
            Game = game;
            ContextFactory = contextFactory;
            Context = context;
            IO = io;
        }

        public IGameState Handle() {
            Game.MakeMove(Context);
            Game.SwitchPlayer();

            IO.Write(Game.Board.ToString());   

            return Game.IsOver() ? new GameEndedState() : GetNextPlayerState();
        }

        protected abstract IGameState GetNextPlayerState();
    }
}
