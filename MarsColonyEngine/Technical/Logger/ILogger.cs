namespace MarsColonyEngine.Logger {
    public interface ILogger {

        bool LogQuietMessages { get; }
        bool LogWhisperMessages { get; }
        void Message (string message);
        void Warning (string message);
        void Quiet (string message);
        void Whisper (string message);
        void Error (string message);
    }
}
