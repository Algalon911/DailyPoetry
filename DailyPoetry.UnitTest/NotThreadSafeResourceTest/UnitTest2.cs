using System.Threading;
namespace DailyPoetry.UnitTest.NotThreadSafeResourceTest;

[Collection(nameof(NotThreadSafeResourceCollection))]
public class UnitTest2
{
    [Fact]
    public void Test2()
    {
        NotThreadSafeResource.Use();
        Thread.Sleep(1000);
        NotThreadSafeResource.Release();
    }
}
