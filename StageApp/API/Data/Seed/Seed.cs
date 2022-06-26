using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using API.Entities;
using System;
using Microsoft.Extensions.Logging;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {

            if (await context.Users.AnyAsync()) { return; }

            var students = await EnterUsersAsync<Student>(context, "Data/Seed/StudentSeedData.json");
            await EnterUsersAsync<Teacher>(context, "Data/Seed/TeacherSeedData.json");
            var courses = await EnterCourseUnitsAsync(context);
            await EnterStudentsAsync(context, students, courses);

        }

        private static async Task<List<Course>> EnterCourseUnitsAsync(DataContext context)
        {
            var random = new Random();

            var courses = await AsList<Course>("Data/Seed/CourseSeedData.json");
            var units = await AsList<Unit>("Data/Seed/UnitSeedData.json");


            foreach (var course in courses)
            {
                course.Units = new List<Unit>();
                context.Add(course);
            }
            await context.SaveChangesAsync();

            foreach (var unit in units)
            {
                var course = courses[random.Next(0, courses.Count)];
                course.Units.Add(unit);
            }
            await context.SaveChangesAsync();
            return courses;
        }

        private static async Task EnterStudentsAsync(DataContext context, List<Student> students, List<Course> courses)
        {
            var random = new Random();

            for (int i = 0; i < 30; i++)
            {
                var course = courses[random.Next(0, courses.Count)];
                course.Students.Add(students[random.Next(0, students.Count)]);
            }
            await context.SaveChangesAsync();
        }

        private static async Task<List<T>> AsList<T>(string path)
        {
            var data = await System.IO.File.ReadAllTextAsync(path);
            var list = JsonSerializer.Deserialize<List<T>>(data);
            return list;
        }

        private static async Task<List<T>> EnterUsersAsync<T>(DataContext context, string path) where T : AppUser
        {

            var Users = await AsList<T>(path);

            foreach (var user in Users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$word"));
                user.PasswordSalt = hmac.Key;

                context.Add(user);
            }
            await context.SaveChangesAsync();
            return Users;
        }
    }
}