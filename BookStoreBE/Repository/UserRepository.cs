using BookStoreBE.Models;

namespace BookStoreBE.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _dbContext;
        public UserRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Users GetUserById(int id)
        {
            return _dbContext.users.Find(id);
        }

        public Users Login(Users user)
        {
            return _dbContext.users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
        }

        public void Registration(Users user)
        {
            _dbContext.users.Add(user);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
      
    }
}
