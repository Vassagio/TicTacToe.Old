namespace TicTacToe.Core.AI.Human {
    public class HumanIntelligence : IHumanIntelligence {
        private readonly IInputOutput _io;

        public HumanIntelligence(IInputOutput io) {
            _io = io;
        }
        public BoardCoordinate DetermineBest(IIntelligenceContext context) {
            var answer = _io.Read("Choose a position: ");
            return context.Board.ToCoordinate(int.Parse(answer));
        }
    }
}