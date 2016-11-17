namespace TicTacToe.Core.Players {
    public interface IPlayer {
        string Name { get; }
        char Symbol { get; }
        void ChoosePosition(IBoard board, int position);
        bool HasWon(IBoard board);
    }
}