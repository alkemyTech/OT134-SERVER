using Microsoft.EntityFrameworkCore;
using OngProject.Core.Helper;
using OngProject.DataAccess;
using OngProject.Entities;
using System;

namespace Test.Helper
{
    internal class PrepareDbContextHelper
    {
        private static AppDbContext _context { get; set; }

        public static AppDbContext MakeDbContext(bool pupulate = true)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "Ong").Options;

            _context = new AppDbContext(options);
            _context.Database.EnsureDeleted();

            if (pupulate)
            {
                PrepareRoles();
                PrepareUsers();
                SeedMembers();
                SeedOrganization();
                SeedSlide();
                SeedNews();
                SeedCategory();
                _context.SaveChanges();
            }

            return _context;
        }
        
        private static void PrepareRoles()
        {
            _context.Add(new Rol
            {
                Id = 1,
                Name = "User",
                Description = "Regular user without permissions"
            });
            _context.Add(new Rol
            {
                Id = 2,
                Name = "Administrator",
                Description = "Administrator user with permissions"
            });
        }

        private static void PrepareUsers()
        {
            for (int i = 1; i < 11; i++)
            {
                var delete = i > 1 ? false : true; // el primer usuario lo creo eliminado
                _context.Add(
                    new User
                    {
                        Id = i,
                        FirstName = "Name User " + i,
                        LastName = "Last Name User" + i,
                        Email = "User" + i + "@ong.com",
                        Password = EncryptHelper.GetSHA256("Password" + i),
                        Photo = "Photo" + i,
                        SoftDelete = delete,
                        RolId = 1,
                        LastModified = DateTime.Now
                    }
                );
            }

            for (int i = 11; i < 21; i++)
            {
                _context.Add(
                    new User
                    {
                        Id = i,
                        FirstName = "Name User " + i,
                        LastName = "Last Name User" + i,
                        Email = "User" + i + "@ong.com",
                        Password = EncryptHelper.GetSHA256("Password" + i),
                        Photo = "Photo" + i,
                        SoftDelete = false,
                        RolId = 2,
                        LastModified = DateTime.Now
                    }
                );
            }
        }
        private static void SeedMembers()
        {
            //agrego este miembro para verificar que no se puede eliminar un miembro ya eliminado
            _context.Add(
                    new Member
                    {
                        Id = 1,
                        Name = "Name " + 1,
                        Image = "Image " + 1,
                        FacebookUrl = "Facebook Url " + 1,
                        InstagramUrl = "Instagram Url " + 1,
                        LinkedinUrl = "Linkedin Url " + 1,
                        Description = "Description " + 1,
                        SoftDelete = true,
                        LastModified = DateTime.Now
                    }
                );
            for (int i = 2; i < 12; i++)
            {
                _context.Add(
                    new Member
                    {
                        Id = i,
                        Name = "Name " + i,
                        Image = "Image " + i,
                        FacebookUrl = "Facebook Url " + i,
                        InstagramUrl = "Instagram Url " + i,
                        LinkedinUrl = "Linkedin Url " + i,
                        Description = "Description " + i,
                        SoftDelete = false,
                        LastModified = DateTime.Now
                    }
                );
            }
        }
        private static void SeedOrganization()
        {
            for (int i = 1; i < 11; i++)
            {
                _context.Add(
                    new Organization
                    {
                        Id = i,
                        Name = "Organization name " + i,
                        Image = "Image " + i,
                        Address = "Address " + i,
                        Phone = int.MaxValue,
                        Email = "name@organization.com",
                        WelcomeText = "Welcome text " + i,
                        AboutUsText = "About us text " + i,
                        FacebookUrl = "Facebook url " + i,
                        InstagramUrl = "Instagram url " + i,
                        LinkedinUrl = "Linkedin url " + i,
                        SoftDelete = false,
                        LastModified = DateTime.Now
                    }
                );
            }
        }

        public static void SeedSlide()
        {
            for (int i = 1; i < 11; i++)
            {
                _context.Add(
                    new Slides
                    {
                        Id = i,
                        ImageUrl = "Image " + i,
                        Text = "Text " + i,
                        Order = i,
                        OrganizationId = i,
                        SoftDelete = false,
                        LastModified = DateTime.Now
                    }
               );
            }
        }
        private static void SeedNews()
        {
            for (int i = 1; i < 11; i++)
            {
                _context.Add(
                    new New
                    {
                        Id = i,
                        Name = "New " + i,
                        Content = "Content for New " + i,
                        Image = "image_new " + i,
                        LastModified = DateTime.Now,
                        CategoryId = i,
                    }
               );
            }
        }
        private static void SeedCategory()
        {
            for (int i = 1; i < 11; i++)
            {
                _context.Add(
                    new Category
                    {
                        Id = i,
                        Name = "Category " + i,
                        Description = "Description for Category" + i,
                        Image = "image_category" + i,
                        LastModified = DateTime.Now
                    }
               );
            }
        }

    }
}
