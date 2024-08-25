using api.Dtos;
using api.Models;

namespace api.Mappers{
    public static class userMappers{
        public static UserDTO toUserDto(this User user){
            return new UserDTO{
                username = user.username,
                userEmail = user.userEmail
            };
        }

        public static User toUserFromRegistration(this RegisterUserRequestDto registerUserDto){
            return new User{
                username = registerUserDto.username,
                userEmail = registerUserDto.userEmail,
                passwordHash = registerUserDto.passwordHash
            };
        }
    }
}