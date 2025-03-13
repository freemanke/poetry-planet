namespace PoetryPlanet.Web;

public static class Consts
{
    public const string DEFAULT_CONNECTION = "DefaultConnection";
    public const string WORKS_ALERT_CLOSED = "WORKS_ALERT_CLOSED";
    public const string CURRENT_THEME = "CURRENT_THEME";

    public const string MYSQL_ROOT_PASSWORD = "MYSQL_ROOT_PASSWORD";
    public const string MYSQL_CONTAINER_NAME = "poetry-planet-mysql";
    
    public const string RouterHome = "/";
    public const string RouterAuth = "/auth";
    public const string RouterError = "/error";
    public const string RouterNotFound = "/not-found";
    
    public const string RouterDynasties = "/dynasties";
    public const string RouterAuthors = "/authors";
    public const string RouterAuthorDetail = "/authors/{id}";
    public const string RouterWorks = "/works";    
    public const string RouterWorkDetail = "/works/{id}";
    public const string RouterCollections = "/collections";
    public const string RouterQuotes = "/quotes";
    public const string RouterMyFavorites = "/my-favorites";
    public const string RouterMyCollections = "/my-collections";
    public const string RouterSettings = "/settings";
}