using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq.Expressions;
using TheSalLab.MauiInfiniteScrolling;

namespace DailyPoetry.ViewModels;

public partial class ResultPageViewModel : ObservableObject
{
    public const int PageSize = 20;
    public const string Loading = "正在载入";
    public const string NoResult = "没有满足条件结果";
    public const string NoMoreReult = "没有更多结果";

    bool canLoadMore;

    //[ObservableProperty]
    //Expression<Func<Poem, bool>> where;

    private Expression<Func<Poem, bool>> where;
    public Expression<Func<Poem, bool>> Where
    {
        get => where;
        set => canLoadMore = SetProperty(ref where, value);
    }

    [ObservableProperty]
    string status;

    public MauiInfiniteScrollCollection<Poem> Poetry { get; }
    //to do: 这是测试
    private readonly IPoetryStorage poetryStorage;

    public ResultPageViewModel(IPoetryStorage poetryStorage)
    {
        //to do: 这是测试
        Where = Expression.Lambda<Func<Poem, bool>>(
            Expression.Constant(true),
            Expression.Parameter(typeof(Poem), "p"));
        this.poetryStorage = poetryStorage;

        Poetry = new MauiInfiniteScrollCollection<Poem>
        {
            OnCanLoadMore = () => canLoadMore,
            OnLoadMore = async () =>
            {
                Status = Loading;
                List<Poem> poetry = (await poetryStorage.GetPoetryAsync(Where,
                                                                 Poetry.Count,
                                                                 PageSize)).ToList();
                Status = string.Empty;
                if (poetry.Count < PageSize)
                {//没有更多结果了
                    canLoadMore = false;
                    Status = NoMoreReult;
                }

                if (Poetry.Count == 0 && poetry.Count == 0)
                {
                    canLoadMore = false;
                    Status = NoResult;
                }
                return poetry;
            }
        }; 
    }

    //旧版本异步方法
    //private AsyncRelayCommand navigatedToAsyncCommand;
    //public AsyncRelayCommand NavigatedToAsyncCommand =>
    //    navigatedToAsyncCommand ??= new AsyncRelayCommand(
    //        async () =>
    //        {
    //            Poetry.Clear();
    //            await Poetry.LoadMoreAsync();
    //        } );


    [RelayCommand]
    public async Task NavigatedTo()
    {
        await poetryStorage.InitailizeAsync();
        Poetry.Clear();
        await Poetry.LoadMoreAsync();
    }
}
