using DataAccessLayer_BidStamp.Models;
using DataAccessLibrary_BidStamp;
using DataAccessLibrary_BidStamp.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer_BidStamp.Repositories;

public interface IStampRepository {
    Task<bool> deleteStamp(Stamp stamp);
    Task<List<Stamp>> getAllStamps();
    Task<Stamp> getStampById(Guid id);
    Task storeStamp(Stamp stamp);
    Task updateStamp(Stamp stamp);
}

public class StampRepository : IStampRepository {
    private readonly DatabaseContext _dbContext;

    public StampRepository(DatabaseContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<List<Stamp>> getAllStamps() {
        return await _dbContext.Stamps.ToListAsync();
    }

    public async Task<Stamp> getStampById(Guid id) {
        return await _dbContext.Stamps.FirstOrDefaultAsync(s => s.StampId == id);
    }

    public async Task storeStamp(Stamp stamp) {
        await _dbContext.Stamps.AddAsync(stamp);
        await _dbContext.SaveChangesAsync();
    }

    public async Task updateStamp(Stamp stamp) {
        _dbContext.Update(stamp);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> deleteStamp(Stamp stamp) {
        var listing = await _dbContext.Listings.FirstOrDefaultAsync(l => l.Stamp.Equals(stamp));

        _dbContext.Stamps.Remove(stamp);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}