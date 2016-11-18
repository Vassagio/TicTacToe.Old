using System;
namespace TicTacToe.Core.AI {
    public interface IIntelligenceContextFactory {
        IIntelligenceContext Create(IGame game);
    }
}
