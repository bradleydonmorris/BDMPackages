using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BDMCiphers
{
    public enum EnvelopeState
    {
        Unknown,
        Sealed,
        Opened,
        Errored
    }

    public class Envelope
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EnvelopeState State { get; set; }
        public String StateComment { get; set; }
        public String Hash { get; set; }
        public String Content { get; set; }
        public String Passphrase { get; set; }

        public Envelope()
        {
            this.State = EnvelopeState.Unknown;
            this.StateComment = String.Empty;
            this.Hash = String.Empty;
            this.Content = String.Empty;
            this.Passphrase = String.Empty;
        }

        public Envelope(String content, String passphrase)
        {
            this.State = EnvelopeState.Unknown;
            this.StateComment = String.Empty;
            this.Hash = String.Empty;
            this.Content = content;
            this.Passphrase = passphrase;
        }

        public void Seal(String publicEncryptionKey, String privateSigningKey)
        {
            using SHA256 sha256 = SHA256.Create();
            Byte[] contentBytes = Encoding.UTF8.GetBytes(this.Content);
            this.Hash = Convert.ToBase64String(
                sha256.ComputeHash(contentBytes, 0, contentBytes.Length)
            );
            this.Hash = RSACipher.Sign(
                privateSigningKey,
                this.Hash
            );
            this.Content = AESCipher.Encrypt(
                this.Passphrase,
                this.Content
            );
            this.Passphrase = RSACipher.Encrypt(
                publicEncryptionKey,
                this.Passphrase
            );
            this.State = EnvelopeState.Sealed;
        }

        public void Open(String privateEncryptionKey, String publicSigningKey)
        {
            String passphrase = this.Passphrase;
            String content = this.Content;
            String hash; // = String.Empty;
            passphrase = RSACipher.Decrypt(
                privateEncryptionKey,
                passphrase
            );
            content = AESCipher.Decrypt(
                passphrase,
                content
            );
            using SHA256 sha256 = SHA256.Create();
            Byte[] contentBytes = Encoding.UTF8.GetBytes(content);
            hash = Convert.ToBase64String(
                sha256.ComputeHash(contentBytes, 0, contentBytes.Length)
            );
            if (!RSACipher.IsSignatureValid(publicSigningKey, hash, this.Hash))
                throw new CryptographicException("Invalid signed hash.");
            this.Content = content;
            this.Passphrase = passphrase;
            this.Hash = hash;
            this.State = EnvelopeState.Opened;
        }

        public void Seal(String publicEncryptionKey)
        {
            using SHA256 sha256 = SHA256.Create();
            Byte[] contentBytes = Encoding.UTF8.GetBytes(this.Content);
            this.Hash = Convert.ToBase64String(
                sha256.ComputeHash(contentBytes, 0, contentBytes.Length)
            );
            this.Content = AESCipher.Encrypt(
                this.Passphrase,
                this.Content
            );
            this.Passphrase = RSACipher.Encrypt(
                publicEncryptionKey,
                this.Passphrase
            );
            this.State = EnvelopeState.Sealed;
        }

        public void Open(String privateEncryptionKey)
        {
            String passphrase = this.Passphrase;
            String content = this.Content;
            String hash; // = String.Empty;
            passphrase = RSACipher.Decrypt(
                privateEncryptionKey,
                passphrase
            );
            content = AESCipher.Decrypt(
                passphrase,
                content
            );
            using SHA256 sha256 = SHA256.Create();
            Byte[] contentBytes = Encoding.UTF8.GetBytes(content);
            hash = Convert.ToBase64String(
                sha256.ComputeHash(contentBytes, 0, contentBytes.Length)
            );
            if (!hash.Equals(this.Hash))
                throw new CryptographicException("Invalid hash.");
            this.Content = content;
            this.Passphrase = passphrase;
            this.Hash = hash;
            this.State = EnvelopeState.Opened;
        }

        public String Serialize(Boolean minify, Boolean base64Encode)
        {
            String jsonString = JsonConvert.SerializeObject
            (
                this,
                new JsonSerializerSettings()
                {
                    DateFormatString = "yyyy-MM-dd HH:mm:ss.fffffffK",
                    Formatting = (minify ? Formatting.None : Formatting.Indented),
                    NullValueHandling = NullValueHandling.Include
                }
            );
            if (base64Encode)
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));
            else
                return jsonString;
        }

        public static Envelope? Deserialize(String data, Boolean isBase65Encoded)
        {
            if (isBase65Encoded)
                data = Encoding.UTF8.GetString(Convert.FromBase64String(data));
            return JsonConvert.DeserializeObject<Envelope>
            (
                data,
                new JsonSerializerSettings()
                {
                    DateFormatString = "yyyy-MM-dd HH:mm:ss.fffffffK",
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Include
                }
            );
        }

        public static Envelope Errored(String stateComment)
        {
            return new()
            {
                State = EnvelopeState.Errored,
                StateComment = stateComment,
            };
        }
    }
}
