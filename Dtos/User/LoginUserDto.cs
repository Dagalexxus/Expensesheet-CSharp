namespace api.Dtos{
    public class LoginUserDto{
        public string username {get; set;} = string.Empty;
        public string passwordHash{get; set;} = string.Empty;
    }
}