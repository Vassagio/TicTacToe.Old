using System.Collections.Generic;

namespace TicTacToe.Core {
    public interface IPatternFactory {
        IEnumerable<string> Create(int boardSize);
    }
}