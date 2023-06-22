using DataAccessLibrary_BidStamp;
using DataAccessLibrary_BidStamp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer_BidStamp.Models {
    public interface IStampRepository {
        Task<bool> deleteStamp(Stamp stamp);
        Task<List<Stamp>> getAllStamps();
        Task<Stamp> getStampById(Guid id);
        Task storeStamp(Stamp stamp);
        Task updateStamp(Stamp stamp);
    }

    public class StampRepository : IStampRepository {
        private readonly DatabaseContext _db_context;

        public StampRepository(DatabaseContext db_context) {
            _db_context = db_context;
        }

        public async Task<List<Stamp>> getAllStamps() {
            return await _db_context.Stamps.ToListAsync();
        }

        public async Task<Stamp> getStampById(Guid id) {
            return await _db_context.Stamps.FirstOrDefaultAsync(s => s.StampId == id);
        }

        public async Task storeStamp(Stamp stamp) {
            await _db_context.Stamps.AddAsync(stamp);
            await _db_context.SaveChangesAsync();
        }

        public async Task updateStamp(Stamp stamp) {
            _db_context.Update<Stamp>(stamp);
            await _db_context.SaveChangesAsync();
        }

        public async Task<bool> deleteStamp(Stamp stamp) {
            var listing = await _db_context.Listings.FirstOrDefaultAsync(l => l.Stamp.Equals(stamp));

            _db_context.Stamps.Remove(stamp);
            await _db_context.SaveChangesAsync();
            return true;
        }

    }
}
