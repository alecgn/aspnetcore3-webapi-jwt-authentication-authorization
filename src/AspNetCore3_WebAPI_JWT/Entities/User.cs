using System.ComponentModel.DataAnnotations;

namespace AspNetCore3_WebAPI_JWT.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        public string Role { get; set; }
    }
}