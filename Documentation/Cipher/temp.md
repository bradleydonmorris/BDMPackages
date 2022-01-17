
# Authentication Flow

## Requirements
  * EmailAddress
  * Password
  * RandomPassPhrase: Any random string
  * Client Keys
    - RSA
      - Encryption
        - Private (`Client.RSA.Encryption.Private`)
        - Public (`Client.RSA.Encryption.Public`)
      - Signing
        - Private (`Client.RSA.Signing.Private`)
        - Public (`Client.RSA.Signing.Public`)
  * Server Keys
    - RSA
      - Encryption
        - Private (`Server.RSA.Encryption.Private`)
        - Public (`Server.RSA.Encryption.Public`)
      - Signing
        - Private (`Server.RSA.Signing.Private`)
        - Public (`Server.RSA.Signing.Public`)

### Client:
  1. Build `LoginRequest`
     1. Email
     2. Password: One way hashed `Password` salted with known string, such as `EmailAdress`
     3. PublicEncryptionKeyXML: `Client.RSA.Encryption.Public`
     4. PublicSigningKeyXML: `Client.RSA.Signing.Public`
  2. Build request `Envelope`
     1. Set `Content` to `LoginRequest.Serilize()`
     2. Set `Passphrase` to `RandomPassPhrase`
     3. `Seal()`
         - Encrypt with `Server.RSA.Encryption.Public`
         - Sign with `Client.RSA.Signing.Private`
  3. Serialize and send envelope

### Server:
  1. Receive and deserialize to request `Envelope`
     1. `Open()`
         - Verify with `Client.RSA.Signing.Public`
         - Decrypt with `Server.RSA.Encryption.Private`
  2. Build `LogingResponse`
     1. 
  3. Decrypt `RandomPassPhrase` with `ReceiverRSAPrivateKey`
  3. Decrypt `Data` with AES using `RandomPassPhrase`
  4. Hash and verify decrypted data
  5. Deserialize `Envelope.Content` to `LoginRequest`
  6. Validate Email and hashed password
  7. Build `LoginRespoinse`
     1. TrackId: random string
     2. Token: JWT

