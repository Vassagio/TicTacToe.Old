using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace TicTacToe.Core.Test {
    public class PatternFactoryTest {
        public static IEnumerable<object[]> GetPatterns() {
            yield return new object[] {
                1,
                new List<string>()
            };

            yield return new object[] {
                2,
                new List<string> {
                    "11..",
                    "..11",
                    "1.1.",
                    ".1.1",
                    "1..1",
                    ".11."
                }
            };

            yield return new object[] {
                3,
                new List<string> {
                    "111......",
                    "...111...",
                    "......111",
                    "1..1..1..",
                    ".1..1..1.",
                    "..1..1..1",
                    "1...1...1",
                    "..1.1.1..",
                }
            };

            yield return new object[] {
                4,
                new List<string> {
                    "1111............",
                    "....1111........",
                    "........1111....",
                    "............1111",
                    "1...1...1...1...",
                    ".1...1...1...1..",
                    "..1...1...1...1.",
                    "...1...1...1...1",
                    "1....1....1....1",
                    "...1..1..1..1...",
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetPatterns))]
        public void Creates_List_Of_Winning_Patterns_Based_On_Board_Size(int size, IEnumerable<string> expected) {
            var factory = new PatternFactory();

            var patterns = factory.Create(size);

            patterns.Should().BeEquivalentTo(expected);
        }
    }
}
