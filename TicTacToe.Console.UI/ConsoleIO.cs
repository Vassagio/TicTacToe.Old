using TicTacToe.Core;

namespace TicTacToe.Console.UI {
    public class ConsoleIO : IInputOutput {
        public void Write(string text) {
            System.Console.WriteLine(text);
            System.Console.ReadKey();
        }

        public string Read(string question) {
            System.Console.WriteLine(question);
            return System.Console.ReadLine();
        }
    }
}