namespace PortfolioApi.Models;

public class UserStats
{
    public string Owner { get; set; } = "Mykhailo Peresypkin";
    public ServerInfo Status { get; set; } = new();
    public ChessStats Chess { get; set; } = new();
    public GithubStats Github { get; set; } = new();
}

public class ServerInfo
{
    public string Environment { get; set; } = "Ubuntu Server (Home Lab)";
    public string Framework { get; set; } = ".NET 8";
    public bool IsOnline { get; set; } = true;
}

public class ChessStats
{
    public int BlitzRating { get; set; }
    public int RapidRating { get; set; }
}

public class GithubStats
{
    public int PublicRepos { get; set; }
    public int Followers { get; set; }
}