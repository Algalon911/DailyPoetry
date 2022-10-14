using System.Threading;
namespace DailyPoetry.UnitTest.NotThreadSafeResourceTest;

//[Collection(nameof(NotThreadSafeResourceCollection))]
public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        NotThreadSafeResource.Use();
        Thread.Sleep(3000);
        NotThreadSafeResource.Release();
    }

    [Fact]
    public void Test3()
    {
        NotThreadSafeResource.Use();
        Thread.Sleep(1000);
        NotThreadSafeResource.Release();
    }
}
