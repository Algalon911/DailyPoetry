using SQLite;
namespace DailyPoetry.Models;

[Table("works")]
public class Poem
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("author_name")]
    public string Author { get; set; }
    [Column("dynasty")]
    public string Dynasty { get; set; }
    [Column("content")]
    public string Content { get; set; }

    private string snippet;
    [Ignore]
    public string Snippet => snippet ?? Content.Split("。")[0];

}
