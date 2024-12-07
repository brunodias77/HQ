namespace HQ.Domain.Security.PasswordEncripter;

public interface IPasswordEncripter
{
    string Encrypt(string password);

    bool Verify(string password, string passwordHash);
}