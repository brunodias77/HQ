using HQ.Domain.Security.PasswordEncripter;
using BC = BCrypt.Net.BCrypt;

namespace HQ.Infra.Security.PasswordEncripter;

public  class BCrypt : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }

    public bool Verify(string password, string passwordHash) => BC.Verify(password, passwordHash);
}