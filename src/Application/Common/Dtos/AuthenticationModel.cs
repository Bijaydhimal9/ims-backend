namespace Application.Common.Dtos;
public class AuthenticationModel
{
    public UserModel User { get; set; }
    public string Token { get; set; }
}