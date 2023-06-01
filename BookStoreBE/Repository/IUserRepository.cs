using BookStoreBE.Models;

namespace BookStoreBE.Repository
{
    public interface IUserRepository
    {
        Users Login(Users user);        

        void Registration(Users user);
        void Save();
    }
}
    