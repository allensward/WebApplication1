using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models.DAL
{
    public class UserRepository : IUsersRepository
    {
        private MyWebApiContext _dbContext;
        public UserRepository(MyWebApiContext context)
        {
            _dbContext = context;
        }
        public IQueryable<User> GetUsers()
        {
            return _dbContext.Users.AsQueryable<User>();
        }
        public User? GetUserById(int id)
        {
            return _dbContext.Users.Find(id);
        }
        public void InsertUser(User user)
        {
            _dbContext.Users.Add(user);
        }
        public void UpdateUser(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
        }
        public void DeleteUser(int id)
        {
            User? user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
            }
            
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
