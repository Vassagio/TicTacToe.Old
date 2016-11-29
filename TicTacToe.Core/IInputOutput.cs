using System;

namespace TicTacToe.Core {
    public interface IInputOutput {
        void Write(string text);
        string Read(string question);
    }
}
