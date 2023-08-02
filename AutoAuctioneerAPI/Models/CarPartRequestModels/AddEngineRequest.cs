namespace API_AutoAuctioneer.Models.CarPartRequestModels;

public class AddEngineRequest : AddCarPartRequest
{
    public string EngineType { get; set; }
    public double Displacement { get; set; }
    public int Horsepower { get; set; }
    public int Torque { get; set; }
}