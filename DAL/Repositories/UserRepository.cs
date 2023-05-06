using DAL.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(User entity)
    {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetById(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<User>> GetActiveUsers()
    {
        return await _context.Users.Where(u => u.RevokedOn == null).OrderBy(s => s.CreatedOn).ToListAsync();
    }

    public async Task<User?> GetUserByLoginAndPassword(string login, string password)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
    }

    public async Task<User?> GetUserByLogin(string? login)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task<IEnumerable<User>> GetUsersOlderThan(int age)
    {
        var date = new DateTime(DateTime.Today.Year - age, DateTime.Today.Month, DateTime.Today.Month);
        return await _context.Users.Where(u => u.Birthday != null && u.Birthday < date).ToListAsync();
    }
}