using System.Text;
using System.Threading;
using System.Security.Cryptography;
using System.Runtime.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using API.Entities;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {

            if (await context.Users.AnyAsync()) { return; }

            await EnterUsersAsync<Student>(context, "Data/StudentSeedData.json");
            await EnterUsersAsync<Teacher>(context, "Data/TeacherSeedData.json");
            await EnterUsersAsync<Manager>(context, "Data/ManagerSeedData.json");

            await context.SaveChangesAsync();
        }

        private static async Task EnterUsersAsync<T>(DataContext context, string path) where T : AppUser
        {
            var UsersData = await System.IO.File.ReadAllTextAsync(path);
            var Users = JsonSerializer.Deserialize<List<T>>(UsersData);

            foreach (var user in Users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$word"));
                user.PasswordSalt = hmac.Key;

                context.Add(user);
            }
        }
    }
}