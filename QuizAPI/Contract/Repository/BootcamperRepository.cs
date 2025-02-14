using Microsoft.EntityFrameworkCore;
using QuizAPI.Contract.Interface;
using QuizAPI.Data;
using QuizAPI.Model;

namespace QuizAPI.Contract.Repository
{
    public class BootcamperRepository : IBootcamperRepository
    {
        private readonly AppDbContext _context;

        public BootcamperRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Bootcamper?> CreateBootcamper(Bootcamper bootcamper)
        {
            try
            {
                var created_bootcamper = await _context.Bootcampers.AddAsync(bootcamper);
                if (created_bootcamper == null)
                {
                    return null;
                }
                await _context.SaveChangesAsync();
                return bootcamper;
            }
            catch (Exception ex) 
            {
                throw new Exception("Something went wrong while adding the data");
            }

        }

        public async Task<Bootcamper?> DeleteBootcamper(int id)
        {
            var bootcamper = await _context.Bootcampers
                .FirstOrDefaultAsync(bc => bc.BootcamperId == id);
            if (bootcamper == null) 
            { 
                return null;
            }
            _context.Remove(bootcamper);
            await _context.SaveChangesAsync();
            return bootcamper;
        }

        public async Task<ICollection<Bootcamper>?> Get()
        {
            var bootcampers = await _context.Bootcampers.ToListAsync();
            if(bootcampers == null)
            {
                return null;
            }
            return bootcampers;
        }

        public async Task<Bootcamper?> GetById(int id)
        {
            var bootcampers = await _context.Bootcampers
                .FirstOrDefaultAsync(bc => bc.BootcamperId == id);
            if(bootcampers == null)
            {
                return null;
            }
            return bootcampers;
        }

        public Task<Bootcamper?> UpdateBootcamper(int id, Bootcamper bootcamper)
        {
            throw new NotImplementedException();
        }
    }
}
