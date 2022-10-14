using System.Threading;

namespace DailyPoetry.UnitTest.NotThreadSafeResourceTest;

public static class NotThreadSafeResource
{
    private static int UsedByThreadId { get; set; }
    private static object UsedByThreadIdLock { get; } = new object();

    public static void Use()
    {
        lock (UsedByThreadIdLock)
        {
            if (UsedByThreadId != 0) throw new InvalidOperationException("Resource already in use");
            UsedByThreadId = Thread.CurrentThread.ManagedThreadId;
        }
    }

    public static void Release()
    {
        lock (UsedByThreadIdLock)
        {
            UsedByThreadId = 0;
        }
    }
}
