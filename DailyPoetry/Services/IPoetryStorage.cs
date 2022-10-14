using System.Linq.Expressions;

namespace DailyPoetry.Services;

public interface IPoetryStorage
{
    bool IsInitialized { get; }
    Task InitailizeAsync();
    Task<Poetry> GetPoemAsync(int id);
    Task<IEnumerable<Poetry>> GetPoetryAsync(
        Expression<Func<Poetry, bool>> where, int skip, int take);
}
