using System;

namespace MarsColonyEngine.Helpers {
    public static class KRandom {
        private static Random _rand = null;
        private static Random Rand {
            get {
                return _rand != null ? _rand : (_rand = new Random());
            }
        }
        public static void Seed (int seed) {
            _rand = new Random(seed);
        }
        public static int Int (int min = int.MinValue, int max = int.MaxValue) {
            return Rand.Next(min, max);
        }
        public static float Float (float min = float.MinValue, float max = float.MaxValue) {
            return (float)Rand.NextDouble() * (max - min) + min;
        }

        public static float Float01 () {
            return (float)Rand.NextDouble();
        }

        public static bool Bool (int chancesPercent = 50) {
            return Float01() <= chancesPercent / 100f ? true : false;
        }
    }
}
