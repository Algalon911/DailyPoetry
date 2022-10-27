using DailyPoetry.Models;
using DailyPoetry.Services;
using DailyPoetry.UnitTest.Sevices;
using DailyPoetry.ViewModels;
using System.IO;
using System.Linq.Expressions;

namespace DailyPoetry.UnitTest.ViewModels;

public class ResultPageViewModelTest : IDisposable
{
    public ResultPageViewModelTest()
    {
        File.Delete(PoetryStorage.PoetryDbPath);
    }

    public void Dispose() => File.Delete(PoetryStorage.PoetryDbPath);

    [Fact]
    public async Task Poetry_Default()
    {
        var where = Expression.Lambda<Func<Poem, bool>>(
            Expression.Constant(true),
            Expression.Parameter(typeof(Poem), "p"));

        var poetryStorage = await PoetryStorageTest.GetInitializedPoetryStorage();
        var resultPageModel = new ResultPageViewModel(poetryStorage)
        {
            Where = where
        };

        var statusList = new List<string>();
        resultPageModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(ResultPageViewModel.Status))
            {
                statusList.Add(resultPageModel.Status);
            }
        };//测试Status

        //测试调用command的完整方法
        //resultPageModel.NavigatedToCommand.Execute(null);
        //await resultPageModel.NavigatedToCommand.ExecutionTask!;

        //简单测试方法
        await resultPageModel.NavigatedTo();

        Assert.Equal(ResultPageViewModel.PageSize, resultPageModel.Poetry.Count);
        Assert.Equal(2, statusList.Count);
        Assert.Equal(ResultPageViewModel.Loading, statusList[0]);
        Assert.Equal(string.Empty, statusList[1]);

        await poetryStorage.CloseAsync();
    }
}
