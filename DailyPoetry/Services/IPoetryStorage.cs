using System.Linq.Expressions;

namespace DailyPoetry.Services;

public interface IPoetryStorage
{
    bool IsInitialized { get; }
    Task InitailizeAsync();
    Task<Poem> GetPoemAsync(int id);
    Task<IEnumerable<Poem>> GetPoetryAsync(
        Expression<Func<Poem, bool>> where, int skip, int take);
}
