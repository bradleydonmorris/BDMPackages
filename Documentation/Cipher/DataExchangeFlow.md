
# Data Exchange Flow

## Requirements
  * Data: a serialized request
  * RandomPassPhrase: Any random string
  * SenderRSAPrivateKey
  * SenderRSAPublicKey
  * ReceiverRSAPrivateKey
  * ReceiverRSAPublicKey

### Sender:
  1. Hash `Data` to use as a checksum
  2. Encrypt `Data` with AES using `RandomPassPhrase`
  3. Encrypt `RandomPassPhrase` with `ReceiverRSAPublicKey`
  4. Sign encrypted data with `SenderRSAPrivateKey`
  5. Sign encrypted passphrase with `SenderRSAPrivateKey`
  6. Serialize and send envelope

### Receiver:
  1. Receive and deserialize envelope
  2. Verify AES encrypted data with `SenderRSAPublicKey`
  3. Verify RSA encrypted data with `SenderRSAPublicKey`
  4. Decrypt `RandomPassPhrase` with `ReceiverRSAPrivateKey`
  5. Decrypt `Data` with AES using `RandomPassPhrase`
  6. Hash and verify decrypted data
