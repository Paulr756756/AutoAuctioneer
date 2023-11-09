namespace Client.Shared; 

public static class EnvUrls {
    public const string BaseUrl = "https://localhost:7162/api/";
    public const string User = "user/";
    public const string Car = "car/";
    public const string Bid = "bid/";
    public const string Part = "part/";
    public const string Listing = "listing/";
    public const string Item = "item/";

    public const string GetAll = "getall";
    
    //car
    public const string GetOwned = "getowned";
    public const string StoreCar = "store";
    public const string UpdateCar = "";
    
    //item
    public const string GetById = "getById?id=";

    public const string StorePart = "";

    //crud
    public const string Add = "add";
    public const string Update = "update";
    public const string Delete = "delete";

    //listing
    public const string GetListings = "listing/getresponselist";
    public const string GetListingById = "listing/getresponsebyid?id=";
    public const string VerifyUser = "";
}