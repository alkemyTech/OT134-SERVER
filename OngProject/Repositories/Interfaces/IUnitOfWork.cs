using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<Activities> ActivitiesRepository { get; }
        public IRepository<New> NewsRepository { get; }
        public IRepository<Testimonials> TestimonialsRepository { get; }
        public IRepository<User> UserRepository { get; }
        public IRepository<Member> MemberRepository { get; }
        public IRepository<Organization> OrganizationRepository { get; }
        public IRepository<Category> CategoryRepository { get; }
        public IRepository<Rol> RolRepository { get; }
        public IRepository<Slides> SlideRepository { get; }
        public void Dispose();
        public void SaveChanges();
        public Task SaveChangesAsync();
    }
}
