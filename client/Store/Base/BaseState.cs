namespace Client.Store.Base;
public class BaseState {
    public bool IsLoggedIn { get; set; }
    public string? UserId { get; set; }
    public string? Username { get; set; }
    public string? JwtToken { get; set; }
/*    public GarageState? GarageState { get; set; }*/
//TODO(Figure out garagestate)

    public BaseState() { }

    public BaseState( string? userId, string? username,  string? jwtToken, bool isLoggedIn) {
        IsLoggedIn = isLoggedIn;
        UserId = userId;
        Username = username;
        JwtToken = jwtToken;
    }
}
