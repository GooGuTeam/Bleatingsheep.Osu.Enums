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

        [Fact]
        public void ShortString()
        {
            Assert.Equal("catch", Mode.Catch.GetShortModeString());
            Assert.Equal("osu!", Mode.Standard.GetShortModeString());
        }
    }
}
