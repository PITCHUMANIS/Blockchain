using System.Security.Cryptography;

namespace PropertyOwnershipRegistration.Cryptography
{
	public class HashData
	{
		public static byte[] ComputeHashSha256(byte[] toBeHashed)
		{
			using (var sha256 = SHA256.Create())
			{
				return sha256.ComputeHash(toBeHashed);
			}
		}
	}
}
