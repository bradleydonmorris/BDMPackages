using System.Security.Cryptography;
using System.Text;

namespace BDMCiphers
{
    public static class AESCipher
    {
        private const Int32 AesBlockByteSize = 128 / 8;

        private const Int32 PasswordSaltByteSize = 128 / 8;
        private const Int32 PasswordByteSize = 256 / 8;
        private const Int32 PasswordIterationCount = 100_000;

        private const Int32 SignatureByteSize = 256 / 8;

        private const Int32 MinimumEncryptedMessageByteSize =
            AESCipher.PasswordSaltByteSize + // auth salt
            AESCipher.PasswordSaltByteSize + // key salt
            AESCipher.AesBlockByteSize + // IV
            AESCipher.AesBlockByteSize + // cipher text min length
            AESCipher.SignatureByteSize; // signature tag

        private static readonly Encoding StringEncoding = Encoding.UTF8;
        private static readonly RandomNumberGenerator Random = RandomNumberGenerator.Create();

        public static String Encrypt(String passphrase, String decryptedString)
        {
            Byte[] keySalt = AESCipher.GenerateRandomBytes(AESCipher.PasswordSaltByteSize);
            Byte[] key = AESCipher.GetKey(passphrase, keySalt);
            Byte[] iv = AESCipher.GenerateRandomBytes(AESCipher.AesBlockByteSize);

            Byte[] cipherText;
            using (Aes aes = AESCipher.CreateAes())
            using (ICryptoTransform encryptor = aes.CreateEncryptor(key, iv))
            {
                Byte[] plainText = AESCipher.StringEncoding.GetBytes(decryptedString);
                cipherText = encryptor
                    .TransformFinalBlock(plainText, 0, plainText.Length);
            }

            Byte[] authKeySalt = AESCipher.GenerateRandomBytes(AESCipher.PasswordSaltByteSize);
            Byte[] authKey = AESCipher.GetKey(passphrase, authKeySalt);

            Byte[] result = AESCipher.MergeArrays(
                additionalCapacity: AESCipher.SignatureByteSize,
                authKeySalt, keySalt, iv, cipherText);

            using HMACSHA256 hmac = new(authKey);
            Int32 payloadToSignLength = result.Length - AESCipher.SignatureByteSize;
            Byte[] signatureTag = hmac.ComputeHash(result, 0, payloadToSignLength);
            signatureTag.CopyTo(result, payloadToSignLength);
            return Convert.ToBase64String(result);
        }

        public static String Decrypt(String passphrase, String encryptedString)
        {
            String returnValue;
            Byte[] encryptedBytes = Convert.FromBase64String(encryptedString);
            if
            (
                encryptedBytes is null
                || encryptedBytes.Length < AESCipher.MinimumEncryptedMessageByteSize
            )
                throw new ArgumentException("Invalid length of encrypted data");
            Byte[] authKeySalt = encryptedBytes
                .AsSpan(0, AESCipher.PasswordSaltByteSize).ToArray();
            Byte[] keySalt = encryptedBytes
                .AsSpan(AESCipher.PasswordSaltByteSize, AESCipher.PasswordSaltByteSize).ToArray();
            Byte[] iv = encryptedBytes
                .AsSpan(2 * AESCipher.PasswordSaltByteSize, AESCipher.AesBlockByteSize).ToArray();
            Byte[] signatureTag = encryptedBytes
                .AsSpan(encryptedBytes.Length - AESCipher.SignatureByteSize, AESCipher.SignatureByteSize).ToArray();
            Int32 cipherTextIndex = authKeySalt.Length + keySalt.Length + iv.Length;
            Int32 cipherTextLength =
                encryptedBytes.Length - cipherTextIndex - signatureTag.Length;
            Byte[] authKey = AESCipher.GetKey(passphrase, authKeySalt);
            Byte[] key = AESCipher.GetKey(passphrase, keySalt);

            using HMACSHA256 hmac = new(authKey);
            Int32 payloadToSignLength = encryptedBytes.Length - AESCipher.SignatureByteSize;
            Byte[] signatureTagExpected = hmac.ComputeHash(
                encryptedBytes,
                0,
                payloadToSignLength
            );
            Int32 signatureVerificationResult = 0;
            for (Int32 i = 0; i < signatureTag.Length; i++)
                signatureVerificationResult |= signatureTag[i] ^ signatureTagExpected[i];
            if (signatureVerificationResult != 0)
                throw new CryptographicException("Invalid signature");


            using Aes aes = AESCipher.CreateAes();
            using ICryptoTransform encryptor = aes.CreateDecryptor(key, iv);
            Byte[] decryptedBytes = encryptor.TransformFinalBlock(
                encryptedBytes, cipherTextIndex, cipherTextLength
            );
            returnValue = AESCipher.StringEncoding.GetString(decryptedBytes);
            return returnValue;
        }

        private static Aes CreateAes()
        {
            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            return aes;
        }

        private static Byte[] GetKey(String password, Byte[] passwordSalt)
        {
            Byte[] passwordBytes = AESCipher.StringEncoding.GetBytes(password);
            using Rfc2898DeriveBytes rfc2898DeriveBytes = new(
                passwordBytes,
                passwordSalt,
                AESCipher.PasswordIterationCount,
                HashAlgorithmName.SHA256
            );
            return rfc2898DeriveBytes.GetBytes(AESCipher.PasswordByteSize);
        }

        private static Byte[] GenerateRandomBytes(Int32 numberOfBytes)
        {
            Byte[] randomBytes = new Byte[numberOfBytes];
            AESCipher.Random.GetBytes(randomBytes);
            return randomBytes;
        }

        private static Byte[] MergeArrays(Int32 additionalCapacity = 0, params Byte[][] arrays)
        {
            var merged = new byte[arrays.Sum(a => a.Length) + additionalCapacity];
            var mergeIndex = 0;
            for (Int32 i = 0; i < arrays.GetLength(0); i++)
            {
                arrays[i].CopyTo(merged, mergeIndex);
                mergeIndex += arrays[i].Length;
            }
            return merged;
        }
    }
}