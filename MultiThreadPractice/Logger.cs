using System.Text;

namespace MultiThreadPractice
{
    public class Logger : IDisposable
    {
        private const string _FilePath = @".\Logs\log.log";
        //private readonly object _Locker = new object();
        private readonly FileStream? _FileStream = null;
        private readonly StreamWriter? _StreamWriter = null;


        public Logger()
        {
            try
            {
                var dir = Path.GetDirectoryName(_FilePath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir!);

                _FileStream = File.Open(_FilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
                _StreamWriter = new StreamWriter(_FileStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void WriteLog(string message)
        {
            var logText = new StringBuilder()
                .Append($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] ")
                .Append($"ThreadId:{Thread.CurrentThread.ManagedThreadId.ToString()}, ")
                .Append(message)
                .ToString();

            try
            {
                //lock (_Locker)
                //{
                    _StreamWriter?.WriteLine(logText);
                    _StreamWriter?.Flush();

                    //using (var fs = File.Open(_FilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                    //using (var sw = new StreamWriter(fs))
                    //{
                    //    sw.WriteLine(logText);
                    //}
                //}

                Console.WriteLine(logText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Dispose()
        {
            _StreamWriter?.Dispose();
            _FileStream?.Dispose();
        }
    }
}
