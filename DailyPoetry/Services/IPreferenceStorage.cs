namespace DailyPoetry.Services;

public interface IPreferenceStorage
{
    void Set(string key, int value);
    int Get(string key, int defaultValue);
}
