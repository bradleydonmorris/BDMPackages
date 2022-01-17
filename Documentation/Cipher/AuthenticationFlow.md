
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
         - (Signing can't happen yet. Server does not have Client's public signing key till after the login flow is complete.)
  3. Serialize and send envelope

### Server:
  1. Receive and deserialize to request `Envelope`
     1. `Open()`
         - (Can't verify signature duing the login flow.)
         - Decrypt with `Server.RSA.Encryption.Private`
         - Verify hash match
  2. Desearlize `Content` to `LogingRequest`
     1. Verify `Email` and `Password`
     2. Store Client keys
  3. Build `LoginResponse`
     1. TrackId: random string
     2. Token: JWT signed with `Server.RSA.Signing.Private`
  4. Build request `Envelope`
     1. Set `Content` to `LoginResponse.Serilize()`
     2. Set `Passphrase` to `RandomPassPhrase`
     3. `Seal()`
         - Encrypt with `Client.RSA.Encryption.Public`
         - Sign with `Server.RSA.Signing.Private`
  5. Serialize and send envelope
