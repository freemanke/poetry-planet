using System.Text;

namespace PoetryPlanet.Web.Data.Models;

public static class Extensions
{
    private static readonly Dictionary<string, string> workKindDict = new()
    {
        { "shi", "诗" },
        { "ci", "词" },
        { "wen", "文" },
        { "fu", "赋" },
        { "qu", "曲" },
    };

    public static string GetYearString(this Dynasty d)
    {
        var sb = new StringBuilder();
        sb.Append(d.StartYear < 0 ? "公元前 " : "");
        sb.Append($"{Math.Abs(d.StartYear)}");
        sb.Append(" - ");
        sb.Append(d.EndYear < 0 ? "公元前 " : "");
        sb.Append($"{Math.Abs(d.EndYear)}");
        return sb.ToString();
    }

    public static string GetKindString(this Work e)
    {
        return workKindDict.TryGetValue(e.Kind, out var kind) ? kind : e.Kind;
    }

    public static string GetAuthorDesc(this Work e)
    {
        return string.IsNullOrEmpty(e.AuthorDesc) ? "暂无信息" : e.AuthorDesc;
    }

    public static string GetKindString(this Quote e)
    {
        return workKindDict.TryGetValue(e.Kind, out var kind) ? kind : e.Kind;
    }
}