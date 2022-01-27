﻿using Microsoft.EntityFrameworkCore;
using OngProject.Entities;
using System;

namespace OngProject.DataAccess
{
    public class SeedDataUser
    {
        public void SeedRegularUsers(ModelBuilder modelBuilder)
        {
            for (int i = 1; i < 11; i++)
            {
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        FirstName = "Name User " + i,
                        LastName = "Last Name User" + i,
                        Email = "Email User" + i,
                        Password = "Password" + i,
                        Photo = "Photo" + i,
                        SoftDelete = false,
                        RolId = 1,
                        LastModified = DateTime.Now
                    }
                );
            }
        }
        public void SeedAdministratorUsers(ModelBuilder modelBuilder)
        {
            for (int i = 11; i < 21; i++)
            {
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        FirstName = "Name User " + i,
                        LastName = "Last Name User" + i,
                        Email = "Email User" + i,
                        Password = "Password" + i,
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