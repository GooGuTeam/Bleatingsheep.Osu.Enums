using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bleatingsheep.Osu
{
    /// <summary>
    /// Extension methods and utilities for <see cref="Mode"/>.
    /// </summary>
    public static class ModeExtensions
    {
        private const string ModeInfo = @"0,std,osu,osu!,standard,osu!standard,戳泡泡,o
1,taiko,osu!taiko,太鼓,t,o!t
2,ctb,catch,osu!catch,接水果,c,o!c
3,mania,osu!mania,m,o!m";

        private static readonly IReadOnlyDictionary<string, Mode> pairs;

        static ModeExtensions()
        {
            void ConcatLine(string line, Mode mode, ref IEnumerable<KeyValuePair<string, Mode>> toAdd)
            {
                var alias = line.Split(',').Select(s => new KeyValuePair<string, Mode>(s.ToUpperInvariant(), mode));
                toAdd = toAdd.Concat(alias);
            }

            IEnumerable<KeyValuePair<string, Mode>> maps = new Dictionary<string, Mode>();

            //var aliases = ModeInfo.ToUpperInvariant().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var aliases = new List<string>();
            var stringReader = new StringReader(ModeInfo);
            string singleLine;
            while ((singleLine = stringReader.ReadLine()) != null)
            {
                aliases.Add(singleLine);
            }
            for (int i = 0; i < aliases.Count; i++)
            {
                ConcatLine(aliases[i], (Mode)i, ref maps);
            }

            //pairs = new Dictionary<string, Mode>(maps);
            pairs = maps.ToDictionary(p => p.Key, p => p.Value);
        }

        /// <summary>
        /// Converts the <see cref="string"/> to <see cref="Mode"/>.
        /// </summary>
        /// <param name="s">A string containing a mode to convert.</param>
        /// <exception cref="ArgumentNullException"><c>s</c> is <c>null</c>.</exception>
        /// <exception cref="FormatException"><c>s</c> is not a valid mode string.</exception>
        /// <returns>A <see cref="Mode"/> enum that the string represents.</returns>
        public static Mode Parse(string s)
        {
            s = s.ToUpperInvariant();
            return pairs.TryGetValue(s, out Mode result) ? result : throw new FormatException("Invalid mode string.");
        }

        /// <summary>
        /// Get a short string that represents the giving <see cref="Mode"/>.
        /// </summary>
        /// <returns>"osu!", "taiko", "catch", "mania" or <c>null</c>.</returns>
        public static string GetShortModeString(this Mode mode)
        {
            switch (mode)
            {
                case Mode.Standard:
                    return "osu!";
                case Mode.Taiko:
                    return "taiko";
                case Mode.Catch:
                    return "catch";
                case Mode.Mania:
                    return "mania";
                default:
                    return null;
            }
        }
    }
}
