using SQLite;
using System.Linq.Expressions;

namespace DailyPoetry.Services;

public class PoetryStorage : IPoetryStorage
{
    public const string DbName = "poetrydb.sqlite3";
    public static readonly string PoetryDbPath = 
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder
                .LocalApplicationData),DbName);

    private readonly IPreferenceStorage preferenceStorage;

    private SQLiteAsyncConnection connection;

    public SQLiteAsyncConnection Connection => connection ?? new SQLiteAsyncConnection(PoetryDbPath);

    public PoetryStorage(IPreferenceStorage preferenceStorage)
    {
        this.preferenceStorage = preferenceStorage;
    }

    public bool IsInitialized =>
        preferenceStorage.Get(PoetryStorageConstant.VersionKey, 0) ==
        PoetryStorageConstant.Version;

    public async Task InitailizeAsync()
    {
        await using var dbFileStream = new FileStream(PoetryDbPath, FileMode.OpenOrCreate);
        await using var dbAssetStream = typeof(PoetryStorage).Assembly.GetManifestResourceStream(DbName)//这里获取嵌入式资源
            ?? throw new Exception($"找不到名为{DbName}的资源");
        await dbAssetStream.CopyToAsync(dbFileStream);
        preferenceStorage.Set(PoetryStorageConstant.VersionKey, PoetryStorageConstant.Version);
    }

    public async Task<IEnumerable<Poem>> GetPoetryAsync(Expression<Func<Poem, bool>> where, int skip, int take) => 
        await Connection.Table<Poem>().Where(where).Skip(skip).Take(take).ToListAsync();

    public async Task<Poem> GetPoemAsync(int id) => 
        await Connection.Table<Poem>().FirstOrDefaultAsync(p => p.Id == id);

    public async Task CloseAsync() => await Connection.CloseAsync();
}

public static class PoetryStorageConstant
{
    public const int Version = 1;
    public const string VersionKey = $"{nameof(PoetryStorageConstant)}.{nameof(Version)}";
}