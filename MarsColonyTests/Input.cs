using System;

namespace MarsColonyTests {
    static class Input {
        public static ConsoleKeyInfo? PressedKey {
            get {
                if (Console.KeyAvailable) {
                    PrevKey = Console.ReadKey(true);
                    return PrevKey;
                }
                return null;
            }
        }
        private static ConsoleKeyInfo? PrevKey { get; set; }
        public static ConsoleKeyInfo? PressedKeyDown {
            get {
                if (Console.KeyAvailable) {
                    var key = Console.ReadKey(true);
                    if (PrevKey == null || PrevKey != key) {
                        PrevKey = key;
                        return key;
                    }
                }
                return null;
            }
        }
    }
}
