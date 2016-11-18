namespace TicTacToe.Core {
    public interface IGameInitializer {
        IGame Create(GameSettings gameSettings);
    }
}