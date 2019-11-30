namespace MarsColonyEngine.Logger {
    public static class KLogger {
        public static ILogger Log { get; } = new ConsoleLogger();
        public static bool LogQuietMessages { get; set; }
        public static bool LogWhisperMessages { get; set; }
    }
}
