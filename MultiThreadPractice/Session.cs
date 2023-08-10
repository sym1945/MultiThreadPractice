namespace MultiThreadPractice
{
    public class Session
    {
        private static int _LastSessionId = 0;
        private readonly int _Id;
        private readonly PacketProcessor _PacketProcessor;

        public Session(PacketProcessor packetProcessor)
        {
            _PacketProcessor = packetProcessor;
            _Id = Interlocked.Increment(ref _LastSessionId);
        }

        public Task StartRecvAsync(CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var packet = DummyPacketGenerator.Generate();

                    _PacketProcessor.ProcessPacket(_Id, packet.Item1, packet.Item2);

                    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken).ConfigureAwait(false);
                }

            }, cancellationToken);
        }
    }

    public static class DummyPacketGenerator
    {
        public static Tuple<string, string> Generate()
        {
            string msgId = $"MSG{Random.Shared.Next(1, 6):D2}";
            string data = Path.GetRandomFileName();

            return new Tuple<string, string>(msgId, data);
        }
    }
    
}
