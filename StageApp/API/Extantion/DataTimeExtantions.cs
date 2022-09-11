using System;

namespace API.Extensions
{
    public static class DataTimeExtensions
    {
        public static int CalculateAge(this DateTime dob)
        {
            var age = DateTime.Now.Year - dob.Year;
            if (dob.AddYears(age) > DateTime.Now) { age--; }
            return age;
        }
    }
}