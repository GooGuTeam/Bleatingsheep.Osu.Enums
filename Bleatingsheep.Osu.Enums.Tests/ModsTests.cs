using System;
using Xunit;

namespace Bleatingsheep.Osu.Enums.Tests
{
    public class ModsTests
    {
        [Fact]
        public void HideLower()
        {
            Assert.Equal("NC", (Mods.Nightcore | Mods.DoubleTime).Display());
            Assert.Equal("PF", (Mods.SuddenDeath | Mods.Perfect).Display());
        }

        [Fact]
        public void Display()
        {
            var ezhd = (Mods.Hidden | Mods.Easy).Display();
            Assert.True(ezhd.Equals("HDEZ") || ezhd.Equals("EZHD"));

            var hdv2 = (Mods.Hidden | Mods.ScoreV2).Display();
            Assert.Equal("HD, " + nameof(Mods.ScoreV2), hdv2);
        }

        [Fact]
        public void Parse()
        {
            Assert.Equal(
                Mods.Nightcore | Mods.DoubleTime,
                ModsExtensions.Parse("nC")
            );

            Assert.Equal(
                Mods.Perfect | Mods.DoubleTime | Mods.SuddenDeath,
                ModsExtensions.Parse("dtpf")
            );

            Assert.Equal(
                Mods.Hidden | Mods.DoubleTime,
                ModsExtensions.Parse("hddt")
            );

            Assert.Equal(
                Mods.Hidden | Mods.DoubleTime,
                ModsExtensions.Parse("dTHd")
            );

            Assert.Equal(
                Mods.NoFail,
                ModsExtensions.Parse("nf")
            );

            Assert.Equal(
                Mods.None,
                ModsExtensions.Parse("noNe")
            );

            Assert.Equal(
                Mods.None,
                ModsExtensions.Parse(string.Empty)
            );

            var ex = Assert.Throws<FormatException>(() => ModsExtensions.Parse("hdd"));
            Assert.Equal("Mods format is invalid.", ex.Message);
            Assert.StartsWith($"   at {typeof(ModsExtensions).FullName}", ex.StackTrace, StringComparison.Ordinal);

            ex = Assert.Throws<FormatException>(() => ModsExtensions.Parse("hddf"));
            Assert.Equal("Mods format is invalid.", ex.Message);
            Assert.StartsWith($"   at {typeof(ModsExtensions).FullName}", ex.StackTrace, StringComparison.Ordinal);
        }
    }
}
