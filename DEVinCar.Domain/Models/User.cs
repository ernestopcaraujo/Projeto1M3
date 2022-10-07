using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Enum;

namespace DEVinCar.Domain.Models
{
    public class User
    {
        public int Id {get; internal set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role {get; set;}

        public User()
        {
            
        }
        public User(
            int id, 
            string email, 
            string password, 
            string name, 
            DateTime birthDate,
            string role
            )
        {
            Id = id;
            Email = email;
            Password = password;
            Name = name;
            BirthDate = birthDate;
            Role = role;
        }
        public User(UserDTO userDTO)
        {
            Name = userDTO.Name;
            Email = userDTO.Email;
            Password = userDTO.Password;
            BirthDate = userDTO.BirthDate;
            Role = userDTO.Role;
        }
    }
}