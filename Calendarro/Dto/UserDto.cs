using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendarro.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string EMail { get; set; }
        public DateTime CreateDate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
    }
}
