namespace MultiThreadPractice
{
    public static class ThreadSafeLogger
    {
        private static readonly Logger _Logger = new Logger();

        private static readonly ThreadLocal<Logger> _tlLogger = new ThreadLocal<Logger>(() => _Logger);

        public static void WriteLog(string message)
        {
            lock (_Logger)
            {
                _tlLogger.Value?.WriteLog(message);
            }
        }

    }
}
