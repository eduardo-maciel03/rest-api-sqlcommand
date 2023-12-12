using API.Models;

namespace API.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<Users>> Get();
        public Task<IEnumerable<Users>> GetID(int id);
        public Task<int> Create(Users user);
        public Task<int> Update(int id, Users user);
        public Task<int> Delete(int id);
    }
}
