using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<IEnumerable<User>> GetActiveUsers();
    public Task<User?> GetUserByLoginAndPassword(string login, string password);
    public Task<User?> GetUserByLogin(string? login);
    public Task<IEnumerable<User>> GetUsersOlderThan(int age);
}