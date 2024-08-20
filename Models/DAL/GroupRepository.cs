using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApplication1.Models.DAL
{
    public class GroupRepository : IGroupRepository
    {
        private MyWebApiContext _dbContext;
        public GroupRepository(MyWebApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteGroup(int groupId)
        {
            Group? group = _dbContext.Groups.Find(groupId);
            if (group != null)
            {
                _dbContext.Groups.Remove(group);
            }
        }
        public Group? GetGroupById(int groupId)
        {
            try
            {
                return _dbContext.Groups.Find(groupId);             
            }catch(NullReferenceException ex)
            {
                Console.WriteLine(ex.Message); 
                return null;
            } 
        }

        public IQueryable<Group> GetGroups()
        {
            return _dbContext.Groups.AsQueryable<Group>();
        }

        public void InsertGroup(Group group)
        {
            _dbContext.Groups.Add(group);
        }

        public void UpdateGroup(Group group)
        {
            _dbContext.Entry(group).State= EntityState.Modified;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
