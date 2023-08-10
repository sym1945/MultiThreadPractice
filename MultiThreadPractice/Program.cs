using MultiThreadPractice;

Console.WriteLine("Program start");


var canceller = new CancellationTokenSource();
var packetProcessor = new PacketProcessor();


// Create sessions
var sessions = new List<Session>();

for (int i = 0; i < 10; i++)
    sessions.Add(new Session(packetProcessor));

// Start recv
var recvTasks = new List<Task>();
recvTasks.AddRange(sessions.Select(session => session.StartRecvAsync(canceller.Token)));


while (!canceller.IsCancellationRequested)
{
    var inputKey = Console.ReadKey();

    switch (inputKey.KeyChar)
    {
        case 'c':
            {
                canceller.Cancel();
                break;
            }
    }
}


try
{
    await Task.WhenAll(recvTasks);
}
catch (OperationCanceledException)
{
}

Console.WriteLine("Program end");
Console.ReadLine();