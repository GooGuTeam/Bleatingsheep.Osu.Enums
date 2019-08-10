using Xunit;

namespace Bleatingsheep.Osu.Enums.Tests
{
    public class ModeTests
    {
        [Fact]
        public void ParseCatch()
        {
            string mode = "osu!catch";
            var result = ModeExtensions.Parse(mode);
            Assert.Equal(Mode.Catch, result);

            mode = "cAtcH";
            result = ModeExtensions.Parse(mode);
            Assert.Equal(Mode.Catch, result);

            mode = "½ÓË®¹û";
            result = ModeExtensions.Parse(mode);
            Assert.Equal(Mode.Catch, result);
        }

        [Theory]
        [InlineData("o", Mode.Standard)]
        [InlineData("t", Mode.Taiko)]
        [InlineData("c", Mode.Catch)]
        [InlineData("m", Mode.Mania)]
        [InlineData("o!t", Mode.Taiko)]
        [InlineData("o!c", Mode.Catch)]
        [InlineData("o!m", Mode.Mania)]
        public void Parse(string modeString, Mode expected)
        {
            var mode = ModeExtensions.Parse(modeString);

            Assert.Equal(expected, mode);
        }

        [Fact]
        public void ShortString()
        {
            Assert.Equal("catch", Mode.Catch.GetShortModeString());
            Assert.Equal("osu!", Mode.Standard.GetShortModeString());
        }
    }
}
