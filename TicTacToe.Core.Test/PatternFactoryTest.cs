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
                    "1100",
                    "0011",
                    "1010",
                    "0101",
                    "1001",
                    "0110"
                }
            };

            yield return new object[] {
                3,
                new List<string> {
                    "111000000",
                    "000111000",
                    "000000111",
                    "100100100",
                    "010010010",
                    "001001001",
                    "100010001",
                    "001010100",
                }
            };

            yield return new object[] {
                4,
                new List<string> {
                    "1111000000000000",
                    "0000111100000000",
                    "0000000011110000",
                    "0000000000001111",
                    "1000100010001000",
                    "0100010001000100",
                    "0010001000100010",
                    "0001000100010001",
                    "1000010000100001",
                    "0001001001001000",
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
