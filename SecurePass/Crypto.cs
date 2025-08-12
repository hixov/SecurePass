using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class Crypto
{
    private const int SaltSize = 16; 
    private const int KeySize = 32;  
    private const int NonceSize = 12; 
    private const int TagSize = 16;  
    private const int Iterations = 100000; 

    public static byte[] Encrypt(string plaintext, string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        var keyDerivation = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        byte[] key = keyDerivation.GetBytes(KeySize);

        byte[] nonce = RandomNumberGenerator.GetBytes(NonceSize);
        byte[] tag = new byte[TagSize];
        byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
        byte[] ciphertext = new byte[plaintextBytes.Length];

        using (var aesGcm = new AesGcm(key, TagSize))
        {
            aesGcm.Encrypt(nonce, plaintextBytes, ciphertext, tag);
        }

        byte[] result = new byte[SaltSize + NonceSize + TagSize + ciphertext.Length];
        Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
        Buffer.BlockCopy(nonce, 0, result, SaltSize, NonceSize);
        Buffer.BlockCopy(tag, 0, result, SaltSize + NonceSize, TagSize);
        Buffer.BlockCopy(ciphertext, 0, result, SaltSize + NonceSize + TagSize, ciphertext.Length);

        return result;
    }

    public static string Decrypt(byte[] encryptedData, string password)
    {
        byte[] salt = new byte[SaltSize];
        Buffer.BlockCopy(encryptedData, 0, salt, 0, SaltSize);

        byte[] nonce = new byte[NonceSize];
        Buffer.BlockCopy(encryptedData, SaltSize, nonce, 0, NonceSize);

        byte[] tag = new byte[TagSize];
        Buffer.BlockCopy(encryptedData, SaltSize + NonceSize, tag, 0, TagSize);

        byte[] ciphertext = new byte[encryptedData.Length - SaltSize - NonceSize - TagSize];
        Buffer.BlockCopy(encryptedData, SaltSize + NonceSize + TagSize, ciphertext, 0, ciphertext.Length);

        var keyDerivation = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        byte[] key = keyDerivation.GetBytes(KeySize);

        byte[] decryptedBytes = new byte[ciphertext.Length];

        using (var aesGcm = new AesGcm(key, TagSize))
        {
            aesGcm.Decrypt(nonce, ciphertext, tag, decryptedBytes);
        }

        return Encoding.UTF8.GetString(decryptedBytes);
    }
}