using Microsoft.EntityFrameworkCore;
using Vagabond.API.Data;
using Vagabond.API.Models;

namespace Vagabond.API.Interfaces
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly AppDbContext _context;

        public DestinationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            return await _context.Destinations.ToListAsync();
        }

        public async Task<Destination?> GetByIdAsync(int id)
        {
            return await _context.Destinations.FindAsync(id);
            //return _context.Destinations.FirstOrDefault(d => d.Id == id);
        }

        public async Task AddAsync(Destination destination)
        {
            try
            {
                // Ensure the Id is zero so the database identity generates the value
                destination.Id = 0;
                await _context.Destinations.AddAsync(destination);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // rethrow to let middleware handle the error; keep original exception details
                throw;
            }
        }

        public async Task UpdateAsync(Destination destination)
        {
            _context.Destinations.Update(destination);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var dest = await _context.Destinations.FindAsync(id);

            if (dest != null)
            {
                _context.Destinations.Remove(dest);
                await _context.SaveChangesAsync();
            }
        }
    }
}
