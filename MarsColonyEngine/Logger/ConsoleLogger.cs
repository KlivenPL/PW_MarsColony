using System;

namespace MarsColonyEngine.Logger {
    internal class ConsoleLogger : ILogger {
        public bool LogQuietMessages => KLogger.LogQuietMessages;
        public bool LogWhisperMessages => KLogger.LogWhisperMessages;

        public void Error (string message) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.Write("Error: ");
            Console.ResetColor();
            Console.Error.Write(message);
            Console.Error.WriteLine();
        }

        public void Message (string message) {
            Console.WriteLine(message);
        }

        public void Quiet (string message) {
            if (LogQuietMessages) {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Quiet: ");
                Console.ResetColor();
                Console.Write(message);
                Console.WriteLine();
            }
        }
        public void Whisper (string message) {
            if (LogQuietMessages) {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Whisper: ");
                Console.Write(message);
                Console.WriteLine();
                Console.ResetColor();
            }
        }
    }
}
