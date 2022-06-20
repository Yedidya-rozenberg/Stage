using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class MemberDto
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public bool IsActive { get; set; } = true;

    }
}