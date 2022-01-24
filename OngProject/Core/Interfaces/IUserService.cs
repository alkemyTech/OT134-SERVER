using OngProject.Entities;
using System.Collections.Generic;

namespace OngProject.Core.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<User> GetAll();
        public User GetById();
        public void Insert(User user);
        public void Update(User user);
        public void Delete(User user);
    }
}