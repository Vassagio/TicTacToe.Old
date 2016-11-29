using System.Collections.Generic;
using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test {
    public class ApplicationTest {
        //[Fact]
        //public void Initializes_A_New_Application() {
        //    var application = BuildApplication();

        //    application.Should().NotBeNull();
        //    application.Should().BeOfType<Application>();
        //    application.Should().BeAssignableTo<IApplication>();
        //}

        //[Fact]
        //public void Creates_A_New_Game() {
        //    var player1 = new MockPlayer { Symbol = 'X' };
        //    var player2 = new MockPlayer { Symbol = 'O' };
        //    var players = new List<IPlayer> {
        //        player1,
        //        player2
        //    };
        //    var boardCoordinate1 = new BoardCoordinate(1, 1);
        //    var boardCoordinate2 = new BoardCoordinate(2, 2);
        //    var board = new MockBoard()
        //        .GetWinnerStubbedToReturn(new Nobody(), new Nobody(), new Nobody(), new Nobody(), new Nobody(), player1 )
        //        .GetOpenSpacesStubbedToReturn(new List<BoardCoordinate> {boardCoordinate1, boardCoordinate2}, new List<BoardCoordinate> { boardCoordinate2 });
        //    var ai = new MockIntelligence().DetermineBestStubbedToReturn(boardCoordinate1, boardCoordinate2);
        //    var game = new MockGame {
        //        Board = board,
        //        Players = players,
        //        CurrentPlayer = player1
        //    }.GetIntelligenceStubbedToReturn(ai);
        //    var gameSettings = new GameSettings();
        //    var gameInitializer = new MockGameInitializer().CreateStubbedToReturn(game);
        //    var context = new MockIntelligenceContext();
        //    var aiContextFactory = new MockIntelligenceContextFactory().CreateStubbedToReturn(context);
        //    var application = BuildApplication(gameInitializer, aiContextFactory);

        //    application.Run(gameSettings);

        //    gameInitializer.VerifyCreateCalled(gameSettings);
        //    aiContextFactory.VerifyCreatedCalled(game);
        //    ai.VerifyDetermineBestCalled(context, 9);
        //    board.VerifyGetWinnerCalled(players, 9);
        //    board.VerifyGetOpenSpacesCalled(9);
        //}

        //private static Application BuildApplication(IGameInitializer gameInitializer = null, IIntelligenceContextFactory aiContextFactory = null) {
        //    gameInitializer = gameInitializer ?? new MockGameInitializer();
        //    aiContextFactory = aiContextFactory ?? new MockIntelligenceContextFactory();
        //    return new Application(gameInitializer, aiContextFactory);
        //}
    }
}
