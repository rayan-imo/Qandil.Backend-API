using Microsoft.AspNetCore.Identity;
using Qandil.Core.AuthServices.Hasher;
using System.Security.Cryptography;

namespace Qandil.Services.AuthServices.Hasher;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16; // 128 bits
    private const int HashSize = 32; // 256 bits
    private const int Iterations = 10000; // Adjust based on your performance needs

    public string HashPassword(string password)
    {
        // Generate a random salt
        byte[] salt;
        RandomNumberGenerator.Fill(salt = new byte[SaltSize]);

        // Create the hash
        var pbkdf2 = new Rfc2898DeriveBytes(
            password: password,
            salt: salt,
            iterations: Iterations,
            hashAlgorithm: HashAlgorithmName.SHA256);

        byte[] hash = pbkdf2.GetBytes(HashSize);

        // Combine salt and hash
        byte[] hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        // Convert to base64
        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        // Extract bytes
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);

        // Get salt
        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        // Create hash with provided password
        var pbkdf2 = new Rfc2898DeriveBytes(
            password: providedPassword,
            salt: salt,
            iterations: Iterations,
            hashAlgorithm: HashAlgorithmName.SHA256);

        byte[] hash = pbkdf2.GetBytes(HashSize);

        // Compare
        for (int i = 0; i < HashSize; i++)
        {
            if (hashBytes[i + SaltSize] != hash[i])
                return false;
        }
        return true;
    }
}
