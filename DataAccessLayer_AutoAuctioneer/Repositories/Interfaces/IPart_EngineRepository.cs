using DataAccessLayer_AutoAuctioneer.Models;

public interface IPart_EngineRepository {
    Task<Part_Engine?> GetEngineById(Guid guid);
   
}