using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Core {
    public class PatternFactory : IPatternFactory {
        private string _defaultPattern;
        private int _boardSize;
        private int _spaces;

        public IEnumerable<string> Create(int boardSize) {
            if (boardSize == 1)
                yield break;

            _boardSize = boardSize;
            _spaces = boardSize*boardSize;
            _defaultPattern = new string('.', _spaces);

            var patterns = CreatePatterns();
            foreach (var pattern in patterns)
                yield return pattern;
        }

        private static void ReplaceCharacter(StringBuilder pattern, int position) {
            pattern.Remove(position, 1);
            pattern.Insert(position, "1");
        }

        private IEnumerable<string> CreatePatterns() {
            var horizontal = CreateHorizontalPatterns();
            var vertical = CreateVerticalPatterns();
            var downwardDiagonal = CreateDownwardDiagonalPattern();
            var upwardDiagonal = CreateUpwardDiagonalPattern();
            return horizontal.Concat(vertical).Concat(downwardDiagonal).Concat(upwardDiagonal);
        }

        private IEnumerable<string> CreateHorizontalPatterns() {
            for (var x = 0; x < _spaces; x += _boardSize) {
                var pattern = new StringBuilder(_defaultPattern);
                for (var y = 0; y < _boardSize; y++)
                    ReplaceCharacter(pattern, x + y);
                yield return pattern.ToString();
            }
        }

        private IEnumerable<string> CreateVerticalPatterns() {
            for (var y = 0; y < _boardSize; y++) {
                var pattern = new StringBuilder(_defaultPattern);
                for (var x = 0; x < _spaces; x += _boardSize)
                    ReplaceCharacter(pattern, x + y);
                yield return pattern.ToString();
            }
        }

        private IEnumerable<string> CreateDownwardDiagonalPattern() {
            var pattern = new StringBuilder(_defaultPattern);
            for (var i = 0; i < _spaces; i += _boardSize + 1)
                ReplaceCharacter(pattern, i);
            yield return pattern.ToString();
        }

        private IEnumerable<string> CreateUpwardDiagonalPattern() {
            var pattern = new StringBuilder(_defaultPattern);
            for (var i = _boardSize - 1; i < _spaces - _boardSize + 1; i += _boardSize - 1)
                ReplaceCharacter(pattern, i);
            yield return pattern.ToString();
        }
    }
}