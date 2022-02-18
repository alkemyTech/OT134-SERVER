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

        public static AppDbContext MakeDbContext(bool pupulate=true)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "Ong").Options;
            
            _context = new AppDbContext(options);
            _context.Database.EnsureDeleted();

            if (pupulate)
            {
                PrepareRoles();
                PrepareUsers();
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
    }
}
