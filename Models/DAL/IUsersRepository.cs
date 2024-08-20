namespace WebApplication1.Models.DAL
{
    public interface IUsersRepository
    {
        IQueryable<User> GetUsers();
        User? GetUserById(int id);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        void SaveChanges();
    }
}
