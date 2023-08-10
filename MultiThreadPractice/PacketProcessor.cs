using System.Reflection;

namespace MultiThreadPractice
{
    public class PacketProcessor
    {
        private readonly Dictionary<string, Action<int, string>> _ProcessorMap;

        public PacketProcessor()
        {
            _ProcessorMap = new Dictionary<string, Action<int, string>>
            {
                { "MSG01", ProcessMsg01 },
                { "MSG02", ProcessMsg02 },
                { "MSG03", ProcessMsg03 },
                { "MSG04", ProcessMsg04 },
                { "MSG05", ProcessMsg05 },
            };
        }

        public void ProcessPacket(int sessionId, string msgId, string data)
        {
            if (_ProcessorMap.TryGetValue(msgId, out var processor))
                processor.Invoke(sessionId, data);
        }

        private void ProcessMsg01(int sessionId, string data)
        {
            ThreadSafeLogger.WriteLog($"SessionId:{sessionId}, {MethodBase.GetCurrentMethod()?.Name} -> {data}");
        }

        private void ProcessMsg02(int sessionId, string data)
        {
            ThreadSafeLogger.WriteLog($"SessionId:{sessionId}, {MethodBase.GetCurrentMethod()?.Name} -> {data}");
        }

        private void ProcessMsg03(int sessionId, string data)
        {
            ThreadSafeLogger.WriteLog($"SessionId:{sessionId}, {MethodBase.GetCurrentMethod()?.Name} -> {data}");
        }

        private void ProcessMsg04(int sessionId, string data)
        {
            ThreadSafeLogger.WriteLog($"SessionId:{sessionId}, {MethodBase.GetCurrentMethod()?.Name} -> {data}");
        }

        private void ProcessMsg05(int sessionId, string data)
        {
            ThreadSafeLogger.WriteLog($"SessionId:{sessionId}, {MethodBase.GetCurrentMethod()?.Name} -> {data}");
        }
    }
}
