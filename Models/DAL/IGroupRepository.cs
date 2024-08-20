namespace WebApplication1.Models.DAL
{
    public interface IGroupRepository
    {
        IQueryable<Group> GetGroups();
        Group? GetGroupById(int id);
        void InsertGroup(Group group);
        void UpdateGroup(Group group);
        void DeleteGroup(int id);
        void SaveChanges();
    }
}
