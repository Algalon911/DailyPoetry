using DailyPoetry.Models;
using DailyPoetry.UnitTest.NotThreadSafeResourceTest;
using Moq;
using System.IO;
using System.Linq.Expressions;

namespace DailyPoetry.UnitTest.Sevices;
[Collection(nameof(NotThreadSafeResourceCollection))]
public class PoetryStorageTest : IDisposable
{
    public PoetryStorageTest() => File.Delete(PoetryStorage.PoetryDbPath);

    public void Dispose() => File.Delete(PoetryStorage.PoetryDbPath);

    [Fact]
    public void IsInitailized_Default()
    {
        var preferenceStorageMock = new Mock<IPreferenceStorage>();
        preferenceStorageMock
            .Setup(p => p.Get(PoetryStorageConstant.VersionKey, 0))
            .Returns(PoetryStorageConstant.Version);
        var mockPreferenceStorage = preferenceStorageMock.Object;

        var poetryStorage = new PoetryStorage(mockPreferenceStorage);
        Assert.True(poetryStorage.IsInitialized);
    }

    [Fact]
    public async Task TestInitializeAsync_Default()
    {
        var preferenceStorageMock = new Mock<IPreferenceStorage>();
        var mockPreferenceStorage = preferenceStorageMock.Object;

        var poetryStorage = new PoetryStorage(mockPreferenceStorage);

        Assert.False(File.Exists(PoetryStorage.PoetryDbPath));
        await poetryStorage.InitailizeAsync();
        Assert.True(File.Exists(PoetryStorage.PoetryDbPath));

        //验证调用了Set函数一次
        preferenceStorageMock.Verify(p => p.Set(PoetryStorageConstant.VersionKey,
                                                PoetryStorageConstant.Version), Times.Once);

    }

    [Fact]
    public async Task GetPoemAsync_Default()
    {
        PoetryStorage poetryStorage = await GetInitializedPoetryStorage();
        var poem = await poetryStorage.GetPoemAsync(10001);
        Assert.Equal("临江仙 · 夜归临皋", poem.Name);
        await poetryStorage.CloseAsync();
    }

    [Fact]
    public async Task GetPoetryAsnc_Default()
    {
        var poetryStorage = await GetInitializedPoetryStorage();
        var poetry = await poetryStorage.GetPoetryAsync(
            Expression.Lambda<Func<Poem, bool>>(Expression.Constant(true),
                Expression.Parameter(typeof(Poem), "p")), 0, int.MaxValue);
        Assert.Equal(30, poetry.Count());
        await poetryStorage.CloseAsync();
    }

    /// <summary>
    /// 获取已经初始化的PoetryStorage
    /// </summary>
    /// <returns></returns>
    public static async Task<PoetryStorage> GetInitializedPoetryStorage()
    {
        var preferenceStorageMock = new Mock<IPreferenceStorage>();
        var mockPreferenceStorage = preferenceStorageMock.Object;
        var poetryStorage = new PoetryStorage(mockPreferenceStorage);
        await poetryStorage.InitailizeAsync();
        return poetryStorage;
    }


}
