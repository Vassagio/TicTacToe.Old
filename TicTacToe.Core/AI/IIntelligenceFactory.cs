namespace TicTacToe.Core.AI {
    public interface IIntelligenceFactory {
        IIntelligence Create(GameSettings gameSettings);
    }
}
