namespace TicTacToe.Core.States {
    public interface IGameState {
        IGameState Handle();
    }
}