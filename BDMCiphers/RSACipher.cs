using System.Security.Cryptography;
using System.Text;

namespace BDMCiphers
{
    public class RSACipher
    {
        private const Int32 KeySize = 2048;
        public static void GenerateKey(String privateKeyFilePath, String? publicFilePath = null)
        {
            if (String.IsNullOrEmpty(publicFilePath))
                publicFilePath = $"{privateKeyFilePath}.pub";
            using RSA rsa = RSA.Create(RSACipher.KeySize);
            File.WriteAllText(privateKeyFilePath, rsa.ToXmlString(true));
            File.WriteAllText(publicFilePath, rsa.ToXmlString(false));
        }

        public static KeyPair GenerateKeyPair()
        {
            using RSA rsa = RSA.Create(RSACipher.KeySize);
            return new KeyPair()
            {
                PrivateKeyXML = rsa.ToXmlString(true),
                PublicKeyXML = rsa.ToXmlString(false)
            };
        }

        public static String Sign(String privateKey, String hash)
        {
            using RSA rsa = RSA.Create(512);
            rsa.FromXmlString(privateKey);
            Byte[] signature = rsa.SignHash(
                Convert.FromBase64String(hash),
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1
            );
            return Convert.ToBase64String(signature, 0, signature.Length);
        }

        public static Boolean IsSignatureValid(String publicKey, String hash, String signedHash)
        {
            using RSA rsa = RSA.Create(512);
            rsa.FromXmlString(publicKey);
            return rsa.VerifyHash(
                Convert.FromBase64String(hash),
                Convert.FromBase64String(signedHash),
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1
            );
        }

        public static String Encrypt(String publicKey, String decryptedString)
        {
            using RSA rsa = RSA.Create(RSACipher.KeySize);
            rsa.FromXmlString(publicKey);
            Byte[] encryptedBytes = rsa.Encrypt(
                Encoding.UTF8.GetBytes(decryptedString),
                RSAEncryptionPadding.Pkcs1
            );
            return Convert.ToBase64String(encryptedBytes, 0, encryptedBytes.Length);
        }

        public static String Decrypt(String privateKey, String encryptedString)
        {
            using RSA rsa = RSA.Create(RSACipher.KeySize);
            rsa.FromXmlString(privateKey);
            return Encoding.UTF8.GetString(
                rsa.Decrypt(
                    Convert.FromBase64String(encryptedString),
                    RSAEncryptionPadding.Pkcs1
                )
            );
        }

        public static void Encrypt(String publicFilePath, String decryptedFilePath, String encryptedFilePath)
        {
            RSA rsa = RSA.Create(RSACipher.KeySize);
            String publicKeyString = File.ReadAllText(publicFilePath);
            rsa.FromXmlString(publicKeyString);
            String decryptedString = File.ReadAllText(decryptedFilePath);
            Byte[] decryptedBytes = Encoding.UTF8.GetBytes(decryptedString);
            Byte[] encryptedBytes = rsa.Encrypt(decryptedBytes, RSAEncryptionPadding.Pkcs1);
            String encryptedString = Convert.ToBase64String(encryptedBytes, 0, encryptedBytes.Length);
            File.WriteAllText(encryptedFilePath, encryptedString);
        }

        public static void Decrypt(String privateFilePath, String encryptedFilePath, String decryptedFilePath)
        {
            RSA rsa = RSA.Create(RSACipher.KeySize);
            String privateKeyString = File.ReadAllText(privateFilePath);
            rsa.FromXmlString(privateKeyString);
            String encryptedString = File.ReadAllText(encryptedFilePath);
            Byte[] encryptedBytes = Convert.FromBase64String(encryptedString);
            Byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
            String decryptedString = Encoding.UTF8.GetString(decryptedBytes);
            File.WriteAllText(decryptedFilePath, decryptedString);
        }
    }
}
