using WebApplication1.Entities;
using WebApplication1.Repositories.Base;

namespace WebApplication1.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
