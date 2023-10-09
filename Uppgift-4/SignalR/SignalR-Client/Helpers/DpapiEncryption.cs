using System;
using System.Security.Cryptography;
using System.Text;

public static class DpapiEncryption
{
    // Dessa är de vanligaste flaggorna för DPAPI
    // UserScope betyder att endast den inloggade användaren kan dekryptera data
    private const DataProtectionScope Scope = DataProtectionScope.CurrentUser;

    public static string Encrypt(string data)
    {
        byte[] byteData = Encoding.UTF8.GetBytes(data);
        byte[] encryptedData = ProtectedData.Protect(byteData, null, Scope);
        return Convert.ToBase64String(encryptedData);
    }

    public static string Decrypt(string encryptedData)
    {
        byte[] byteEncryptedData = Convert.FromBase64String(encryptedData);
        byte[] decryptedData = ProtectedData.Unprotect(byteEncryptedData, null, Scope);
        return Encoding.UTF8.GetString(decryptedData);
    }
}
