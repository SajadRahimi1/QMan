namespace QMan.Application.Dtos.Base;

public class UserJwtModel
{
    public int UserId { get; set; }
    public UserRole Role { get; set; }
}

public enum UserRole
{
    Admin,
    Business,
    User,
}