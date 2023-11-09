namespace Client.Store.Base;

public class FetchBaseDataResultAction {
    public bool IsLoggedIn { get; set; }
    public string? UserId { get; set; }
    public string? Username { get; set; } 
    public string? JwtToken { get; set; }

    public FetchBaseDataResultAction(string? userId, string? username, string? jwtToken, bool isLoggedIn) {
        UserId = userId;
        Username = username;
        JwtToken = jwtToken;
        IsLoggedIn = isLoggedIn;
    }
}