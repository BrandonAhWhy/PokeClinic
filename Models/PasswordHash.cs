using System.Security.Cryptography;
using BCr = BCrypt.Net;

namespace PokeClinic.Models
{
	public class PasswordHash  
	{
	    private static string GetRandomSalt()
	    {
	        return BCr.BCrypt.GenerateSalt(12);
	    }

	    public static string HashPassword(string password)
	    {
	        return BCr.BCrypt.HashPassword(password, GetRandomSalt());
	    }

	    public static bool ValidatePassword(string password, string correctHash)
	    {
	        return BCr.BCrypt.Verify(password, correctHash);
	    }
	}
}
