using AspNetCore3_WebAPI_JWT.Models;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCore3_WebAPI_JWT.Repositories
{
    public static class UserRepository
    {
        private static List<User> _users = new List<User> {
            new User(){ Id = 1, Username = "batman", Password = "batman123", Role = "manager" }, 
            new User(){ Id = 2, Username = "robin", Password = "robin123", Role = "employee" }
        };
        
        public static User Get(string username, string password)
        {
            return _users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
        } 
    }
}