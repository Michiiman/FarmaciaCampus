
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

    public class UserRepository :GenericRepository<User>, IUser
    {
         protected readonly FarmaciaContext _context;
        
        public UserRepository(FarmaciaContext context) : base (context)
        {
            _context = context;
        }
        
       public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(p => p.Rols)
                .ToListAsync();
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
            .Include(p => p.Rols)
            .FirstOrDefaultAsync(p =>  p.Id == id);
        }
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
            .Include(p => p.Rols)
            .FirstOrDefaultAsync(p =>  p.Username.ToLower() == username.ToLower());
        }
    }